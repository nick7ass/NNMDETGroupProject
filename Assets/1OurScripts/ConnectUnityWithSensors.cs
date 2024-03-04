using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp; // Ensure this matches the WebSocket library you're using

public class ConnectUnityWithSensors : MonoBehaviour
{
    // Websocket Service
    WebSocket ws;
    //public AudioSource audioSource; // Assign in inspector
    //public AudioClip narrationClip; // Assign in inspector
    //
    public string esp32IPAddress = "10.204.0.249"; // Assign your ESP32 IP Address
    public string esp32WebsocketPort = "81"; // Assign your ESP32 WebSocket port, typically "81"

    private bool forceDataReceived = false;
    private int receivedForceValue = 0;

    public static bool isForceDetected = false;

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
            bool isNumeric = int.TryParse(e.Data, out parsedValue);
            if (isNumeric)
            {
                receivedForceValue = parsedValue;
                forceDataReceived = true; // Indicate that new data has been received
            }
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    

    void Update()
    {
        if (earthScript.narrationHasFinished && !earthScript.seedHasAppeared)
        {
            Debug.Log("Asking for force.");

            ws.Send("Need Force");

            if (forceDataReceived)
            {
                if (receivedForceValue > 40)
                {
                    Debug.Log("Force threshold exceeded, action triggered.");
                    isForceDetected = true;
                    earthScript.collectForce();

                }
                forceDataReceived = false; // Reset for the next message
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
