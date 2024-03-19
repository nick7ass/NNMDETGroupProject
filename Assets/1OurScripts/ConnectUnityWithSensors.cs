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
    private bool isForceDetected = false;
    private bool waitForForceCoroutineStarted = false; // New flag to control coroutine start

    public BoundEarthScript earthScript = new BoundEarthScript();

    void Start()
    {
        ConnectWithESP32();
    }

    public void ConnectWithESP32()
    {
        Debug.Log("Connecting Unity with ESP32 via Websockets...");
        ws = new WebSocket($"ws://{esp32IPAddress}:{esp32WebsocketPort}");
        ws.OnOpen += (sender, e) => {
            Debug.Log("WebSocket connected");
            ws.Send("Hello from Unity!");
        };
        ws.OnMessage += (sender, e) => {
            Debug.Log("Received message: " + e.Data);
            int parsedValue;
            bool isNumeric = int.TryParse(e.Data, out parsedValue);
            if (isNumeric)
            {
                receivedForceValue = parsedValue;
                forceDataReceived = true;
            }
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    void Update()
    {
        if (earthScript.narrationHasFinished && !earthScript.seedHasAppeared && !waitForForceCoroutineStarted)
        {
            Debug.Log("Asking for force.");
            ws.Send("Need Force");
            StartCoroutine(IfForceUnavailable());
            waitForForceCoroutineStarted = true; // Ensure coroutine is started only once
        }

        if (forceDataReceived)
        {
            if (receivedForceValue > 100 && !isForceDetected)
            {
                Debug.Log("Force threshold exceeded, action triggered.");
                isForceDetected = true;
                earthScript.collectForce();
            }
            forceDataReceived = false; // Reset for the next message
        }
    }

    IEnumerator IfForceUnavailable()
    {
        yield return new WaitForSeconds(10); // Ensure this is 30 seconds, not 30000
        if (!isForceDetected)
        {
            Debug.Log("No force detected in 30 seconds, triggering action.");
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