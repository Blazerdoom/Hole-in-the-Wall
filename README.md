# Hole in the Wall

![Hole in the Wall](docs/gifs/g1-shoved.gif)

## About the game

Walls with a hole slide toward you on a narrow platform. Move left and right, or jump, to get through the hole.

If you hit a wall, you don't die. The wall pushes you back and you lose ground. You only lose when you fall off the platform. Your score is how long you survive.

## Project info

| | |
|:-|:-|
| **Engine** | Unity 6 (6000.6.0f1), URP, 3D |
| **Role** | Solo: game idea and programming |
| **Development time** | 8 days (16–23 September 2026) |
| **Controls** | A / D or arrow keys to move, Space to jump, R to restart |

## My contribution

- I came up with the game idea and wrote all the gameplay code (the 6 scripts below).
- I used an AI assistant (Claude) as a coach while learning 3D Unity. It explained things, reviewed my code and set up some scene settings for me (UI layout, fog and colors).

## Key features

- Every wall gets a random hole. A wall is a 5×5 grid of blocks, and a script hides 2 blocks (a low hole) or 3 blocks (a tall hole you have to jump into).
- Hitting a wall pushes you back instead of ending the game.
- A new wall comes every 2 seconds. Walls remove themselves after they pass you.
- Camera shake when a wall hits you, and dust when you jump and land.
- A time score, a game over screen, and R to restart.

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

## Scripts

| Script | What it does |
|:-|:-|
| `PlayerController.cs` | Moving, jumping, checking if you fell off, reacting to walls |
| `WallSpawner.cs` | Spawns a new wall every 2 seconds |
| `ObsticleMove.cs` | Moves a wall toward the player and removes it after it passes |
| `HoleMaker.cs` | Makes a random hole when a wall spawns |
| `GameManager.cs` | Time score, game over and restart |
| `CameraShake.cs` | Shakes the camera for a moment |

Each script does one job. If I kept working on it, I would stop the player from calling the GameManager directly and use events instead.

## How to open it

1. Open the project with Unity **Unity 6** (6000.6.0f1).
2. Open `Assets/Scenes/main game.unity` and press Play.

## Credits

- Font: Rubik (SIL Open Font License, the license file is in `Assets/Fonts`).
