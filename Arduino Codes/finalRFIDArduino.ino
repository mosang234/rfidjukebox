#include <Arduino_FreeRTOS.h>
#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN 9
#define SS_PIN 10

MFRC522 mfrc522(SS_PIN, RST_PIN);

TaskHandle_t rfid_task;
TaskHandle_t play_task;
TaskHandle_t pause_task;

int sw1, sw2;


void setup() {
  Serial.begin(9600);
  SPI.begin();
  mfrc522.PCD_Init();

  pinMode(7, INPUT); //Button for Pause
  pinMode(6, INPUT); //Button for Play

  xTaskCreate(RFID, "RFID", 100, NULL, 2, &rfid_task);
  xTaskCreate(Play, "Play", 100, NULL, 1, &play_task);
  xTaskCreate(Pause, "Pause", 100, NULL, 1, &pause_task);



}

void loop() {
  // put your main code here, to run repeatedly:

}

static void RFID(void* pvParameters)
{
  while(1){
     if (mfrc522.PICC_IsNewCardPresent() && mfrc522.PICC_ReadCardSerial()) {
    String rfidTagId = "";
    for (byte i = 0; i < mfrc522.uid.size; i++) {
      rfidTagId += mfrc522.uid.uidByte[i];
    }
    if(rfidTagId == "692522124"){
      Serial.write('1');
    }else if (rfidTagId == "16715917491"){
      Serial.write('2');
    }else if (rfidTagId == "3111161228"){
      Serial.write('3');
    }else if (rfidTagId == "72100210131"){
      Serial.write('4');
    }else if (rfidTagId == "547168114"){
      Serial.write('5');
    }
   mfrc522.PICC_HaltA();
    } 
  vTaskDelay(100/portTICK_PERIOD_MS);
  taskYIELD();
  }
}

static void Play(void* pvParameters)
{
  while(1){
    sw1 = digitalRead(6);
    if(sw1 == HIGH){
      Serial.write('6');
    }
    vTaskDelay(100/portTICK_PERIOD_MS);
    taskYIELD();
  }
}

static void Pause(void* pvParameters)
{
while(1){
    sw2 = digitalRead(7);
    if(sw2 == HIGH){
      Serial.write('7');
    }
    vTaskDelay(100/portTICK_PERIOD_MS);
    taskYIELD();
  }
}

