# Projectile Launcher Assignment

Developer: Ayush Kumar Jha  
Engine: Unity  
Platform: Android  
Build File: ProjectileLauncher_Assignment_V2_Ayush_jha.apk  

---

1. Project Overview
This project is a physics-based projectile launching game developed in Unity. The player launches a projectile by dragging it and releasing it toward the target. 

The project focuses on:
- Projectile physics simulation
- Trajectory prediction
- Surface interaction and collision handling
- Gameplay state management

A playable Android APK has been created and successfully tested on a physical Android device.

---

2. Controls
Mobile Controls (Touch):
- Drag the Projectile: Adjusts the aiming direction and force.
- Release the Projectile: Launches the ball toward the target.
- Red Trajectory Line: Displays the predicted physics path prior to launch.
- Objective: Hit the target within the allowed time.

Note: The same drag-based control scheme is supported in the Unity Editor Game View.

---

3. Implementation Approach

Projectile Launch:
- Uses Unity's Rigidbody component for physics motion.
- Drag distance determines velocity and force.
- Converts screen-space drag input into an appropriate 3D world direction relative to the main camera.
- Uses Rigidbody.AddForce() with ForceMode.Impulse to initiate motion.

Trajectory Prediction:
- Implemented using a Line Renderer.
- Calculates real-time projectile paths using standard kinematic movement equations.
- Ensures the predicted path matches the actual physics trajectory identically.

---

4. Surface Interaction Logic
Managed via the SurfaceInteraction C# script utilizing OnCollisionEnter().

- Target Interaction: Colliding with the Target or GoalMaker triggers a successful hit and transitions to the Success State.
- Path Interaction: Bouncing off or rolling along the surface/path is permitted and does not trigger an immediate failure.
- Failure Conditions:
  1. Time Limit: A 5-second timer starts upon launch. Failing to hit the target within 5 seconds triggers Game Over.
  2. Stuck Detection: If the projectile moves below a minimum movement threshold for ~1.5 seconds, it is marked as stuck, entering the Game Over state.

---

5. Game Management
The GameManager script oversees global gameplay flow:
- Tracks Success State and Failure / Game Over State.
- Handles full session restarts and UI overlays.

---

6. Android Build & Testing
- Built and verified as an Android APK (.apk).
- Setup and build configuration challenges were investigated, debugged, and resolved.
- Tested on a physical Android device, verifying touch dragging, trajectory rendering, surface interactions, and UI states.

---

7. Challenges Faced
- Translating screen-space touch drag into accurate 3D world vector directions.
- Synchronizing visual trajectory paths with Unity's physics system.
- Handling path collisions without interrupting active gameplay.
- Implementing robust stuck-detection logic for low-velocity states.
- First-time Android build setup and deployment debugging.

---

8. Developer Note
I have given my best effort based on my current knowledge. Moving forward, I am eager to expand my skills and improve even further in future projects.

---

9. Technologies Used
- Engine: Unity
- Language: C#
- Physics & Components: Rigidbody, Line Renderer, Raycasting, Physics Gravity
- IDE: Visual Studio / Unity Editor
- AI Assistance: ChatGPT
- Target OS: Android