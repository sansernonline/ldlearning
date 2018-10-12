CREATE TABLE `User` (
`Id` decimal NOT NULL,
`FirstName` varchar(255) NULL,
`LastName` varchar(255) NULL,
`Email` varchar(255) NULL,
`Password` varchar(255) NULL,
`Status` int NULL,
`Tel` varchar(20) NULL,
`Token` varchar(255) NULL,
`Coin` varchar(100) NULL,
`CreateDate` datetime NULL,
`UpdateDate` datetime NULL,
`CreateBy` varchar(255) NULL,
`UpdateBy` varchar(255) NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `UserRole` (
`Id` decimal NOT NULL,
`Name` varchar(255) NULL,
`Description` varchar(255) NULL,
`Status` int NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `CourseDetail` (
`Id` decimal NOT NULL,
`Name` varchar(255) NULL,
`Topic` varchar(255) NULL,
`Description1` varchar(255) NULL,
`Description2` varchar(255) NULL,
`Description3` varchar(255) NULL,
`Description4` varchar(255) NULL,
`Level` int NULL,
`LevelLimit` int NULL,
`LevelBefore` int NULL,
`Score` int NULL,
`UseCoin` varchar(100) NULL,
`Image1` varchar(255) NULL,
`Image2` varchar(255) NULL,
`Image3` varchar(255) NULL,
`Image4` varchar(255) NULL,
`Sound1` varchar(255) NULL,
`Sound2` varchar(255) NULL,
`Sound3` varchar(255) NULL,
`Sound4` varchar(255) NULL,
`CourseId` varchar(255) NULL,
`ExamId` decimal NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `Exam` (
`Id` decimal NOT NULL,
`ExamGroup` varchar(255) NULL,
`Name` varchar(255) NULL,
`Description` varchar(255) NULL,
`LevelBefore` int NULL,
`Score` int NULL,
`Question` varchar(255) NULL,
`QuestionImage1` varchar(255) NULL,
`QuestionSound1` varchar(255) NULL,
`Answer1` varchar(255) NULL,
`QuestionImage2` varchar(255) NULL,
`QuesttionImage3` varchar(255) NULL,
`QuestionImage4` varchar(255) NULL,
`Answer2` varchar(255) NULL,
`QuestionSound2` varchar(255) NULL,
`QuestionSound3` varchar(255) NULL,
`QuestionSound4` varchar(255) NULL,
`Answer3` varchar(255) NULL,
`Answer4` varchar(255) NULL,
`Answer5` varchar(255) NULL,
`Answer6` varchar(255) NULL,
`AnswerImage1` varchar(255) NULL,
`AnswerImage2` varchar(255) NULL,
`AnswerImage3` varchar(255) NULL,
`AnswerImage4` varchar(255) NULL,
`AnswerSound1` varchar(255) NULL,
`AnswerSound2` varchar(255) NULL,
`AnswerSound3` varchar(255) NULL,
`AnswerSound4` varchar(255) NULL,
`Level` int NULL,
`LevelLimit` int NULL,
`Status` int NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `UserExamScore` (
);

CREATE TABLE `UserCourseScore` (
`Id` decimal NOT NULL,
`CourseDetailId` decimal NULL,
`UserId` decimal NULL,
`MyScore` int NULL,
`CourseScore` int NULL,
`Status` int NULL,
`Version` int NULL,
`CreateDate` datetime NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `ExamAnswer` (
);

CREATE TABLE `CourseGroup` (
`Id` double NOT NULL,
`Name` longtext NULL,
`Description` longtext NULL,
`Status` int NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `ExamGroup` (
);

CREATE TABLE `Course` (
`Id` decimal NOT NULL,
`CourseGroup` varchar(255) NULL,
`Name` varchar(255) NULL,
`Description` varchar(255) NULL,
`Status` int NULL,
`Level` int NULL,
`LevelLimit` int NULL,
`Score` int NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `ExamDetail` (
);

CREATE TABLE `Payment` (
);

CREATE TABLE `CreditCard` (
);

CREATE TABLE `CourseAssetDetail` (
`Id` double NOT NULL,
`Name` longtext NULL,
`Text` longtext NULL,
`Image` longtext NULL,
`Sound` longtext NULL,
PRIMARY KEY (`Id`) 
);

CREATE TABLE `ExamScore` (
`Id` decimal NOT NULL,
`ExamId` decimal NULL,
`UserId` decimal NULL,
`MyScore` int NULL,
`CourseScore` int NULL,
`Status` int NULL,
`Version` int NULL,
`CreateDate` datetime NULL,
PRIMARY KEY (`Id`) 
);

