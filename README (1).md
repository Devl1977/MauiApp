# Samms Guessing Game (.NET MAUI) 🎮

## 🕹️ Project Overview
**Samms Guessing Game** is a memory-matching card game built using **.NET MAUI**. Developed as a final project for class, it showcases cross-platform development, responsive UI design, event-driven gameplay, and audio feedback.

Your goal is simple: **match all the card pairs before time runs out** — the faster, the better your score!

---

## 🎯 Core Features

### ✅ Name Entry System
Players enter their name before starting. It's used to track scores.

### ✅ Difficulty Levels
- **Easy:** 4×4 grid (8 pairs)  
- **Medium:** 5×6 grid (15 pairs)  
- **Hard:** 9×12 grid (27+ pairs)

### ✅ Memory Match Mechanics
- Cards are shuffled randomly each game.
- Click to flip two cards.
- If matched: stay revealed.
- If not: flip back after delay.

### ✅ Audio Integration
Uses `Plugin.Maui.Audio` for immersive sound effects:
- **Match:** `match.mp3`
- **No Match:** `nomatch.mp3`
- **Win:** `win.mp3`
- **Timeout:** `timeout.mp3`

### ✅ High Score System
- Top 5 scores saved to `highscores.json`
- Viewable directly within the app
- Scores are sorted by remaining time

### ✅ Score Reset Button
- Clears all high scores
- Prompts user to confirm before deleting

### ✅ Responsive UI
- Works across platforms and resolutions
- Uses Grid, StackLayout, and ScrollView for layout flexibility

---

## ⚠️ Attempted (but Unfinished) Features

### 🎨 Theme Selection (Feature Shelved)
An optional stretch goal allowed players to pick themes like:
- Cars
- Flowers
- Marvel / DC
- Pokémon
- Playing Cards

Each theme would dynamically load its own card assets from folders like `Resources/Images/<Theme>`. Despite extensive effort (conditional asset loading, path fixes, .csproj edits, rebuilds, etc.), .NET MAUI's limitations with runtime asset resolution caused persistent issues. The feature was ultimately removed to prioritize stability.

> 💭 *“I spent **days** trying to make this work. It became the most frustrating part of the entire project. After all the debugging, nothing felt more important than submitting a functional, polished app.”* – Samm

---

## ✅ Summary of Completed Features

| Feature                         | Status  |
|---------------------------------|---------|
| Card flip + match logic         | ✅       |
| Difficulty selection (Easy–Hard)| ✅       |
| Game timer                      | ✅       |
| Sounds (match, miss, win, timeout) | ✅    |
| Save + view top 5 scores        | ✅       |
| Reset all high scores           | ✅       |
| Responsive layout               | ✅       |
| Theme system                    | ❌ (shelved) |

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

This project reflects personal test of patience, persistence, and creativity along with **perseverance**, **problem-solving**, and a passion to keep learning. .NET MAUI posed many challenges, especially with asset loading and platform-specific behavior, but I pushed through and delivered a working app that I’m proud of.

I hope you enjoy playing it as much as I learned building it!

– **Sammuel Joseph**
