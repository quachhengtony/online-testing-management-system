DROP DATABASE IF EXISTS OnlineTestingManagementSystemDb;
GO

CREATE DATABASE OnlineTestingManagementSystemDb;
GO

USE OnlineTestingManagementSystemDb;
GO

CREATE TABLE QuestionCategory(
    Id TINYINT NOT NULL PRIMARY KEY,
    Category VARCHAR(25) NOT NULL 
);
GO

CREATE TABLE Question(
    Id NVARCHAR(25) NOT NULL PRIMARY KEY,
    Content NVARCHAR(1000) NOT NULL,
    Weight TINYINT NOT NULL,
    QuestionCategoryId TINYINT,
	QuestionCreatorId NVARCHAR(25) NOT NULL
);
GO

CREATE TABLE Answer(
    Id TINYINT NOT NULL PRIMARY KEY,
    Content NVARCHAR(1000),
    IsCorrect BIT NOT NULL,
    QuestionId NVARCHAR(25) NOT NULL
);
GO

CREATE TABLE TestCreator(
    Id NVARCHAR(25) NOT NULL PRIMARY KEY,
    Username NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE TestTaker(
    Id NVARCHAR(25) NOT NULL PRIMARY KEY,
    Username NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE TestCategory(
    Id TINYINT NOT NULL PRIMARY KEY,
    Category VARCHAR(25) NOT NULL
);
GO

CREATE TABLE Test(
    Id NVARCHAR(25) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    KeyCode VARCHAR(25),
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    GradeReleaseDate DATETIME NOT NULL,
    GradeFinalizationDate DATETIME NOT NULL,
    Duration TINYINT NOT NULL,
    Batch VARCHAR(50) NOT NULL,
    TestCategoryId TINYINT,
    TestCreatorId NVARCHAR(25) NOT NULL
);
GO

CREATE TABLE Submission(
    Id NVARCHAR(25) NOT NULL PRIMARY KEY,
	TestTakerId NVARCHAR(25) NOT NULL,
	TestId NVARCHAR(25) NOT NULL,
	SubmittedDate DATETIME NOT NULL,
    GradedDate DATETIME NOT NULL,
	TimeTaken TINYINT NOT NULL,
	Score DECIMAL NOT NULL,
	Feedback NVARCHAR(100),
	IsGraded BIT,
    Content NVARCHAR(MAX) NOT NULL
);
GO

-- CREATE TABLE QuestionGroup(
--     Id NVARCHAR(25) NOT NULL PRIMARY KEY,
--     Name NVARCHAR(50),
-- 	QuestionGroupCreatorId NVARCHAR(25) NOT NULL
-- );
-- GO

-- Question many-to-one QuestionCategory
ALTER TABLE Question WITH NOCHECK
ADD FOREIGN KEY (QuestionCategoryId) REFERENCES QuestionCategory(Id) ON UPDATE CASCADE ON DELETE SET NULL;
GO

-- Answer many-to-one Question
ALTER TABLE Answer WITH NOCHECK 
ADD FOREIGN KEY (QuestionId) REFERENCES Question(Id) ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- TestCreator one-to-many Test
ALTER TABLE Test WITH NOCHECK
ADD FOREIGN KEY (TestCreatorId) REFERENCES TestCreator(Id) ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- TestCreator one-to-many QuestionGroup
-- ALTER TABLE QuestionGroup WITH NOCHECK
-- ADD FOREIGN KEY (QuestionGroupCreatorId) REFERENCES TestCreator(Id) ON UPDATE CASCADE ON DELETE CASCADE;
-- GO

-- TestCreator one-to-many Question
ALTER TABLE Question WITH NOCHECK
ADD FOREIGN KEY (QuestionCreatorId) REFERENCES TestCreator(Id) ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- Test one-to-many Submission
ALTER TABLE Submission WITH NOCHECK
ADD FOREIGN KEY (TestId) REFERENCES Test(Id) ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- TestTaker one-to-many Submission
ALTER TABLE Submission WITH NOCHECK
ADD FOREIGN KEY (TestTakerId) REFERENCES TestTaker(Id) ON UPDATE CASCADE ON DELETE CASCADE;
GO

-- TestCategory one-to-many Test
ALTER TABLE Test WITH NOCHECK
ADD FOREIGN KEY (TestCategoryId) REFERENCES TestCategory(Id) ON UPDATE CASCADE ON DELETE SET NULL;
GO

-- QuestionGroup many-to-many Question
-- CREATE TABLE QuestionGroupQuestion (
--     QuestionGroupId NVARCHAR(25) NOT NULL,
--     QuestionId NVARCHAR(25) NOT NULL,
--     PRIMARY KEY (QuestionGroupId, QuestionId),
--     CONSTRAINT FK_QuestionGroupQuestion_QuestionGroup FOREIGN KEY (QuestionGroupId) REFERENCES QuestionGroup(Id) ON UPDATE CASCADE ON DELETE CASCADE,
--     CONSTRAINT FK_QuestionGroupQuestion_Question FOREIGN KEY (QuestionId) REFERENCES Question(Id) ON UPDATE NO ACTION ON DELETE NO ACTION
-- );
-- GO

-- Test many-to-many QuestionGroup
-- CREATE TABLE TestQuestionGroup (
--     QuestionGroupId NVARCHAR(25) NOT NULL,
--     TestId NVARCHAR(25) NOT NULL,
--     PRIMARY KEY (QuestionGroupId, TestId),
--     CONSTRAINT FK_TestQuestionGroup_QuestionGroup FOREIGN KEY (QuestionGroupId) REFERENCES QuestionGroup(Id) ON UPDATE CASCADE ON DELETE CASCADE,
--     CONSTRAINT FK_TestQuestionGroup_Test FOREIGN KEY (TestId) REFERENCES Test(Id) ON UPDATE NO ACTION ON DELETE NO ACTION
-- );
-- GO

CREATE TABLE TestQuestion (
    QuestionId NVARCHAR(25) NOT NULL,
    TestId NVARCHAR(25) NOT NULL,
    PRIMARY KEY (QuestionId, TestId),
    CONSTRAINT FK_TestQuestion_Question FOREIGN KEY (QuestionId) REFERENCES Question(Id) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_TestQuestion_Test FOREIGN KEY (TestId) REFERENCES Test(Id) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

USE OnlineTestingManagementSystemDb;
GO