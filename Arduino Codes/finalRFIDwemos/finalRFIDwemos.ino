#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

const char* ssid = "change";
const char* password = "change";
const char* host = "change";

String song_number = "";
String status_song = "";

void setup() {

  pinMode(D7, OUTPUT);
  pinMode(D6, OUTPUT);
  Serial.begin(9600);
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
  delay(1000);
  Serial.print(".");
  }
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void loop() {
  if (Serial.available()) {
    char data_rcvd = Serial.read();
    if(data_rcvd == '1'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      //Change RFID Tag
      song_number = "Change RFID Tag";
    }else if (data_rcvd == '2'){
      digitalWrite(D6, HIGH);
      digitalWrite(D7, LOW);
      song_number = "Change RFID Tag";
    }else if (data_rcvd == '3'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      song_number = "Change RFID Tag";
    }else if (data_rcvd == '4'){
      digitalWrite(D6, HIGH);
      digitalWrite(D7, LOW);
      song_number = "Change RFID Tag";
    }
    else if (data_rcvd == '5'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      song_number = "Change RFID Tag";
    }else if (data_rcvd == '6'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      status_song = "1";
    }else if (data_rcvd == '7'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      status_song = "2";
    }

     if(WiFi.status()== WL_CONNECTED){
  
      if(WiFi.status()== WL_CONNECTED){
        WiFiClient client;
         HTTPClient http2;
         http2.begin(client, "http://ipaddress:port/FINAL/update_song.php?songnumber="+ song_number);
         http2.addHeader("Content-Type", "text/plain");
         http2.GET();
         String res = http2.getString();
         delay(1000);
        
      }
      else{
       Serial.println("Error in WiFi connection");
      }
    }
  }

  if(WiFi.status()== WL_CONNECTED){
  
      if(WiFi.status()== WL_CONNECTED){
        WiFiClient client;
         HTTPClient http2;
         http2.begin(client, "http://ipaddress:port/FINAL/update_stat_song.php?status_song="+ status_song);
         http2.addHeader("Content-Type", "text/plain");
         http2.GET();
         String res = http2.getString();
         delay(1000);
        
      }
      else{
       Serial.println("Error in WiFi connection");
      }
    }
  }
