# Household Chaos and Chores

A Unity 2D game prototype built around a loop of survival combat, chore breaks, and progression. The project blends fast-paced survival gameplay with a short dish-washing minigame, where the player alternates between fighting incoming threats and completing household tasks.

## Overview

This project is organized as a simple modular Unity codebase with clear responsibilities:

- `Assets/Game System` handles the core game loop, progression, timers, and flow control.
- `Assets/Game Object` contains the player, enemies/attack objects, and the character state system.
- `Assets/Common` contains reusable systems such as audio, saving, camera, and settings.
- `Assets/UI` contains menu screens, HUD, win/lose flows, and UI animation handling.
- `Assets/Scenes` contains the main menu and gameplay scenes.

## Key Features

### 1. Survival Phase
The main gameplay loop is a survival section where the player is active and must avoid or defeat incoming attack objects. The project uses:

- a `Player` controller with state-based behavior
- an `AttackObjectSpawner` that creates random projectiles/objects over time
- a `GameTimer` for overall session timing
- a `ProgressionController` that adjusts spawn behavior as the game advances

### 2. Chore Break Minigame
At set intervals, the game pauses the survival phase and launches a dish-washing mini-game:

- the player is temporarily disabled in survival mode
- a chore-break UI sequence plays
- `DishWashingController` spawns plates, tracks remaining dirt, and calculates a result
- the minigame ends with a speed effect based on how clean the plates were

This creates a rhythm of survival -> chore break -> tougher survival loop.

### 3. Difficulty and Progression
The game supports multiple difficulty modes through `ProgressionController`:

- Easy
- Normal
- Hard

Each difficulty selects a different `ProgressionData` asset, which controls:

- total game time
- chore break timing range
- spawn timing ranges for each progression level

### 4. Audio and UI Flow
The project includes a centralized audio system and animated UI transitions:

- `AudioManager` manages music and SFX, including fade transitions
- `UIAnimationController` triggers opening, chore break, win, and lose sequences
- `MainMenuController`, `PauseScreenController`, and `GameoverScreenController` drive the menu and screen flow

### 5. Save Data
The project includes a simple persistent save system:

- `GameSaveHandler` writes and loads `GameData`
- current save data tracks whether a run exists and whether the player was defeated

## Main Modules and Components

### Game System

#### `GameManager`
The central coordinator of the project. It:

- connects the systems together
- starts the opening sequence
- switches between survival and chore phases
- ends the game on win or loss
- controls pause behavior and game-wide state

#### `GameTimer`
Handles the main time-based state transitions:

- countdown timer for the full session
- random chore break timer
- start, pause, and resume behavior

#### `ProgressionController`
Controls progression scaling:

- picks difficulty data
- advances through spawn configurations after each chore minigame
- updates object spawn timing dynamically

#### `ProgressionData`
A ScriptableObject container for progression settings such as:

- game duration
- chore interval range
- spawn timing data per level

### Game Objects

#### `Player`
The main character controller. It contains:

- input-driven combat checks
- state changes via `StateController`
- health/damage handling
- player buff effects and sprite animation callbacks

#### `StateController`
A lightweight state machine that enables simple state-driven behavior for the player. It maps state names to concrete state components and switches between them cleanly.

#### `AttackObjectSpawner`
Spawns randomized attack objects from a configured prefab list and controls the spawn timer.

#### `BaseAttackObject`
Base class for spawned attack objects. Concrete implementations such as `Hammer`, `Knife`, and `Rock` extend this class.

### Chores System

#### `DishWashingController`
Responsible for running the dish-washing minigame:

- spawns plates
- tracks completed plates
- calculates remaining dirt
- shows a result screen
- ends the minigame and returns control to the main game loop

#### `DishWashingAnimation`
Handles visual sequencing for the minigame UI and plate transitions.

#### `DishWashingResult`
Displays the final cleanliness result and applies the resulting gameplay effect.

### Common Systems

#### `AudioManager`
Manages music and SFX through dedicated audio sources and mixer parameters.

#### `GameSaveHandler`
Simple JSON-based save utility used to persist game data.

#### `CameraFollow`
Provides basic camera follow behavior for the player.

#### `ParallaxController` / `ParallaxLayer`
Used for background movement and layered parallax effects.

### UI

#### `MainMenuController`
Handles the menu screens, difficulty selection, and scene transitions.

#### `PauseScreenController`
Manages pause screen open/close behavior.

#### `GameoverScreenController`
Handles win and lose screen display.

#### `UIAnimationController`
Coordinates UI animations for intro, chore breaks, and ending states.

## Gameplay Flow

### Startup Flow
1. The player opens the main menu.
2. The player selects a difficulty.
3. `MainMenuController` loads the `GameScene`.
4. `GameManager` runs its start logic and asserts required references.
5. The opening sequence UI plays.
6. The survival phase begins, music starts, the timer starts, and spawners activate.

### Main Game Loop
1. `GameTimer` counts down the primary survival timer.
2. `AttackObjectSpawner` repeatedly instantiates attack objects.
3. The player can fight, dodge, or take damage.
4. When the chores timer reaches zero, the game switches to the chore phase.

### Chore Break Flow
1. `GameManager.SwitchToChoresPhase()` disables the player and pauses other survival systems.
2. `UIAnimationController.DoChoreBreak()` plays the chore transition animation.
3. `DishWashingController.StartMinigame()` displays the dish-washing UI.
4. The player cleans plates and the result is calculated.
5. `DishWashingController` ends the minigame and `GameManager` resumes survival gameplay.

### Progression Flow
1. After a chore minigame, `ProgressionController.NextProgressionLevel()` advances the current progression level.
2. Spawn timing becomes more difficult for the next survival phase.
3. The loop continues until the session ends.

### End Game Flow
- If the player loses, `Player.ReceiveDamage()` triggers `OnPlayerDied`, and `GameManager.EndGame(false)` opens the lose screen.
- If the main timer ends, `GameManager.EndGame(true)` opens the win screen and saves the result.

## Scene Structure

### `MainMenuScene`
Used for:

- difficulty selection
- settings view
- credits view
- quit handling

### `GameScene`
Contains the active gameplay scene, including:

- the player
- spawner systems
- timer/UI elements
- chore minigame content
- UI animation controller

## Suggested Project Structure

```text
Beyond Game Jam/
├─ Assets/
│  ├─ Common/
│  ├─ Game Object/
│  ├─ Game System/
│  ├─ Scenes/
│  ├─ UI/
│  └─ ...
├─ Packages/
├─ ProjectSettings/
├─ Build/
├─ Library/
├─ Logs/
├─ Temp/
├─ UserSettings/
├─ Assembly-CSharp.csproj
├─ Beyond Game Jam.slnx
├─ README.md
└─ .gitignore
```

## Notes for Contributors

- Most major systems are already connected through serialized references in `GameManager`.
- The project uses Unity’s built-in animation and UI systems, plus DOTween for tweening and the Input System for player input.
- `ProgressionData` assets are the main place to tune difficulty balance and pacing.
- `DebugData` can be used to quickly toggle debug behavior during development.

## License

This project currently does not include a license file. If you intend to publish or share it publicly, consider adding a license such as MIT or GPL before releasing the repository.

## Quick Start

1. Open the project in Unity.
2. Load `MainMenuScene` to start from the menu.
3. Select a difficulty and begin the run.
4. Iterate on spawn timings, chore timing, and balance values via `ProgressionData` and `GameManager` references.

## Useful Files to Explore First

- `Assets/Game System/Manager/GameManager.cs`
- `Assets/Game System/Progression/ProgressionController.cs`
- `Assets/Game System/Chores/Script/DishWashingController.cs`
- `Assets/Game Object/Character/Player/Player.cs`
- `Assets/Common/Audio Manager/AudioManager.cs`
- `Assets/UI/Main Menu Scene/MainMenuController.cs`
