# Hole in the Wall

![Hole in the Wall](docs/gifs/g1-shoved.gif)

## About the game

A 3D dodge game made in Unity 6. Walls with a hole in them slide toward you on a narrow platform. Move left and right, or jump, to get through the hole.

Hitting a wall doesn't kill you. It pushes you back and you lose ground, and you only lose when you fall off the platform. Your score is how long you survive.

- **Controls:** A / D or arrow keys to move, Space to jump, R to restart
- **Engine:** Unity 6 (6000.6.0f1), URP
- **Time:** 8 days (16 to 23 September 2026), solo

## Gameplay

<table>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g1-walk-through.gif" width="100%" alt="Walking through a low hole"><br>Walking through a low hole</td>
    <td width="50%" valign="top"><img src="docs/gifs/g1-jump-through.gif" width="100%" alt="Jumping into a tall hole"><br>Jumping into a tall hole</td>
  </tr>
  <tr>
    <td width="50%" valign="top"><img src="docs/gifs/g1-shoved.gif" width="100%" alt="Missing the hole: the wall pushes you back"><br>Missing the hole: the wall pushes you back</td>
    <td width="50%" valign="top"><img src="docs/gifs/g1-game-over.gif" width="100%" alt="Falling off, game over, R to restart"><br>Falling off, game over, R to restart</td>
  </tr>
</table>

## Features

- Every wall is a 5 x 5 grid of blocks. When a wall spawns, a script picks a random column and hides 2 blocks (a low hole you can walk through) or 3 blocks (a tall hole you have to jump into).
- A wall that hits you pushes you back instead of ending the game.
- A new wall comes every 2 seconds, and walls delete themselves once they're behind you.
- Camera shake when a wall hits you, and dust when you jump and land.
- A time score, a game over screen, and R to restart.

## My contribution

- I came up with the idea and wrote all 6 gameplay scripts.
- This was my first 3D game in Unity, so I used Claude (an AI assistant) as a tutor. It explained things, reviewed my code and set up some scene settings for me (UI layout, fog and colors).

## Architecture

| Script | What it does |
|:-|:-|
| `PlayerController.cs` | Moving, jumping, checking if you fell off, reacting to walls |
| `WallSpawner.cs` | Spawns a new wall every 2 seconds with `InvokeRepeating` |
| `ObsticleMove.cs` | On every wall: moves it toward the player and deletes it after it passes |
| `HoleMaker.cs` | On every wall: makes a random hole when the wall spawns |
| `GameManager.cs` | Time score, game over and restart |
| `CameraShake.cs` | Shakes the camera for a moment |

How the scripts connect:

- `GameManager` and `CameraShake` are singletons (`GameManager.instance`), so the player can end the game or shake the camera without needing a reference.
- The wall prefab carries `ObsticleMove` and `HoleMaker`, so every wall moves itself, makes its own hole and deletes itself. `WallSpawner` only creates walls and never has to keep track of them.
- The player only knows about walls through the `Obsticle` tag.

What I'd change: the player calls `GameManager.instance.gameOver()` directly. With an event, the player wouldn't need to know the GameManager exists. `HoleMaker` also finds blocks by their names ("1" to "25"), so renaming a block would break it. An array set in the Inspector would be safer.

## How to open it

1. Open the project with Unity 6 (6000.6.0f1).
2. Open `Assets/Scenes/main game.unity` and press Play.

## Credits

- Font: Rubik (SIL Open Font License, the license file is in `Assets/Fonts`).
