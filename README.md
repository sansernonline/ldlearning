# LD E-Learning — แอปฝึกภาษาไทยสำหรับเด็ก LD

> เกมการศึกษาบน Unity สำหรับฝึก **พยัญชนะ สระ การออกเสียง การสะกดคำ และการคัดลายมือ** ภาษาไทย ออกแบบเพื่อเด็กที่มีภาวะบกพร่องทางการเรียนรู้ (Learning Disability)

![Unity](https://img.shields.io/badge/Unity-2017.4.6f1-black?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20iOS-blue)
![Language](https://img.shields.io/badge/Lang-C%23-239120?logo=csharp)
![Database](https://img.shields.io/badge/DB-SQLite-003B57?logo=sqlite)

---

## ภาพรวม

**LD E-Learning** เป็นแอปพลิเคชันมือถือเชิงการศึกษาที่พัฒนาด้วย Unity เพื่อช่วยเด็กที่มีภาวะบกพร่องทางการเรียนรู้ (LD) ฝึกทักษะพื้นฐานทางภาษาไทยผ่านบทเรียนแบบโต้ตอบ มีทั้งภาพ เสียง และการฝึกเขียนตามรอย พร้อมระบบแบบทดสอบและเก็บคะแนนของผู้เรียนแบบ local

เนื้อหาแบ่งเป็นหมวด **พยัญชนะ (Consonant) · สระ (Vowel) · การสะกดคำ (Spelling) · ประโยค (Sentence)** โดยแต่ละหมวดมีโหมดเรียนรู้และโหมดทดสอบ (Exam)

## คุณสมบัติหลัก (Features)

- 🔤 **บทเรียนพยัญชนะ/สระ** — แสดงรูปภาพ ตัวอักษร และเล่นเสียงประกอบของแต่ละตัว
- ✏️ **ฝึกคัดลายมือ** — ลากเส้นเขียนตามรอยตัวอักษรด้วย `LineRenderer` (scene `consonant-write`)
- 🔊 **ฝึกการออกเสียง** — โหมด consonant sound พร้อมแบบทดสอบเสียง
- 📝 **แบบทดสอบ (Exam)** — แยกตามกลุ่มพยัญชนะ/สระ พร้อมเสียง feedback ถูก/ผิด
- 👤 **โปรไฟล์ผู้เรียน** — บันทึกความคืบหน้าและคะแนนรายบุคคล
- 💾 **ฐานข้อมูล local (SQLite)** — ทำงานออฟไลน์ได้ ไม่ต้องต่ออินเทอร์เน็ต

## หน้าจอ / Scenes

| กลุ่ม | Scenes |
|------|--------|
| หลัก | `main-menu`, `main-profile`, `main-game`, `mark-menu` |
| พยัญชนะ | `consonant-menu`, `consonant-text`, `consonant-sound`, `consonant-write`, `consonant-text-menu`, `consonant-*-test-*` |
| สระ | `vowel-menu`, `vowel-text`, `vowel-text-menu`, `vowel-text-test-*` |
| อื่น ๆ | `spelling-menu`, `sentence-menu` |

## เทคโนโลยีที่ใช้ (Tech Stack)

- **Engine:** Unity `2017.4.6f1`
- **ภาษา:** C# (MonoBehaviour)
- **ฐานข้อมูล:** SQLite ผ่านไลบรารี [`SQLite4Unity3d`](https://github.com/codecoding/SQLite4Unity3d)
- **แพลตฟอร์มเป้าหมาย:** Android (หลัก) และ iOS
- **สื่อ:** รูป PNG, เสียง MP3, ฟอนต์ไทย (TTF/OTF)

## โครงสร้างโปรเจกต์

```
ldlearning/
├── src/ld/                    # โปรเจกต์ Unity
│   ├── Assets/
│   │   ├── Scene/             # ฉากทั้งหมดของแอป
│   │   ├── Scripts/           # โค้ด C# (Model / Controller / Service)
│   │   ├── Resources/         # รูป/เสียงตัวอักษร + FeedbackSound (correct/wrong)
│   │   └── StreamingAssets/   # ไฟล์ฐานข้อมูล SQLite (LD, existing.db)
│   └── ProjectSettings/
├── sqlite/                    # สคริปต์ schema (MySQL + SQLite) และเครื่องมือ
├── resource/                  # ไลบรารี SQLite4Unity3d, ฟอนต์, กราฟิก, mockup, GUI
├── document/                  # storyboard, use case, prototype (Axure), report
└── README.md
```

### สคริปต์สำคัญ

| ไฟล์ | หน้าที่ |
|------|---------|
| `MenuController.cs` | จัดการการโหลด scene ของแต่ละบทเรียน/แบบทดสอบ |
| `ConsonantModel.cs` / `VowelModel.cs` | โหลดรูป ข้อความ และเสียงของตัวอักษรจาก `Resources/` |
| `*ExamModel.cs` | ตรรกะของแบบทดสอบแต่ละหมวด |
| `LineDrawer.cs` / `LineCreator.cs` | ระบบลากเส้นเขียนตามรอย |
| `DataService.cs` / `DatabaseConnection.cs` | จัดการการเชื่อมต่อ SQLite |
| `ProfileModel.cs` | บันทึกสถานะการเรียนรู้ของผู้ใช้ |

## โครงสร้างฐานข้อมูล (Database)

ตารางหลักใน SQLite: `User`, `UserRole`, `Course`, `CourseGroup`, `CourseDetail`, `CourseAssetDetail`, `Exam`, `ExamScore`, `UserCourseScore`

> สคริปต์ schema เต็มอยู่ใน `sqlite/LD_ELearning-SQLite.sql` (และเวอร์ชัน MySQL `sqlite/LD_ELearning-MySQL.sql`)

## เริ่มต้นใช้งาน (Getting Started)

1. ติดตั้ง **Unity 2017.4.6f1** (แนะนำผ่าน Unity Hub — เป็นเวอร์ชันเก่า ต้องโหลดจาก archive)
2. เปิดโปรเจกต์จากโฟลเดอร์ `src/ld`
3. เปิด scene `Assets/Scene/main-menu.unity` แล้วกด Play
4. ไฟล์ฐานข้อมูลจะถูกคัดลอกจาก `StreamingAssets` ไปยัง `Application.persistentDataPath` อัตโนมัติในครั้งแรกที่รันบนอุปกรณ์

### Build

- ตั้ง Platform เป็น **Android** ใน Build Settings
- ตรวจสอบว่า scene ที่ต้องการอยู่ใน **Scenes In Build**

## เอกสารประกอบ

โฟลเดอร์ `document/` มีเอกสารออกแบบและรายงาน:
- `storyboard_ThaiAppforLD.pptx` — สตอรีบอร์ด
- `usecase.epgz` — แผนภาพ use case
- `LD E-Learning-prototype.rp` — ต้นแบบ (Axure)
- `report/report.pptx` — รายงานสรุปโครงงาน

## หมายเหตุ

โปรเจกต์นี้เป็นต้นแบบ (prototype) เพื่อการศึกษา พัฒนาในปี 2018 บน Unity 2017 ฐานข้อมูลและสื่อทั้งหมดทำงานแบบ local เป็นหลัก

## License

ยังไม่ได้ระบุ license — หากต้องการเปิดเป็นสาธารณะ แนะนำให้เพิ่มไฟล์ `LICENSE` (เช่น MIT) ให้ชัดเจน
