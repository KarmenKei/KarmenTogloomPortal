# Миний анхны төсөл
Энэ бол миний Программ хангамж хөгжүүлэлтийн процесс
хичээлийн анхны төсөл.
## Багийн гишүүд
- Ц.Бэлгүтэй (Product Owner)
- Б.Дүүрэнбилэг (Scrum Master)
- Т.Тодбилэг (Хөгжүүлэгч)
- Б.Мөнхсүлд (Хөгжүүлэгч)
## Төслийн зорилго
Энэ төслөөр бид Scrum аргачлалыг дагаж, классик хэв маягийн мини тоглоомын багц хөгжүүлэх юм.

Тоглоом бүр хурдан, амархан тоглох боломжтой, богино хугацаанд хөгжилтэй байхыг зориулав.
Портал нь лидербоард системтэй бөгөөд оноог бекэнд мэдээллийн санд хадгална, ингэснээр тоглогчид жагсаалтад байр сууриа харах боломжтой.

Багц нь видео тоглоомын аппликейшн шиг ажилладаг бөгөөд хэрэглэгчид гол цэсээс хүссэн тоглоомоо сонгож тоглох боломжтой.
Тоглогч нар хоорондоо өрсөлдөж, лидербоард дээр оноогоо хадгалах боломжтой.

🕹 How to Play
1. Launch the Game
Unity Editor-т MainMenu scene-ийг нээнэ

▶ Play дарна
2. Main Menu

START → Flappy Bird тоглоом эхэлнэ
LEADERBOARD → Firebase дээр хадгалагдсан оноонуудыг харах
QUIT → Программаас гарах

3. Flappy Bird
Space / Click → Шувуу үсрэх
Саадыг давах тусам оноо нэмэгдэнэ
Тоглоом дуусахад:
Оноо автоматаар Firebase-д хадгалагдана
(Зөвхөн high score оноо л хадгалагдана)

Firebase Setup (PC / Unity)
1. Firebase Project үүсгэх
https://console.firebase.google.com - руу орно
Add project → Project нэр өгнө
Analytics → Skip (шаардлагагүй)

2. Firestore Database тохируулах

Build → Firestore Database
Create database
Mode: Test mode (эсвэл read-only rules)
Location сонгоно

leaderboards/
 └─ flappy/
    └─ scores/
       └─ {playerId}
          ├─ score: number
          ├─ playerId: string
          └─ updatedAt: timestamp

3. Firebase Unity SDK суулгах

https://firebase.google.com/docs/unity/setup
FirebaseApp, Auth, Firestore packages татна
Unity дээр:

Assets → Import Package → Custom Package

4. Firebase App үүсгэх (Desktop / PC)
Firebase Console → Add app
Platform: Web (PC Unity-д ашиглана)
Config-ийг авна

FirebaseApp.Create(new AppOptions {
  ProjectId = "your-project-id",
  ApiKey = "your-api-key",
  AppId = "your-app-id"
});

5. Firebase Bootstrap (Unity)
FirebaseBootstrapPC script нь:
Firebase initialize
Anonymous user ID үүсгэх
Firestore connection шалгах

Console дээр дараах log харагдана:
Firebase OK
Firestore initialized
PlayerId: xxxxx

6. macOS Gatekeeper анхааруулга (Хэрэв гарвал)

System Settings → Privacy & Security → Allow Anyway → Unity-г restart хийнэ
(Энэ нь Firebase native binary-тай холбоотой, хэвийн үзэгдэл)

🏆 Leaderboard Logic
Оноо Game Over үед автоматаар submit хийнэ
Firebase transaction ашиглан:
Хуучин онооноос бага бол overwrite хийхгүй
Leaderboard олон тоглоомд дахин ашиглагдана


🛠 Development Notes
Unity 6000.x
Input System (New)
Firebase Firestore
Service-based architecture
CI / Code Review ашигласан
