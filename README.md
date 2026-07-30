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

## July 11th, 2026
### Creating a 3D adventure game: adding weapons

NOTE: The project was put on hold during May and June so I could complete my end-of-semester college exams. Now, in July, I will focus on finishing the project, taking advantage of the break from both college and work.

Today, the base scripts for the character's weapon and its projectiles were created. Code previously developed for the 2D platformer was reused.
Since the timeframe for completing these tasks is short (about 20 days), I will focus on core mechanics to ensure I can complete as many tasks as possible.

## July 14th, 2026
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

## July 15th, 2026
### Creating a 3D adventure game: Weapon Selection System

To better integrate weapon switching using Unity's Input System, I created a new action map within the "inputs" Input Action Asset to handle player weapon selection. By pressing keys 1 through 4, the player can choose between the four created weapons. I modified the *Player Ability Shoots* script to adjust the weapon selection logic and updated all weapon Prefabs.

* Press 1 (Keyboard) to select: Weapon Base (Yellow Bullets)
* Press 2 (Keyboard) to select: Weapon Limited Shoot (Green Bullets)
* Press 3 (Keyboard) to select: Weapon Rapid Fire (Red Bullets)
* Press 4 (Keyboard) to select: Weapon Angle Shoot (Pink Bullets)

<img width="280" height="170" alt="Weapon Selection" src= "https://github.com/user-attachments/assets/dcb3ceef-08ba-43ba-9a59-65f5d9887782" />

(Module 30 submission -  Creating a 3D adventure game: Adding Weapons - NOTE: The activity began on July 11th and ended on July 15th.).

## July 16th, 2026
### Creating a 3D adventure game: Adding Enemies

### Basic enemy structure

An "Enemy Base" script featuring health and damage systems was created. It implements health initialization, a method to take damage that reduces current health, and automatic GameObject destruction when health reaches zero. It also includes a test function using the 'K' key to deal 5 damage. The astronaut prefab variant has also been updated.

### NOTE – duplicate scenes and weapon prefabs have been removed from the repository. The correct files have been placed in the folders.

### Integrating 3D enemy

The Enemy Base script was also modified to include enemy spawn animation values ​​using the DG.Tweening library. Containers were created for the enemies to hold the Enemy Base script and box colliders, which were adjusted to fit each character's size.

### Integrating animation

During this stage, enemy animations were implemented. Logic for enemy spawn animations using DOTween was added to the `EnemyBase` class, which manages enemy health, damage, and death. A newly created `AnimationBase` class controls animation triggers (Idle, Run, Death, Attack) via a list configurable in the Inspector. I modified and adjusted all enemy prefabs by adding box colliders and creating a graphics container to allow for scaling the monsters; they now come in three different sizes. The animation system allows for testing enemy damage and death by pressing the 'K' key, which automatically triggers the death animation before destroying the object.

<img width="280" height="170" alt="Enemies-IDLE animation" src= "https://github.com/user-attachments/assets/03baf273-cefa-4322-87de-49b4c606cf5c" />
<img width="280" height="170" alt="Enemies-RUN animation" src= "https://github.com/user-attachments/assets/5b54d4bb-6c72-4a3c-8a7e-a152d4a5b7f2" />

__

<img width="280" height="170" alt="Enemies-DEATH animation" src= "https://github.com/user-attachments/assets/491afb88-bd34-470c-997a-6caddeb401f3" />
<img width="280" height="170" alt="Enemies-ATTACK animation" src= "https://github.com/user-attachments/assets/2e0e7ac1-ab45-40eb-88bb-1a2647b75116" />

## July 17th, 2026
### Creating a 3D adventure game: Causing the enemy to be affected by the weapon

At this stage, changes were made to the enemy base script so that enemies could take damage upon colliding with a projectile. It was also possible to create an interface called `IDamageable` to manage the damage dealt to the enemies.

### Adding Particles
For the visual effects stage regarding hit feedback on characters, a folder named "Visual Effects (VFX)" was created, containing a "Flash Color" script. This script allows enemies to flash after being struck by a projectile. Additionally, modifications were made to the "Enemy Base" to trigger this feedback, and particle effects were added to each of the monster prefabs.

Each monster was assigned a particle system configured with its own VFX material, featuring a color similar to the materials used for the characters themselves. The URP/Lit shader was selected, with adjustments made to surface properties, color, and emission.

Particle settings were also tweaked to simulate chunks of the slimes being propelled outward by the impact of the bullets.

<img width="280" height="170" alt="VFX animations and damage feedbacks" src= "https://github.com/user-attachments/assets/41b217ab-274f-4356-a28d-b258b5028566" />

(Module 31 submission -  Creating a 3D adventure game: Adding Weapons - NOTE: The activity began on July 16th and ended on July 17th.).

## July 18th, 2026
### Creating a 3D adventure game: Adding different types of enemies

Today, a script named "Enemy Walk" was created, representing an enemy type that inherits basic information from the base enemy. This "walk" type enemy can move along defined points.

A patrol system was implemented in the "Enemy Walk" script for enemies moving between specific waypoints. The enemy automatically walks from one point to another using `Vector3.MoveTowards`; upon reaching the vicinity of a waypoint (within a configurable minimum distance), it proceeds to the next one. The system operates in a continuous loop, returning to the first waypoint after completing the path

### How to make an enemy faster and slower

At this stage, adjustments were made to the waypoints and enemy prefabs so that the enemies would face the direction of the waypoints while moving across the terrain. Modifications were also made to the projectiles by accessing their corresponding script and adding logic that caused the enemy to be pushed backward upon impact.

## July 19th, 2026
### Adding a weapon to the enemy

In this stage, we are creating an enemy capable of shooting and implementing a system that deals damage to the player upon contact with enemies.
To achieve this, we implemented logic in the base enemy script so that enemies deal damage when the player touches them. We also adjusted the damage handling for the main character and set up color-flash feedback—triggered by Tweens—that works on both the character and the enemies using `MeshRenderer` and `SkinnedMeshRenderer` components within the `flash()` function. Since the player prefab consists of multiple parts and meshes, we selected the helmet and body to receive the color-flash scripts. We then assigned the relevant elements to the color-flash section of the player controller. As a result, the player now flashes (dark blue) upon contact with enemies, while enemies take damage and flash when hit by projectiles.

<img width="280" height="170" alt="Color-flash effect - damage" src= "https://github.com/user-attachments/assets/109b00ca-17ba-4601-91ad-243d4e258d97"/>

## July 20th, 2026
### Adding a weapon to the enemy

A minor adjustment was made to Slime Monster 2 to include a weapon. The enemy's weapon features its own projectile and a dedicated material. The base enemy script underwent slight modifications to incorporate a shooting system; specifically, a single line was added to the `BornAnimation()` method within the `Init()` function. A new script named "Enemy Shoots" was created to enable enemies to fire. Work also began on modifying the base projectile collision logic to include enemy and player tags, allowing the system to distinguish between shots so that enemies do not hit themselves—ensuring interactions occur only between player and enemy.

<img width="280" height="170" alt="Green Slime Enemy and Pink Slime Enemy" src= "https://github.com/user-attachments/assets/3c7937b1-0645-466e-ada8-dab95f4ecf64"/>

## July 21th, 2026
### Developing the Boss - Part 01

Today, the Boss state machine was implemented; it is structured within the `Boss` folder inside the `Scripts` folder. This folder contains the main scripts managing the Boss's behavioral logic in the game.

The `BossAction` enumeration (Enums) defines the Boss's possible states: `INIT`, `IDLE`, `WALK`, and `ATTACK`. The `BossBase` class is responsible for initializing the state machine using the `StateMachine<BossAction>` class.

In the `Init()` method, the state machine is configured, and the `SwitchState(BossAction state)` method allows for changing the current state by passing a reference to the `this` object, which represents the Boss instance. The `BossStateBase` class, which inherits from `StateBase`, contains a protected attribute named `boss` that stores the reference to `BossBase`.

The `OnStateEnter(params object[] objs)` method is overridden to capture this reference from the passed array of objects. Within this method, the call to `base.OnStateEnter(objs)` ensures the base class's default logic executes, while the line `boss = (BossBase)objs[0]` allows access to the Boss instance.

The `BossStateInit` class represents the Boss's initial state and inherits from `BossStateBase`; it can be expanded to include logic specific to this initial state. Meanwhile, the `StateBase` class provides virtual methods—such as `OnStateEnter`, `OnStateStay`, and `OnStateExit`—that can be overridden in derived classes, allowing each state to define its own behavior. Finally, the `StateMachine` class manages state transitions, storing registered states in a dictionary named `dictionaryStates`.

The `SwitchState(T state, params object[] objs)` method is crucial for state switching; when called, it checks for a current state and invokes `OnStateExit()` before switching to the new state and executing `OnStateEnter(objs)` with the additional parameters. This modular and flexible state machine implementation simplifies the Boss's logic, allowing new states to be added in a simple and organized manner.

## July 22th and 23th, 2026
### Developing the Boss - Part 01 and Part 02

To verify functionality, a `public override void OnStateEnter(params object[] objs){base.OnStateEnter(objs);}` method was added to `public class BossStateInit : BossStateBase`, followed by a debug line—`Debug.Log("Boss: "+ boss)`—to attempt to access the boss at this stage. Testing confirmed that the boss reference is successfully retrieved; consequently, the process of setting up the animation logic began, resulting in the creation of the function `public void StartInitiAnimation() {transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();}` to trigger the boss's animations. With the animations working, the boss needed to be able to move using a waypoint system. To achieve this, a coroutine was implemented to direct the boss to random points; utilizing these waypoints, the boss is now able to move randomly in various directions.

### Part 02

I began by creating the boss's attack state. To do this, I added attack states to both `BossStates` and `BossBase`. In `BossBase`, I also included an "Attack" header to organize the variables needed for the state's functionality: `attackAmount` (to determine how many attacks the boss performs before switching states) and `timeBetweenAttacks` (to set the interval between attacks until the sequence is complete). In the `BossBase` code, I created a new region named "ATTACK" containing a public function, `StartAttack()`, linked to a coroutine. In addition to the control variables in the "Attack" header, I included local attack variables within the coroutine, initialized to zero; a `while` loop was used to increment the attack count by 1 as long as the current count remained below the `attackAmount`. Inside the attack coroutine, I used a debug visual indicator—specifically, a transform scale variation using `transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo)`. Once set up, I added the `StartCoroutine` call to the new `StartAttack()` function and invoked this function within the `OnStateEnter` method of the `BossStateAttack` class (in the `BossStates` script), passing the parameters relevant to the attack state.

After this step, we could see the boss moving and attacking by switching states using NaughtyAttributes buttons. Next, we needed to add callbacks to enable the boss to walk and shoot across the scene. By including callbacks such as `onArrive`, the boss triggers an attack after reaching a waypoint, demonstrating that the logic works. Subsequently, I added "End Callbacks" to the `BossBase` script as arguments—specifically `Action endCallbacks`—within the `StartAttack()` and `AttackCoroutine()` functions. This implementation allows the `EndAttacks` function to be called from the `BossStateAttack` class within the `BossStates` script. Inside the `EndAttacks` function, the boss returns to the `Walk` state after firing—using `boss.SwitchState(BossAction.WALK);`—thereby closing the cycle.

<img width="280" height="170" alt="Green Slime Enemy and Pink Slime Enemy" src= "https://github.com/user-attachments/assets/380c9612-f1e6-41cf-911a-9163fc9c20cf"/>

## July 24th, 2026
### 
Creating a 3D Adventure Game: Hero's Life - Health Base

Inclusion of death state logic and health data. In the `BossState` script, the states were updated to include the death state, and debug logs for state entry and exit were added. In `StateBase`, the debug logs were simply commented out. A new folder named "Health" was created to house the character health scripts. The `HealthBase` script was created to handle the boss's health logic.

(Module 31 submission - Creating a 3D adventure game: Adding Weapons - NOTE: The activity began on July 18th and ended on July 23th.).


## July 26th, 2026
### Creating a 3D Adventure Game: Hero's Life - Implementing Hero Death

Player death functionality was implemented today, eliminating the need to call "Destroy On Kill" via the Inspector. To achieve this, the `OnKill(HealthBase h)` function was added to the `Awake()` method; this function uses an internal conditional check so that, if the character is alive and takes damage, they enter the death state, trigger the death animation in the Animator, and disable their colliders to prevent shots from continuing to hit the character's body.

## July 28th, 2026
### Creating a 3D Adventure Game: Adding a life UI to the hero and Adding Checkpoints to the Game

In this stage, the character's health bar was created to respond to hits and physical damage. Also during this stage, the Checkpoint assets were imported into the project. A script named `CheckPointBase.cs` was created to implement the checkpoint logic; when the player touches the collider trigger on the totem, the emission texture lights up, indicating the checkpoint has been reached. The player's new position is set to the checkpoint's location using `PlayerPrefs`. A new function—based on integer "Key" variables—was created to identify which checkpoint is being activated via `PlayerPrefs` based on its specific key number, always prioritizing the checkpoint with the highest value.

<img width="280" height="170" alt="Checkpoints and Health Bar" src= "https://github.com/user-attachments/assets/c5457247-17ba-406e-8468-a234296c4d85"/>

### Respawning at checkpoints

While implementing the checkpoint system, a bug was observed where the player would die and respawn but stop taking damage, and would always respawn at the same location. The issue where the character stopped taking damage after respawning was fixed: the `ResetLife()` method reset health to zero but failed to reset the `IsDead` flag, causing the `Damage()` method to exit early and never trigger the `FlashColor` effect. The incorrect respawn location issue was also resolved: since the character uses a `CharacterController`, directly modifying `transform.position` was being ignored or reverted by the component; the solution was to disable the `CharacterController` before moving the Transform and re-enable it afterward, allowing the repositioning at the checkpoint to take effect correctly.

### Creating a UI for the checkpoint


A user interface (UI) was created to display a visual message whenever the character reached a checkpoint, providing clear feedback to the player that their progress had been saved. This UI was implemented independently of the `CheckPointManager`; it triggers the moment the checkpoint is activated and automatically disappears after a set duration.

It was determined that the `NullReferenceException` in `CheckPointBase.SaveCheckPoint` occurred because `CheckPointManager.Instance` had not yet been initialized when it was accessed. The fix ensured that the `CheckPointManager` Singleton was created and made available before the `PlayerController`'s `Awake()` method ran, thereby resolving the error. Following this adjustment, the checkpoint system functioned correctly: the UI appears upon touching the totem and vanishes after the configured time, while the respawn mechanism places the player at the last saved checkpoint. The TextMesh Pro package was also imported.

(Module 31 submission - Creating a 3D adventure game: Adding Weapons - NOTE: The activity began on July 24th and ended on July 28th.).

## July 29th, 2026
### Creating a 3D Adventure Game: Post-Production and Cameras

In this stage, the Post-Processing package was downloaded for the project via Unity's Package Manager to handle the game's post-processing. It is applied to the camera; however, since using it can slow down the game, care must be taken with its implementation. When using layer mode in the Main Camera's Inspector, it is important to create a layer named "PostProcessing" so that the processing affects only that specific layer. After setting this up, we create a GameObject named "PostProcessing" and add the Post-Process Volume component to it. This component functions based on a profile; you simply create the settings and click "New Profile," allowing you to select profiles or use the Inspector menu to create effects.

However, this specific workflow is no longer supported in the Unity version I am using (Unity 6.3 LTS / 6000.3.13f1). Consequently, instead of using layers, I added the Volume component to the Post-Processing object and created a new profile containing a list of components that were previously part of the old "Color Grading" feature:

* Color Adjustments (Post-processing) — for general adjustments to exposure, contrast, saturation, color temperature, and color filters. Shadows, Midtones, Highlights — for tonal range grading.

* White Balance — included within Color Adjustments (temperature/tint).

* Channel Mixer, Lift Gamma Gain, Split Toning, Curves — these also appear as separate overrides in URP, corresponding to the sections previously found within the single "Color Grading" override in PPv2. In PPv2, all of these were grouped under one override called "Color Grading."

In URP, these tools have been split into individual overrides. If you like, send me the list or a screenshot, and I can point out exactly which ones to click.

### Changing Post-Processing settings in real-time

To achieve this, a new folder named "Effects" was created within the "Scripts" folder. Inside it, a new script named "EffectsManager" was added to control the post-processing effects. The namespace `UnityEngine.Rendering.PostProcessing` was included in this script. For this script, I will use a `PostProcessVolume` as a public variable to access post-processing components and apply certain effects to the game. The first of these is a vignette effect that flashes red whenever the hero takes damage.

<img width="280" height="170" alt="Effects Post processing - vignette" src= "https://github.com/user-attachments/assets/861dd590-3a2d-4d14-a140-641f8884fb02"/>

## July 30th, 2026
### Creating a 3D Adventure Game: Adding Cinemachine

For this step, the State-Driven Camera was chosen, allowing us to configure multiple cameras based on a specific parameter, such as the Animator. Since our character has various states—such as Idle, Run and Death — we can include different cameras for each of them.

<img width="280" height="170" alt="IDLE Camera" src="https://github.com/user-attachments/assets/61eccea9-70f6-4f7a-8f7e-826e7d132d18" />
<img width="280" height="170" alt="RUN Camera" src="https://github.com/user-attachments/assets/b4637e7d-b862-4aaa-913b-30310b438624"/>
<img width="280" height="170" alt="DEATH Camera" src="https://github.com/user-attachments/assets/f3869cab-5b4b-44a4-ba4c-aab053870a41" />




