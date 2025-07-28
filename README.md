## 🎥 Demo

[![RFID Jukebox](https://github.com/user-attachments/assets/da9d6630-68cb-4750-9c60-8117232d3d21)](https://youtube.com/shorts/l3-CXZ5YFQk)

📺 **Click the thumbnail to watch the demo on Youtube Shorts**

# 🎵 RFID Jukebox

**RFID Jukebox** is an interactive IoT system that allows users to scan RFID cards to play corresponding music tracks through a WiFi-connected mobile application. This project uses an **Wemos D1 R1**, **Arduino Uno** and an **RFID reader** to send data wirelessly to a mobile app built using **Xamarin**.

The system demonstrates how RFID tag scanning can wirelessly trigger music playback, combining physical input with digital output for an engaging user experience.

---

## 💡 Project Overview

Each RFID card is pre-linked to a music track. When scanned using the RFID reader, the Arduino reads the card's UID and sends it via **HTTP over WiFi** to the mobile app. The app then identifies the track and plays it, displaying the song information on screen.

---

## 🎯 Features

- 📶 **WiFi Communication**  
  Sends RFID data from Arduino to mobile app over local network (HTTP or socket)

- 📱 **Mobile App (Xamarin)**  
  Receives UID and plays the corresponding track with song info display

- 🪪 **RFID Tag Integration**  
  Uses MFRC522 to scan unique RFID cards and match to songs

- 🎶 **Remote Audio Playback**  
  Wireless triggering of songs through app

---

## 🧰 Tech Stack

| Component     | Technology                       |
|---------------|----------------------------------|
| Hardware      | Wemos D1 R1, Arduino Uno |
| RFID Reader   | MFRC522 RFID Module              |
| Communication | HTTP requests over WiFi |
| Mobile App    | Xamarin.Forms (C#)               |
| Audio         | Local playback on mobile         |

---

## 🛠️ Setup & Installation

### 🖥️ Arduino Setup
- Connect MFRC522 RFID reader to Arduino
- Connect and configure your WiFi module (ESP8266 or similar)
- Upload the Arduino sketch
- Update WiFi SSID, password, and endpoint URL in code

### 📱 Mobile App Setup
- Open the Xamarin project in Visual Studio
- Build and deploy to Android device
- Ensure it is connected to the same local network as the Arduino
- App will listen for UID data and trigger the appropriate track

---

## 🔁 How It Works

1. User taps an RFID card on the reader  
2. Arduino reads the UID  
3. UID is sent to the mobile app over WiFi  
4. Mobile app maps UID to a song and plays it  
5. Song title and card ID are shown on screen  

---


## 📄 License

This project is for educational and personal use.  
Feel free to fork and modify!

---

