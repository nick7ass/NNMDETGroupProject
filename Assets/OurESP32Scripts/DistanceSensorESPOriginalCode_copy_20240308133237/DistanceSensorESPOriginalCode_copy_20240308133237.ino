#include <Ultrasonic.h>

// Define the pins for the HC-SR04
const int trigPin = A5; // Replace with the GPIO pin connected to the Trig pin
const int echoPin = A1; // Replace with the GPIO pin connected to the Echo pin

Ultrasonic ultrasonic(trigPin, echoPin);

void setup() {
  Serial.begin(115200);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
}

void loop() {
  // Trigger the sensor
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(20);  // Adjust this value
  digitalWrite(trigPin, LOW);

  // Read the echo pulse duration
  unsigned long duration = pulseIn(echoPin, HIGH);

  // Calculate distance in centimeters
  float distance = duration * 0.0343 / 2.0;

  // Print the distance to the Serial Monitor
  Serial.print("Duration: ");
  Serial.print(duration);
  Serial.print(" microseconds, Distance: ");
  Serial.print(distance);
  Serial.println(" cm");

  delay(1000); // Adjust the delay based on your needs
}
