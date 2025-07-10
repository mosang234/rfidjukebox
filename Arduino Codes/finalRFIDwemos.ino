#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

const char* ssid = "ZTE_2.4G_7XW35E";
const char* password = "237F2ysL";
const char* host = "192.168.1.6";

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
      song_number = "692522124";
    }else if (data_rcvd == '2'){
      digitalWrite(D6, HIGH);
      digitalWrite(D7, LOW);
      song_number = "16715917491";
    }else if (data_rcvd == '3'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      song_number = "3111161228";
    }else if (data_rcvd == '4'){
      digitalWrite(D6, HIGH);
      digitalWrite(D7, LOW);
      song_number = "72100210131";
    }
    else if (data_rcvd == '5'){
      digitalWrite(D7, HIGH);
      digitalWrite(D6, LOW);
      song_number = "547168114";
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
         http2.begin(client, "http://192.168.1.6:8080/FINAL/update_song.php?songnumber="+ song_number);
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
         http2.begin(client, "http://192.168.1.6:8080/FINAL/update_stat_song.php?status_song="+ status_song);
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
