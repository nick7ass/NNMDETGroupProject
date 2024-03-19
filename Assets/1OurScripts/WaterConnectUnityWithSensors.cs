using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp; // Ensure this matches the WebSocket library you're using

public class WaterConnectUnityWithSensors : MonoBehaviour
{
    WebSocket ws;
    public string esp32IPAddress = "10.204.0.249";
    public string esp32WebsocketPort = "81";

    private bool touchDataReceived = false;
    private int receivedTouchValue = 0;
    public static bool isTouchDetected = false;

    int threshold = 14000;

    private Coroutine touchCheckCoroutine = null; // Reference to the coroutine

    public BoundWaterScript waterScript = new BoundWaterScript();

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
            ws.Send("Hello from Unity Water Script!");
        };
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received message: " + e.Data);
            int parsedValue;
            if (int.TryParse(e.Data, out parsedValue))
            {
                receivedTouchValue = parsedValue;
                touchDataReceived = true;

                if (receivedTouchValue >= threshold && !isTouchDetected)
                {
                    Debug.Log("Touch detected, cancelling timeout.");
                    isTouchDetected = true;
                    waterScript.collectTouch();

                    // Cancel the timeout coroutine if it's running
                    if (touchCheckCoroutine != null)
                    {
                        StopCoroutine(touchCheckCoroutine);
                        touchCheckCoroutine = null;
                    }
                }
            }
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    void Update()
    {
        if (waterScript.narrationHasFinished && !waterScript.dropHasAppeared && !isTouchDetected)
        {
            Debug.Log("Checking for touch...");

            // Start the timeout coroutine only if it hasn't been started yet
            if (touchCheckCoroutine == null)
            {
                touchCheckCoroutine = StartCoroutine(IfTouchUnavailable());
            }
        }
    }

    IEnumerator IfTouchUnavailable()
    {
        yield return new WaitForSeconds(30); // Wait for 30 seconds

        // Trigger the action if no touch has been detected by this time
        if (!isTouchDetected)
        {
            Debug.Log("No touch detected within 30 seconds, action triggered.");
            isTouchDetected = true;
            waterScript.collectTouch();
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
