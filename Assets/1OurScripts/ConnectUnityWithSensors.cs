using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp; // Ensure this matches the WebSocket library you're using

public class ConnectUnityWithSensors : MonoBehaviour
{
    WebSocket ws;
    public string esp32IPAddress = "10.204.0.248";
    public string esp32WebsocketPort = "81";

    private bool forceDataReceived = false;
    private int receivedForceValue = 0;
    public static bool isForceDetected = false;

    private Coroutine forceCheckCoroutine = null; // Reference to the coroutine for managing its lifecycle

    public BoundEarthScript earthScript = new BoundEarthScript();

    void Start()
    {
        ConnectWithESP32();
    }

    public void ConnectWithESP32()
    {
        Debug.Log("Connecting Unity with ESP32 via Websockets...");
        ws = new WebSocket($"ws://{esp32IPAddress}:{esp32WebsocketPort}");
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connected");
            ws.Send("Hello from Unity!");
        };
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received message: " + e.Data);
            int parsedValue;
            if (int.TryParse(e.Data, out parsedValue))
            {
                receivedForceValue = parsedValue;
                forceDataReceived = true;

                if (receivedForceValue > 100 && !isForceDetected)
                {
                    Debug.Log("Force detected immediately, cancelling timeout.");
                    isForceDetected = true;
                    earthScript.collectForce();

                    // Cancel the timeout coroutine if it's running
                    if (forceCheckCoroutine != null)
                    {
                        StopCoroutine(forceCheckCoroutine);
                        forceCheckCoroutine = null;
                    }
                }
            }
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    void Update()
    {
        if (earthScript.narrationHasFinished && !earthScript.seedHasAppeared && !isForceDetected)
        {
            Debug.Log("Checking for force...");

            // Start the timeout coroutine only if it hasn't been started yet
            if (forceCheckCoroutine == null)
            {
                forceCheckCoroutine = StartCoroutine(IfForceUnavailable());
            }
        }
    }

    IEnumerator IfForceUnavailable()
    {
        yield return new WaitForSeconds(30); // Wait for 30 seconds

        // Trigger the action if no force has been detected by this time
        if (!isForceDetected)
        {
            Debug.Log("No force detected within 30 seconds, action triggered.");
            isForceDetected = true;
            earthScript.collectForce();
        }
    }

    void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }
}