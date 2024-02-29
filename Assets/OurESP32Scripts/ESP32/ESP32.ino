#include <ArduinoWebsockets.h>
#include <WiFi.h>

const char* ssid = "dsv-extrality-lab"; // Change to your WiFi network name
const char* password = "expiring-unstuck-slider"; // Change to your WiFi password

using namespace websockets;

WebsocketsServer server;
WebsocketsClient client;
int forceSensorValue = 0;
const int forceSensorPin = 34; // Change to your actual force sensor pin

void setup() {
  Serial.begin(115200);
  
  // Initialize force sensor pin as input
  pinMode(forceSensorPin, INPUT);
  
  // Connect to WiFi
  WiFi.begin(ssid, password);

  // Wait to connect to WiFi
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }

  Serial.println("Connected to WiFi");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());

  // Start WebSocket server
  server.listen(81);
  Serial.println("WebSocket server started.");
}

void loop() {
   //Serial.println(WiFi.localIP());
if (server.poll()) {  //server.poll() checks if any client is waiting to connect
    Serial.println("Client is available to connect...");
    client = server.accept();  // Accept() --> what server.accept does, is: "server, please wait until there is a client knocking on the door. when there is a client knocking, let him in and give me it's object".
    Serial.println("Client connected...");

    while (client.available()) {
      
        Serial.println("Waiting for client to send a message...");
        WebsocketsMessage msg = client.readBlocking();//readBlocking(removes the need for calling poll()) will return the first message or event received. readBlocking can also return Ping, Pong and Close messages.
       
        // log
        Serial.print("Got Message: ");
        Serial.println(msg.data());
        // Condition to blink the light at the start of program as a hello indication
        if(msg.data().startsWith("Hello")){
          for(int i=0;i<4;i++){
            digitalWrite(11, HIGH);  //Blink on 
            delay(170);
            digitalWrite(11, LOW);  //Blink off 
          }
          client.send(String(10));  
        }
        if (msg.data().equalsIgnoreCase("Need Force")) {
          digitalWrite(11, HIGH);  //Notify user to use force sensor
          Serial.println("Reading value from Force Sensor...");
          while (forceSensorValue <= 20) {

            analogReadResolution(10);  // This statement tells in how many bits the AnalogRead should happen.
            // analogRead function returns the integer 10 bit integer (0 to 1023)
            forceSensorValue = analogRead(A0);

            if (forceSensorValue > 20) {
              digitalWrite(13, HIGH);
              Serial.print(forceSensorValue, DEC);
              Serial.print("\n");  // Sending New Line character is important to read data in unity
              Serial.flush();

              // return echo
              client.send(String(forceSensorValue));
              digitalWrite(11, LOW);  //Notify user NOT to use force sensor
              digitalWrite(13, LOW);  // Turn off built-in LED to notify user that value reading is done.
              forceSensorValue = 0;
              break;
            }
          }
        }
        
    }
    // close the connection
    client.close();
  }
}

