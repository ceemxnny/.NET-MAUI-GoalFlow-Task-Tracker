# Mobile Computing Assessment 25/26

# GoalFlow - Task and Habit Tracker

**Author:** Colin Nartey
**Module:** 6G6Z0014 – Mobile Computing

## About The App
This is a cross-platform mobile application built using .NET MAUI. It serves as an efficient Task and Habit Tracker designed to help users manage their daily routines. The app goes beyond a simple to do list by integrating directly with native device hardware to track user's physical activity and provide physical feedback, ensuring a vert interactive user experience.

## Brief Development Plan (Feature Roadmap)
* **Dashboard (Completed):** A dynamic task list with interactive checkboxes and a real-time hardware step counter.
* **Journal (Planned):**
  A private notes section secured by the device's biometric sensors alongside geocoding for users to tag their locations in their journal(FaceID/Fingerprint).
* **Location Alerts (Planned):** Integration of notifications which activate once the user completes a task.
* **Settings & Profile (Planned):** User configuration and customization pages.

## Technical Architecture
This application strictly adheres to the **MVVVM** design pattern to cleanly separate the user interface from the logic. 
* **UI Construction:** Built entirely using XAML with dynamic data templates like `CollectionView` for rendering infinite task lists.
* **Data Binding:** Utilizes `ICommand` structures for user interactions and `IValueConverter` classes to translate backend boolean logic into visual UI feedback dynamically.

## Hardware Integration & Features
This app actively uses native mobile hardware features via the `Microsoft.Maui.Devices.Sensors` API:

1. **Accelerometer (Step Counter)(Doing):** 
   Instead of relying on standard UI buttons, the app reads movement data from the device's accelerometer. It calculates the total G-force of the device to detect the physical "jolt" of a user taking a step, updating the pedometer UI in real-time.
2. **Haptic Feedback(Doing):**
   Utilizes the device's native vibration motor. When a user toggles a daily task as "completed," the app triggers a physical haptic click, confirming the UI interaction.
