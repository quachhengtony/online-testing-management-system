USE OnlineTestingManagementSystemDb;
GO

INSERT INTO TestCreator (Id, Username, Email, FirstName, LastName, Password)
VALUES (NEWID(), 'johndoe', 'johndoe@gmail.com', 'John', 'Doe', 'password123');
GO

INSERT INTO QuestionCategory (Id, Category)
VALUES (1, 'MultipleChoices'), (2, 'TrueOrFalse'), (3, 'Open-ended');
GO

INSERT INTO TestCategory(Id, Category)
VALUES (1, 'Quiz'), (2, 'Exam');
GO