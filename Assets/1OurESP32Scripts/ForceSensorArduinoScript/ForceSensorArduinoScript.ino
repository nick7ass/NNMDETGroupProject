#include <ArduinoWebsockets.h>
#include <WiFi.h>
#include <ESP32Servo.h>

const char* ssid = "dsv-extrality-lab";            // Change to your WiFi network name
const char* password = "expiring-unstuck-slider";  // Change to your WiFi password

using namespace websockets;

WebsocketsServer server;
WebsocketsClient client;
int forceSensorValue = 0;

const int serverLED = 11;
const int forceLED = 12;
const int buttonPin = 10;

bool forceDetected = false;  // Flag to track force detection
bool ledBlinked = false;     // Flag to ensure LED blinks only once


static const int servoPin = 8;
Servo servo1;

void setup() {
  Serial.begin(115200);

  servo1.attach(servoPin);

  pinMode(serverLED, OUTPUT);
  pinMode(forceLED, OUTPUT);
  pinMode(buttonPin, INPUT_PULLUP);  // Initialize button pin as input with internal pull-up resistor

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }

  Serial.print("Connected to WiFi. IP Address: ");
  Serial.println(WiFi.localIP());

  server.listen(81);
  Serial.println("WebSocket server started.");
  digitalWrite(serverLED, HIGH);  // Turn on server LED
}

void loop() {

  if (server.poll()) {
    client = server.accept();  // Accept client connection
    Serial.println("Client connected...");

    while (client.available()) {
      WebsocketsMessage msg = client.readBlocking();  // Read message from client

        // log
        Serial.print("Got Message: ");
        Serial.println(msg.data());
        forceSensorValue = analogRead(A0);
        Serial.println(forceSensorValue);
        delay(1000);

      if (msg.data().equalsIgnoreCase("Need Force") && !ledBlinked) {
        Serial.println("Reading value from Force Sensor...");
          

        while (true) {                 
          Serial.println(forceSensorValue);
          delay(1000);


          if (forceSensorValue > 7000) {
            // Send force value to Unity and blink LED
            Serial.println(forceSensorValue, DEC);
            Serial.println(forceSensorValue);
            Serial.println("\n");
            client.send(String(forceSensorValue));
            forceSensorValue = 0;
            break;
          }
        }
      }

      // Close client connection
      client.close();
      digitalWrite(serverLED, LOW);  // Turn off server LED
    }
  }

}