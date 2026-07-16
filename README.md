# Adventure_Game_3D
 3D adventure game project containing exercises related to modules 27 through 40 of the EBAC Unity developer course.

## April 19th, 2026
### Creating a 3D adventure game: Basic Structure

A 3D adventure game project (Prototype_Scene_01) has been initiated, containing EBAC art packages such as prefabs for scenarios, enemies, and the player, as well as animations and state machines. Other important plugins were included in the project, such as DoTween, NaughtyAttributes, Cinemachine, and Recording.

The game will be inspired by games like Super Lucky's Tale and Super Mario 3D Worlds.

The game is planned to have: 

* 1 main character, with integrated animations, who walks, jumps, dies, and attacks; 
* coins and/or collectible items scattered throughout the level; 
* chests or items that distribute coins when broken; 
* enemies with different behaviors; 
* and at least 1 simple boss.

<img width="280" height="170" alt="main character and enemies" src="https://github.com/user-attachments/assets/c40d9953-aa99-4048-9994-eb8921790823" />

<img width="280" height="280" alt="Prototype Scene" src="https://github.com/user-attachments/assets/1c12973d-415b-4941-8b8a-6bc083a00d8b" />

## April 20th, 2026
### Implementing a State Machine

In this step, I started building a state machine for the game that will be developed in this project. The summary can be seen in the following figure.

<img width="1952" height="1110" alt="Captura de tela 2026-04-20 151730" src="https://github.com/user-attachments/assets/77e659c5-7680-4496-8e4e-48a4706b2742" />
In this example, we will have a state for each scene or main condition of the game. The "menu state" leads to the "game state," where two possible paths can be followed, leading to new machine states, such as the "lose state," which allows the player to restart the game in the "try again state," and if the player wins, they are directed to the "win state," which closes the cycle and leads to the "menu state."

## April 21th, 2026
### Implementing a State Machine, FSM and Game Manager

A project folder, EDGEE_CORE, was created to include the state machine.cs and state base.cs scripts. In this step, two scripts were created for configuring the FSM and the state machine editor. A game manager was also configured for the game to start the project.

In this step, the foundation of a generic finite state machine for the project was created. I added a StateMachine<T> generic class that stores states in a Dictionary, manages the current state, and exposes Init, RegisterStates, SwitchState and Update methods which call the virtual callbacks on a StateBase class (OnStateEnter/Stay/Exit). A simple FSM MonoBehaviour demonstrates registering enum-based states, and a Custom Editor (StateMachineEditor) provides an inspector view to visualize the current state and registered states in the editor. I also included a small Test class showing how to instantiate and register states programmatically. This setup establishes the core FSM architecture and editor tooling to build concrete states and drive gameplay logic. The Run, Stop, and Jump states were included in the FSM script.

<img width="280" height="170" alt="Adjustment on the sprites for the enemies - eyes and mouth" src="https://github.com/user-attachments/assets/d9781b60-c1ff-4a43-9d81-034ecf492d92"/>

(Module 28 submission - Creating a 3D adventure game: Basic Structure - NOTE: The activity began on April 19th and ended on April 21th.).

## May 1st, 2026
### Creating a 3D adventure game: Hero movement

We began developing the hero's movement using the Mechanism and including Idle, Run, and Death animations in the Animator, testing some states. Additionally, a Blend Tree was created for use in the Idle and Run animations.

<img width="250" height="250" alt="image" src="https://github.com/user-attachments/assets/367ccffa-4512-49ea-896d-8eb9eed6c565" /> 

## May 2nd and 3rd, 2026
### Creating a 3D adventure game: "How do we move the 3D character?" and "Integrating animations with movement".

This stage included the movement of the 3D character, both forward and rotation. The code was implemented in the PlayerController.cs script. The character movement control system was implemented using CharacterController. In the PlayerController.cs script, the logic for movement via inputs and gravity system was implemented, in addition to the jumping mechanic (KeyCode.Space). Logic for running was also implemented, increasing the movement speed adjusted to the animation (KeyCode.LeftShift).

<img width="300" height="150" alt="Movie_005" src="https://github.com/user-attachments/assets/a6a87bb2-7b7e-40c0-91de-16a950ac224f" />

(Module 29 submission -  Creating a 3D adventure game: Hero movement - NOTE: The activity began on April 21th and ended on May 3rd.).

## July 11, 2026
### Creating a 3D adventure game: adding weapons

NOTE: The project was put on hold during May and June so I could complete my end-of-semester college exams. Now, in July, I will focus on finishing the project, taking advantage of the break from both college and work.

Today, the base scripts for the character's weapon and its projectiles were created. Code previously developed for the 2D platformer was reused.
Since the timeframe for completing these tasks is short (about 20 days), I will focus on core mechanics to ensure I can complete as many tasks as possible.

## July 14, 2026
### Creating a 3D adventure game: Adding a weapon to the character using the new Input System

A script named "Player Ability Base" was created to handle player abilities without cluttering the main Player class; additionally, the "Player Ability Shoot" script was created to implement the initial shooting ability using Unity's new input system. A new folder named "inputs" was added to the project, containing an input action asset to define the player's input maps; this was used to configure the "shoot" ability to trigger when the X key is pressed.

<img width="280" height="170" alt="Shoot Ability with new Input System" src="https://github.com/user-attachments/assets/ce6dc6ac-5c12-4aa6-a740-5ccf17227b31" />

### Creating a 3D adventure game: Adding Shot Limits

A "Weapon Shoot Limited" script was created to limit the number of shots a weapon can fire, resulting in a new weapon variant for the project. With this script, the player must wait one second before firing a new burst of five consecutive shots; if five shots are fired in a row, the sixth shot is delayed by one second, simulating a reload.

The "Weapon Shoot Limited" script required adjustments to ensure that the weapon fired only when the X key was pressed and that the firing coroutine did not restart while already running. Previously, the game suffered from a bug where the logic allowed multiple shots to fire automatically after the initial three, resulting in unwanted behavior where the weapon fired continuously without player input. With the new checks implemented in `Update()`, the firing mechanism was more precisely controlled, allowing the player to start and stop firing as needed. Additionally, the reload logic was preserved to ensure the weapon could only fire again after the reload time had elapsed.

<img width="280" height="170" alt="Limited Shoot Ability" src="https://github.com/user-attachments/assets/687b45fb-73c4-414d-bedc-29d8e40be50e" />

### Developing different weapons

At this stage, an angled-shot weapon was created, requiring a script named "Weapon Shoot Angle" with variables that allow for the instantiation of up to four shots at specific angles. To make this script work, adjustments were made to the "Weapon Base" script—specifically changing the `shoot` function from `void` to `virtual void`—thereby allowing the function's content to be accessed via an override in the "Weapon Shoot Angle" script. Modifications ensured the shots were instantiated at the correct angle by utilizing `eulerAngles` and multipliers to distinguish between odd- and even-numbered shots.

Regarding the player's shooting ability, modifications were implemented to allow the character to select or switch weapons based on gameplay needs. This involved creating an empty object named "Weapon_Position" within the player hierarchy and adding a new function to spawn the weapon. Consequently, each weapon prefab can carry its own script, eliminating the need to manually swap weapons within the project hierarchy.

<img width="280" height="170" alt="Basic Shoot Ability" src= "https://github.com/user-attachments/assets/58cee292-4093-4868-a767-4c7a8cfddfaa" />
<img width="280" height="170" alt="Limited Shoot Ability" src= "https://github.com/user-attachments/assets/7b2d4ca6-d44d-4c74-b3d9-2b92ac84e73c" />
<img width="280" height="170" alt="Angle Shoot Ability" src= "https://github.com/user-attachments/assets/6b94cf6d-3e30-43f5-87bf-bf13404b1d9d" />

### Integrating Guns with UI for Bullet Reload Hud

A script named "UI Weapon Updater" was created to visually control an interface fill bar, displaying weapon ammunition or reload status; the script locates the image component within the editor to update the reload UI's visuals.

Within the "Weapon Shoot Limited" script (one of the weapons designed with shot limits and reloading mechanics), a list named `UIWeaponUpdaters` was created, and an `UpdateUI()` function was implemented to adjust the HUD image's fill value based on the bullet count; the UI update itself is handled via the `GetAllUis()` function.

DG.Tweening was also utilized in the "UI Weapon Updater" to ensure smoother transitions for the image and reload animations.

A rapid-fire weapon was created—featuring a 0.2-second interval between shots and a 0.2-second reload time—by inheriting from the weapon base script. Scripts and prefabs for both the weapon and the projectile were created, bringing the total number of weapon types to four (Basic, Limited Shot, Angular Shot, and Rapid Fire).

## July 15, 2026
### Creating a 3D adventure game: Weapon Selection System

To better integrate weapon switching using Unity's Input System, I created a new action map within the "inputs" Input Action Asset to handle player weapon selection. By pressing keys 1 through 4, the player can choose between the four created weapons. I modified the *Player Ability Shoots* script to adjust the weapon selection logic and updated all weapon Prefabs.

* Press 1 (Keyboard) to select: Weapon Base (Yellow Bullets)
* Press 2 (Keyboard) to select: Weapon Limited Shoot (Green Bullets)
* Press 3 (Keyboard) to select: Weapon Rapid Fire (Red Bullets)
* Press 4 (Keyboard) to select: Weapon Angle Shoot (Pink Bullets)

<img width="280" height="170" alt="Weapon Selection" src= "https://github.com/user-attachments/assets/dcb3ceef-08ba-43ba-9a59-65f5d9887782" />

(Module 30 submission -  Creating a 3D adventure game: Adding Weapons - NOTE: The activity began on July 11th and ended on July 15th.).

