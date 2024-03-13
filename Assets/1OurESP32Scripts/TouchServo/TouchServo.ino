#include <ArduinoWebsockets.h>
#include <WiFi.h>
#include <ESP32Servo.h>

const char* ssid = "dsv-extrality-lab";            // Change to your WiFi network name
const char* password = "expiring-unstuck-slider";  // Change to your WiFi password

using namespace websockets;

WebsocketsServer server;
WebsocketsClient client;

//#define TOUCH_PIN 10
//#define TOUCH_THRESHOLD 100

bool touchDetected = false;  // Flag to track touch detection
bool touchHandled = false;   // Flag to track if touch has been handled

int touchValue;

static const int servoPin = 8;
Servo servo1;

void setup() {
  Serial.begin(115200);

  servo1.attach(servoPin);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }

  Serial.print("Connected to WiFi. IP Address: ");
  Serial.println(WiFi.localIP());

  server.listen(81);
  Serial.println("WebSocket server started.");
}

void loop() {
  if (server.poll()) {
    client = server.accept();
    Serial.println("Client connected...");

    while (client.available()) {
      WebsocketsMessage msg = client.readBlocking();
      Serial.print("Got Message: ");
      Serial.println(msg.data());

      if (msg.data().equalsIgnoreCase("Need Touch")) {
        Serial.println("Reading value from touch sensor!");
        //touchValue = touchRead(4);
        Serial.println(touchRead(4));

        while (touchRead(4) < 14000) {
          if (touchRead(4) >= 14000) {
            Serial.println("Value above threshold");

            rotateServo();

            client.send(String(touchRead(4)));

            break;
          }
        }
      }
      
    }
    client.close();
  }
}



void rotateServo() {
  // Rotate the servo motor 180 degrees
  for (int posDegrees = 0; posDegrees <= 180; posDegrees++) {
    servo1.write(posDegrees);
    delay(10);  // Adjust the delay for smooth rotation
  }
}