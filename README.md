# SetupAR

A Mixed Reality application developed in Unity that allows users to explore a virtual recording studio setup. Users can interact with various microphones, control audio tracks, and manage audio mixing (mute states and bass levels) in a 3D space.

The goal of this application is to avoid any extra work by offering to try out various setup options before manually setting up the microphones in the real studio.

## Manual / Controls

### AR Interactions (here used: Meta Quest 3)
The controls (except for moving the piano) are symmetrical for both the left and right controller.

| Input | Action |
| :--- | :--- |
| **Grip Button** (Side) | Grapple / Grab Objects |
| **Trigger Button** (Back) | Interact (Musicpad Buttons, Microphones) |
| **Button Y** | Move Piano |
| **Button A / X** | Open Music Menu |

### PC-Scene Interaction (Debug / Editor)
*Currently configured for VR/AR input. PC controls are for debugging purposes and only work in the scenes called "Debug_PC_Scene".*
* **Mouse Left Click:** Interact (via Raycast)
* **WASD:** Camera Movement (if Debug Camera enabled)

---

## Technical Implementation

### Core Architecture
The project is built on a modular architecture separating Data, Logic, and Visuals.

* **Singleton Pattern:** Used for core managers (`MicGenerator`, `InfoDisplayManager`) to ensure easy global access from UI elements and individual objects.
* **ScriptableObjects:** Microphone data (Name, Tracks, Icons, Prefabs) is stored in `MicData` assets, making it easy to add new content without changing code.

### Interaction System
The project uses a Interface-based interaction system to decouple input from logic.

* **`Interactable` Interface:** * When the **Interact** button is pressed, the system performs a Raycast (or BoxCast).
    * It checks if the hit object implements the `Interactable` interface.
    * If true, it executes the object's specific `.Interact()` method (e.g., a Button adds volume, a Microphone opens an info menu).

### Audio & Visual Logic
* **MicGenerator:** * Manages the list of all active microphones.
    * Handles AudioSource spawning.
    * Controls global Mute/Unmute logic.
    * **Bass Logic:** Implements special logic for the Bass mic. It is auto-muted when volume reaches 0 and auto-activated when volume > 0.
* **StaticMic:**
    * Uses **Billboarding** logic to make 2D Sprite icons always face the AR camera.
    * Handles **Distance Scaling**: Floor markers shrink/grow based on user proximity.
    * Visual Feedback: Swaps between `Active` and `Muted` sprites based on the `MicGenerator` state.

---

##  Project Structure

* `Scripts/Alternative`: Contains MicGenerator.cs, InfoDisplayManager.cs, all scripts used to build the scene logic the new "version" of the application.
* `Scripts/Legacy`: Contains older scripts that were worked on as part of the previous idea for the application.
* `Scripts`: Contains scripts mainly used to interact and implement the Meta Quest 3D, hence the Meta SDK in Unity

##  Setup & Installation

1.  Open the project in Unity (Version 6000.0.6.062f1 was used in our case).
2.  Ensure **Android Build Support** and **XR Plugin** are installed.
3.  Open the main scene `MetaScene`.
4.  Build and Run to Meta Quest via USB (or use Oculus Casting/Link for testing).

##  Credits

* Programming: 
   @diarsl
   @KolchinskiyK
   
* Designs and UI: 
   Michelle Hartl
   Chiara Leitner
   Julia Binder
   
* Audio Recording: 
   Anastasiia Krasnikova
