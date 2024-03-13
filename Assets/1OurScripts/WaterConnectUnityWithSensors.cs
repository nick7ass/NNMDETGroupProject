using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp; // Ensure this matches the WebSocket library you're using

public class WaterConnectUnityWithSensors : MonoBehaviour
{
    // Websocket Service
    WebSocket ws;
    //public AudioSource audioSource; // Assign in inspector
    //public AudioClip narrationClip; // Assign in inspector
    //
    public string esp32IPAddress = "10.204.0.249"; // Assign your ESP32 IP Address
    public string esp32WebsocketPort = "81"; // Assign your ESP32 WebSocket port, typically "81"

    private bool touchDataReceived = false;
    private int receivedTouchValue = 0;

    public static bool isTouchDetected = false;

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
    {//Change to Water script 
        if (waterScript.narrationHasFinished && !waterScript.dropHasAppeared)
        {
            Debug.Log("Asking for touch.");

            ws.Send("Need Touch");

            if (touchDataReceived)
            {
                if (receivedTouchValue == 10)
                {
                    Debug.Log("Distance threshold exceeded, action triggered.");
                    isTouchDetected = true;
                    waterScript.collectTouch();

                }
                touchDataReceived = false; // Reset for the next message
            }
        }

        //Failsafe in case something goes wrong (Get key down to make it so if the sensor
        //doesnt work or is giving issues we can control it somehow (but how if we use apk lol????)
       



    }

    void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }

}

