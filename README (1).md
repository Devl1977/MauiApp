
# Samms Guessing Game (.NET MAUI)

## 🕹️ Project Overview

**Samms Guessing Game** is a memory-matching card game built using **.NET MAUI**. It was developed as part of a final class project to demonstrate skills in cross-platform development, UI design, event-driven logic, and audio integration.

The goal of the game is simple: flip cards to find matching pairs within a countdown timer. The faster you find them, the better your score.

---

## 🎯 Core Features

✅ **Name Entry System**  
Players enter their name before beginning the game. This name is used to track and save their score.

✅ **Difficulty Selection**  
Three levels of difficulty are supported:  
- **Easy**: 4x4 grid (8 pairs)  
- **Medium**: 5x6 grid (15 pairs)  
- **Hard**: 9x12 grid (27 pairs or more)

✅ **Memory Matching Mechanics**  
- Cards are hidden and flipped on click.
- Two flipped cards are compared.
  - If matched: they stay visible.
  - If not: they flip back.
- Cards are randomly shuffled for each game.

✅ **Sound Integration**  
Each game includes interactive sound effects using the **Plugin.Maui.Audio** package:
- Matching: 🎵 `match.mp3`
- Not matching: 🔕 `nomatch.mp3`
- Win: 🏆 `win.mp3`
- Timeout: ⏰ `timeout.mp3`

✅ **Score Saving**  
Top 5 scores are saved to a local JSON file and can be viewed through the app interface.

✅ **Reset Score Functionality**  
Users can clear all scores with one button (with confirmation prompt).

✅ **Responsive UI**  
The layout is adaptive, using `Grid`, `StackLayout`, and `ScrollView` to support varying screen sizes.

---

## ⚠️ Attempted (but not completed) Features

### 🎨 Theme Selection (Unfinished)
A feature was under development to allow players to choose from themes such as:
- Cars
- Flowers
- DC / Marvel
- Pokémon
- Playing Cards

Each theme would load a different set of card images from its respective folder in the project (`Resources/Images/<Theme>`). However, persistent issues with image loading via dynamic folders in .NET MAUI prevented this from working reliably across builds and platforms.

After hours of refactoring, restructuring asset paths, updating the `.csproj`, and verifying resource configurations, the implementation continued to fail. This feature was ultimately **shelved** to preserve the core game's stability and functionality.

---

## 😤 Developer Reflections

> "This feature *should* have been easy, but instead turned into the most frustrating part of the project. I tried everything I could — conditional logic, asset debugging, path rewriting, and even full project rebuilds. In the end, I made the decision to focus on delivering a polished and working game with consistent core functionality instead of risking the entire project for one bonus feature."

The theme system **was never part of the original grading requirements** — just a personal stretch goal. The rest of the project meets or exceeds all of the expected criteria.

---

## ✅ What Works

| Feature                  | Status  |
|--------------------------|---------|
| Card flipping + match detection | ✅ |
| Dynamic grid generation by difficulty | ✅ |
| Sounds and audio feedback | ✅ |
| Score saving + display | ✅ |
| Reset functionality | ✅ |
| Polished and functional UI | ✅ |
| Theming (bonus feature) | ❌ (not working) |

---

## 📁 Project Structure

```
SammsGuessingGameFinal/
├── Resources/
│   ├── Images/
│   │   └── [Card images used for gameplay]
│   ├── Sounds/
│   │   ├── match.mp3
│   │   ├── nomatch.mp3
│   │   ├── win.mp3
│   │   └── timeout.mp3
├── MainPage.xaml
├── MainPage.xaml.cs
├── MauiProgram.cs
├── Models/
│   └── Card.cs
├── highscores.json
└── README.md
```

---

## 💭 Final Thoughts

This project reflects **perseverance**, **problem-solving**, and a passion to keep learning. .NET MAUI posed many challenges, especially with asset loading and platform-specific behavior, but I pushed through and delivered a working app that I’m proud of.

I hope you enjoy playing it as much as I learned building it!

– **Sammuel Joseph**
