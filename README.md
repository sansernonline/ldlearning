# LD E-Learning — Thai Language Practice App for Children with LD

> A Unity educational game for practicing Thai **consonants, vowels, pronunciation, spelling, and handwriting**, designed for children with a Learning Disability (LD).

![Unity](https://img.shields.io/badge/Unity-2017.4.6f1-black?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20iOS-blue)
![Language](https://img.shields.io/badge/Lang-C%23-239120?logo=csharp)
![Database](https://img.shields.io/badge/DB-SQLite-003B57?logo=sqlite)

---

## Overview

**LD E-Learning** is an educational mobile app built with Unity that helps children with a learning disability (LD) practice basic Thai language skills through interactive lessons. It combines images, audio, and trace-to-write practice, with a quiz system and per-learner score tracking stored locally.

Content is organized into **Consonant · Vowel · Spelling · Sentence** categories, each with a learning mode and an exam mode.

## Features

- 🔤 **Consonant / vowel lessons** — shows the image, the letter, and plays the matching audio for each one
- ✏️ **Handwriting practice** — trace letters by drawing with `LineRenderer` (scene `consonant-write`)
- 🔊 **Pronunciation practice** — consonant-sound mode with an audio quiz
- 📝 **Exams** — grouped by consonant/vowel set, with correct/wrong audio feedback
- 👤 **Learner profile** — saves individual progress and scores
- 💾 **Local database (SQLite)** — works offline, no internet required

## Scenes

| Group | Scenes |
|-------|--------|
| Core | `main-menu`, `main-profile`, `main-game`, `mark-menu` |
| Consonant | `consonant-menu`, `consonant-text`, `consonant-sound`, `consonant-write`, `consonant-text-menu`, `consonant-*-test-*` |
| Vowel | `vowel-menu`, `vowel-text`, `vowel-text-menu`, `vowel-text-test-*` |
| Other | `spelling-menu`, `sentence-menu` |

## Tech Stack

- **Engine:** Unity `2017.4.6f1`
- **Language:** C# (MonoBehaviour)
- **Database:** SQLite via the [`SQLite4Unity3d`](https://github.com/codecoding/SQLite4Unity3d) library
- **Target platforms:** Android (primary) and iOS
- **Media:** PNG images, MP3 audio, Thai fonts (TTF/OTF)

## Project Structure

```
ldlearning/
├── src/ld/                    # Unity project
│   ├── Assets/
│   │   ├── Scene/             # all app scenes
│   │   ├── Scripts/           # C# code (Model / Controller / Service)
│   │   ├── Resources/         # letter images/audio + FeedbackSound (correct/wrong)
│   │   └── StreamingAssets/   # SQLite database files (LD, existing.db)
│   └── ProjectSettings/
├── sqlite/                    # schema scripts (MySQL + SQLite) and tools
├── resource/                  # SQLite4Unity3d library, fonts, graphics, mockups, GUI
├── document/                  # storyboard, use case, prototype (Axure), report
└── README.md
```

### Key Scripts

| File | Responsibility |
|------|----------------|
| `MenuController.cs` | handles scene loading for each lesson/exam |
| `ConsonantModel.cs` / `VowelModel.cs` | load each letter's image, text, and audio from `Resources/` |
| `*ExamModel.cs` | logic for each category's exam |
| `LineDrawer.cs` / `LineCreator.cs` | trace-to-write drawing system |
| `DataService.cs` / `DatabaseConnection.cs` | manage the SQLite connection |
| `ProfileModel.cs` | save the learner's progress state |

## Database

Main SQLite tables: `User`, `UserRole`, `Course`, `CourseGroup`, `CourseDetail`, `CourseAssetDetail`, `Exam`, `ExamScore`, `UserCourseScore`

> The full schema is in `sqlite/LD_ELearning-SQLite.sql` (with a MySQL version in `sqlite/LD_ELearning-MySQL.sql`).

## Getting Started

1. Install **Unity 2017.4.6f1** (via Unity Hub — this is an older version and must be downloaded from the archive)
2. Open the project from the `src/ld` folder
3. Open the scene `Assets/Scene/main-menu.unity` and press Play
4. On first run on a device, the database files are copied automatically from `StreamingAssets` to `Application.persistentDataPath`

### Build

- Set the platform to **Android** in Build Settings
- Make sure the required scenes are listed in **Scenes In Build**

## Documentation

The `document/` folder contains design documents and a report:
- `storyboard_ThaiAppforLD.pptx` — storyboard
- `usecase.epgz` — use case diagram
- `LD E-Learning-prototype.rp` — prototype (Axure)
- `report/report.pptx` — project summary report

## Notes

This is an educational prototype, developed in 2018 on Unity 2017. The database and media run primarily in a local-first manner.

## License

No license specified yet — if you plan to make it public, it's recommended to add a clear `LICENSE` file (e.g. MIT).
