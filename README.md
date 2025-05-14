# Still Standing

**Still Standing** is a 3D top-down survivor game with MOBA mechanics, built in Unity. Players control a champion with unique abilities and must survive waves of enemies, leveraging item builds, tactical movement, and skillful ability usage. 
Basically, Vampire Survivor + Dota.

---

## 🎮 Game Overview

- **Genre:** Top-down Action / Survival / MOBA-inspired
- **Engine:** Unity
- **Perspective:** 3D Top-Down
- **Core Loop:** Survive increasingly difficult enemy waves, upgrade your character, and dominate the battlefield.

---

## 🧠 Core Features

### 🧩 Modular Ability System
- Supports directional, homing, and AoE abilities
- Passive and active skill logic separation
- Delegate-driven damage modifiers (Pre/Post hooks)

### ⚔️ Combat System
- Auto-attacks with interruption resilience
- Stackable debuffs and crowd control effects
- True damage, armor/magic resist interactions

### 🧍 Champion Design
- 4 active abilities + 1 passive
- Status effects, scalings, and combo synergy
- Expandable using ScriptableObjects and modular components

### 🧠 Enemy & NPC AI
- Modular `CharacterBrain` system
- Enemy behavior states: Chase, Wander, Wait
- Custom behaviors like "David" (chase player briefly, then idle)

### 🧪 Item System
- Random item generation system in progress
- Items apply `StatModifier` components
- Scales with level, rarity, and effect type

---

## 🛠 Architecture & Principles

- **SOLID Principles** throughout the codebase
- **Strategy** Pattern used for Enemy/NPC, both are Characters, Behavior.
- **Factory** Pattern used for Enemy/NPC spawning.
- **Pool** Pattern used for Projectiles.
- **Singleton** Pattern used for GameManager (only 90 lines of code).
- Decoupled systems for extensibility (e.g., DamageSystem, StatSystem, InputSystem).
- Flexible Stat System that can fit into any AAA project.

---

## 📂 Project Structure

```
StillStanding
├── Assets
│   ├── Animations
|   ├── Artwork
│   ├── Champions
│   ├── Enemies
│   ├── Icons
│   ├── Items
|   ├── Models
|   ├── Prefabs
|   ├── Scenes
│   └── Scripts
|       ├── Abilities
|       ├── AutoAttacks
|       ├── Characters
|           ├── Enemies
|           ├── NPCs
|           └── Player
|       ├── Effects
|       ├── Extensions
|       ├── Interfaces
|       ├── Items
|       ├── Projectiles
|       ├── Stats
|       └── Views
|           └── AbilityIndicators
├── SFX
├── VFX
└── README.md
```

---

## 🚀 Getting Started

1. Clone this repository.
2. Open with Unity (2022.3.46f1 LTS or later recommended).
3. Press Play in the main scene (`Scenes/Main Scene.unity`).
4. Use WASD to move, mouse to aim, and ability hotkeys (QWER by default).

---

## 🔮 Roadmap

- [ ] Item pool generation (procedural & static)
- [ ] More enemy types with gimmicks and passives
- [✅] XP/leveling system
- [ ] Champion roster expansion
- [ ] Save/load system

---

## 📝 License

Custom academic license — see [LICENSE](./LICENSE) for details.

---

## 📫 Contact

For questions or collaboration:
- Telegram: [@dodgecar69]
- Email: sultanedil3@gmail.com
- [Join the community on Telegram](https://t.me/yedilstudio)
> “Powered by Unity, Driven by Passion.”

---

> “Still Standing is about staying alive by thinking like a MOBA player, not a survivor.”
