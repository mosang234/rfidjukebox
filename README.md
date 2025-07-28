## 🎥 Demo

[![RFID Jukebox](https://github.com/user-attachments/assets/da9d6630-68cb-4750-9c60-8117232d3d21)](https://youtube.com/shorts/l3-CXZ5YFQk)

📺 **Click the thumbnail to watch the demo on Youtube Shorts**

# 🎵 RFID Jukebox

The **RFID Jukebox** is an interactive system that allows users to scan RFID cards to play corresponding audio tracks. Designed as a fun and hands-on IoT project, this jukebox merges hardware with software—built using **Arduino** and a **mobile app interface via Xamarin**—to demonstrate real-time interaction between physical inputs and digital outputs.

---

## 💡 Project Overview

Each RFID card is linked to a specific audio file. When scanned using the RFID reader, the system identifies the card's unique ID and triggers the playback of its assigned song on the connected speaker or app interface.

This project showcases:
- RFID-based user interaction
- Serial communication between Arduino and a mobile app
- Music playback logic based on RFID tag mapping

---

## 🎯 Features

- 📻 **RFID Card Recognition**  
  Detects unique RFID tags and maps them to specific songs

- 📱 **Mobile App (Xamarin)**  
  Displays song titles and controls playback via serial communication

- 🎶 **Audio Playback System**  
  Plays pre-assigned tracks based on scanned RFID tag

- 🔌 **Arduino Integration**  
  Handles RFID scanning and sends data to the mobile app via USB/Serial

---

## 🧰 Tech Stack

| Component     | Technology           |
|---------------|----------------------|
| Hardware      | Arduino Uno, MFRC522 RFID Reader |
| Mobile App    | Xamarin (C#)         |
| Communication | Serial (USB)         |
| Audio         | External MP3 player / App-controlled |

---

## 🛠️ Setup & Installation

### 🖥️ Hardware Setup
- Connect MFRC522 RFID reader to the Arduino
- Upload the Arduino sketch (`rfidjukebox.ino`) using the Arduino IDE
- Use USB to establish serial communication with your PC or mobile device

### 📱 Mobile App
- Open the Xamarin project in Visual Studio
- Build and deploy the app to an Android device
- Ensure it can access the serial port or simulate playback upon receiving card data

---

## 🔁 How It Works

1. User taps an RFID card on the reader
2. Arduino reads the RFID UID
3. UID is sent over Serial to the mobile app
4. Mobile app maps UID to a specific track and plays the song
5. Optionally, the song title is displayed on screen

---


## 📄 License

This project is for educational and personal use.  
Feel free to fork and modify!

---

