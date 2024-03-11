#include <ArduinoWebsockets.h>
#include <WiFi.h>

const char* ssid = "dsv-extrality-lab";            // Change to your WiFi network name
const char* password = "expiring-unstuck-slider";  // Change to your WiFi password

using namespace websockets;

WebsocketsServer server;
WebsocketsClient client;

const int trigPin = A5;  // Change to the GPIO pin connected to the Trig pin of the HC-SR04
const int echoPin = A1;  // Change to the GPIO pin connected to the Echo pin of the HC-SR04

void setup() {
  Serial.begin(115200);

  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);

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

      if (msg.data().equalsIgnoreCase("Need Distance")) {
        Serial.println("Measuring distance...");
        // Trigger the sensor
        digitalWrite(trigPin, LOW);
        delayMicroseconds(2);
        digitalWrite(trigPin, HIGH);
        delayMicroseconds(20);  // Adjust this value
        digitalWrite(trigPin, LOW);

        // Read the echo pulse duration
        unsigned long duration = pulseIn(echoPin, HIGH);

        // Calculate distance in centimeters
        int distance = duration * 0.0343 / 2.0;

        // Print the distance to the Serial Monitor
        Serial.print("Duration: ");
        Serial.print(duration);
        Serial.print(" microseconds, Distance: ");
        Serial.print(distance);
        Serial.println(" cm");

        delay(1000);


        // Define a close distance threshold, for example, 10 cm
        if (distance > 0 && distance < 10) {
          // If the object is within the threshold, send a signal to Unity
          Serial.println("Object is close. Sending signal to Unity...");
          //client.send("ObjectClose");
          client.send(String(distance));
        } else {
          // Optional: Send a different signal if the object is not within the threshold
          //client.send("ObjectFar");
        }
      }
    }
    client.close();
  }
}