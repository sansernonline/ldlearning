CREATE TABLE "User" (
"Id" REAL NOT NULL,
"FirstName" TEXT(255),
"LastName" TEXT(255),
"Email" TEXT(255),
"Password" TEXT(255),
"Status" INTEGER,
"Tel" TEXT(20),
"Token" TEXT(255),
"Coin" TEXT(100),
"CreateDate" TEXT,
"UpdateDate" TEXT,
"CreateBy" TEXT(255),
"UpdateBy" TEXT(255),
PRIMARY KEY ("Id") 
);

CREATE TABLE "UserRole" (
"Id" REAL NOT NULL,
"Name" TEXT(255),
"Description" TEXT(255),
"Status" INTEGER,
PRIMARY KEY ("Id") 
);

CREATE TABLE "CourseDetail" (
"Id" REAL NOT NULL,
"Name" TEXT(255),
"Topic" TEXT(255),
"Description1" TEXT(255),
"Description2" TEXT(255),
"Description3" TEXT(255),
"Description4" TEXT(255),
"Level" INTEGER,
"LevelLimit" INTEGER,
"LevelBefore" INTEGER,
"Score" INTEGER,
"UseCoin" TEXT(100),
"Image1" TEXT(255),
"Image2" TEXT(255),
"Image3" TEXT(255),
"Image4" TEXT(255),
"Sound1" TEXT(255),
"Sound2" TEXT(255),
"Sound3" TEXT(255),
"Sound4" TEXT(255),
"CourseId" TEXT(255),
"ExamId" REAL,
PRIMARY KEY ("Id") 
);

CREATE TABLE "Exam" (
"Id" REAL NOT NULL,
"ExamGroup" TEXT(255),
"Name" TEXT(255),
"Description" TEXT(255),
"LevelBefore" INTEGER,
"Score" INTEGER,
"Question" TEXT(255),
"QuestionImage1" TEXT(255),
"QuestionSound1" TEXT(255),
"Answer1" TEXT(255),
"QuestionImage2" TEXT(255),
"QuesttionImage3" TEXT(255),
"QuestionImage4" TEXT(255),
"Answer2" TEXT(255),
"QuestionSound2" TEXT(255),
"QuestionSound3" TEXT(255),
"QuestionSound4" TEXT(255),
"Answer3" TEXT(255),
"Answer4" TEXT(255),
"Answer5" TEXT(255),
"Answer6" TEXT(255),
"AnswerImage1" TEXT(255),
"AnswerImage2" TEXT(255),
"AnswerImage3" TEXT(255),
"AnswerImage4" TEXT(255),
"AnswerSound1" TEXT(255),
"AnswerSound2" TEXT(255),
"AnswerSound3" TEXT(255),
"AnswerSound4" TEXT(255),
"Level" INTEGER,
"LevelLimit" INTEGER,
"Status" INTEGER,
PRIMARY KEY ("Id") 
);

CREATE TABLE "UserExamScore" (
);

CREATE TABLE "UserCourseScore" (
"Id" REAL NOT NULL,
"CourseDetailId" REAL,
"UserId" REAL,
"MyScore" INTEGER,
"CourseScore" INTEGER,
"Status" INTEGER,
"Version" INTEGER,
"CreateDate" TEXT,
PRIMARY KEY ("Id") 
);

CREATE TABLE "ExamAnswer" (
);

CREATE TABLE "CourseGroup" (
"Id" REAL NOT NULL,
"Name" TEXT,
"Description" TEXT,
"Status" INTEGER,
PRIMARY KEY ("Id") 
);

CREATE TABLE "ExamGroup" (
);

CREATE TABLE "Course" (
"Id" REAL NOT NULL,
"CourseGroup" TEXT(255),
"Name" TEXT(255),
"Description" TEXT(255),
"Status" INTEGER,
"Level" INTEGER,
"LevelLimit" INTEGER,
"Score" INTEGER,
PRIMARY KEY ("Id") 
);

CREATE TABLE "ExamDetail" (
);

CREATE TABLE "Payment" (
);

CREATE TABLE "CreditCard" (
);

CREATE TABLE "CourseAssetDetail" (
"Id" REAL NOT NULL,
"Name" TEXT,
"Text" TEXT,
"Image" TEXT,
"Sound" TEXT,
PRIMARY KEY ("Id") 
);

CREATE TABLE "ExamScore" (
"Id" REAL NOT NULL,
"ExamId" REAL,
"UserId" REAL,
"MyScore" INTEGER,
"CourseScore" INTEGER,
"Status" INTEGER,
"Version" INTEGER,
"CreateDate" TEXT,
PRIMARY KEY ("Id") 
);

