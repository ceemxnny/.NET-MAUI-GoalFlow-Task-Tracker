# GoalFlow - Task and Habit Tracker

## About The App
This is a cross-platform mobile application built using C# and .NET MAUI. It serves as an efficient Task and Habit Tracker designed to help users manage their daily routines. The app goes beyond a simple to do list by integrating directly with native device hardware to track user's physical activity and provide physical feedback, ensuring a very interactive user experience.

## Application Features
* **Task Dashboard & Steps:** A dynamic task list with interactive checkboxes and a real-time hardware step counter.
* **Security Locked Journal:** A private notes section secured by the device's biometric sensors alongside geocoding for users to tag their locations in their journal(FaceID/Fingerprint).
* **App Notifications:** Integration of notifications which activate once the user completes a task.
* **Settings & Profile :** User configuration and customization pages.

## Technical Architecture
This application strictly adheres to the **MVVVM** design pattern to cleanly separate the user interface from the logic. 
* **UI Construction:** Built entirely using XAML with dynamic data templates like `CollectionView` for rendering infinite task lists.
* **Data Binding:** Utilizes `ICommand` structures for user interactions and `IValueConverter` classes to translate backend boolean logic into visual UI feedback dynamically.

## Hardware Integration & Features
This app actively uses native mobile hardware features via the `Microsoft.Maui.Devices.Sensors` API:

1. **Accelerometer (Step Counter):**  Instead of relying on standard UI buttons, the app reads movement data from the device's accelerometer. It calculates the total G-force of the device to detect the physical "jolt" of a user taking a step, updating the pedometer UI in real-time.
2. **Haptic Feedback:**
   Utilizes the device's native vibration motor. When a user toggles a daily task as "completed," or when they delete a photo, the app triggers a physical haptic click, confirming the UI interaction.
3. **Camera:**
   Allows the user to take and save photos directly inside journal entries.
4. **Data Persistence:** 
   Uses `Preferences` to save journal text, captured images, and step counts so data is not lost between sessions.
5. **Cross-Platform Routing:** 
   Custom AppShell routing implemented for seamless tab navigation.
6. **Form Validation:** 
   Prevents the saving of empty journal entries and handles hardware permission denials gracefully without crashing.
7. **Text-to-Speech (TTS):** 
   Uses the native OS voice synthesizer to read journal entries out loud.
8. **Biometric Authentication (Fingerprint/FaceID):** 
   Secures the private journal.
9. **Location/GPS:** 
   Utilises the device's geolocation hardware to display the user's current position on a map.



## GoalFlow App Wireframe Design:
<img width="984" height="657" alt="Screenshot 2026-04-30 at 05 39 02" src="https://github.com/user-attachments/assets/a8d4114e-3148-4b6f-9518-a6abf9e89ee8" />

<img width="945" height="655" alt="Screenshot 2026-04-30 at 05 36 58" src="https://github.com/user-attachments/assets/056135a8-32bd-409f-aee4-44c5b11a19d6" />

## How to Run the App (Testing Notes)

This app requires **.NET 9.0** and the **.NET MAUI workload** installed via Visual Studio 2022 or Visual Studio Code.

### Android Emulator Setup Notes:
Because this app relies heavily on strict Android hardware permissions, please note the following if testing on an Android Emulator:
1. **Biometrics:** The Android Emulator must have a PIN and a "fake" fingerprint enrolled in the device settings before the unlock button will trigger the native prompt. You can simulate a touch using the emulator's Extended Controls (...) -> Fingerprint menu.
2. **Google Maps:** The app uses a placeholder API key in the `AndroidManifest.xml` to prevent crashes and demonstrate UI rendering. On an emulator, the map will load as a beige grid, but the app will successfully trigger a GPS coordinate popup proving the hardware ping was successful. 
3. **Text-to-Speech:** Ensure the emulator's media volume is turned up to hear the TTS hardware synthesizer read the journal entries.
