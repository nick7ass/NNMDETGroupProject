using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp; // Ensure this matches the WebSocket library you're using

public class WaterConnectUnityWithSensors : MonoBehaviour
{
    // Websocket Service
    WebSocket ws;
    public string esp32IPAddress = "10.204.0.249"; // Assign your ESP32 IP Address
    public string esp32WebsocketPort = "81"; // Assign your ESP32 WebSocket port, typically "81"

    private bool touchDataReceived = false;
    private int receivedTouchValue = 0;
    public static bool isTouchDetected = false;
    int threshhold = 14000;
    public BoundWaterScript waterScript;

    private float narrationEndTime;
    private float waitTime = 15f;
    private bool objectAppeared = false; // Flag to track whether the object has appeared or not
    private bool startedMeasuringTime = false; // Flag to track whether the time measurement has started

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
            bool isNumeric = int.TryParse(e.Data, out parsedValue);
            if (isNumeric)
            {
                receivedTouchValue = parsedValue;
                touchDataReceived = true; // Indicate that new data has been received
            }
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    void Update()
    {
        if (waterScript.narrationHasFinished && !waterScript.dropHasAppeared)
        {
            if (!startedMeasuringTime)
            {
                startedMeasuringTime = true;
                narrationEndTime = Time.realtimeSinceStartup;
            }

            float elapsedTime = Time.realtimeSinceStartup - narrationEndTime;

            if (elapsedTime < waitTime)
            {
                Debug.Log("Asking for touch.");
                ws.Send("Need Touch");

                if (touchDataReceived)
                {
                    if (receivedTouchValue >= threshhold)
                    {
                        Debug.Log("Touch threshold exceeded, action triggered.");
                        isTouchDetected = true;
                        waterScript.collectTouch();
                        return; // Exit the update loop if touch threshold condition is met
                    }
                    touchDataReceived = false; // Reset for the next message
                }
            }
            else if (!objectAppeared && elapsedTime >= waitTime)
            {
                Debug.Log("15 seconds have passed since the end of narration.");
                // Perform your desired action here after 15 seconds from the end of the narration
                waterScript.collectTouch(); // Call collectTouch() method after 15 seconds
                objectAppeared = true; // Set the flag to indicate that the object has appeared
            }
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