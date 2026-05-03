# Adventure_Game_3D
 3D adventure game project containing exercises related to modules 27 through 40 of the EBAC Unity developer course.

## April 19th, 2026
### Creating a 3D adventure game: Basic Structure

A 3D adventure game project has been initiated, containing EBAC art packages such as prefabs for scenarios, enemies, and the player, as well as animations and state machines. Other important plugins were included in the project, such as DoTween, NaughtyAttributes, Cinemachine, and Recording.

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

(Module 27 submission - Creating a 3D adventure game: Basic Structure - NOTE: The activity began on April 19th and ended on April 21th.).

## May 01st, 2026
### Creating a 3D adventure game: Hero movement

We began developing the hero's movement using the Mechanism and including Idle, Run, and Death animations in the Animator, testing some states. Additionally, a Blend Tree was created for use in the Idle and Run animations.

<img width="250" height="250" alt="image" src="https://github.com/user-attachments/assets/367ccffa-4512-49ea-896d-8eb9eed6c565" /> 

## May 02nd and 3rd, 2026
### Creating a 3D adventure game: "How do we move the 3D character?" and "Integrating animations with movement".

This stage included the movement of the 3D character, both forward and rotation. The code was implemented in the PlayerController.cs script. The character movement control system was implemented, creating the CharacterController script. It includes the logic for movement via inputs and a gravity system, as well as the jumping mechanic (KeyCode.Space). Logic for running was also implemented, increasing the speed of movement and adjusting the animation (KeyCode.LeftShift).

<img width="300" height="150" alt="Movie_005" src="https://github.com/user-attachments/assets/a6a87bb2-7b7e-40c0-91de-16a950ac224f" />


