USE OnlineTestingManagementSystemDb;
GO

INSERT INTO TestCreator (Id, Username, Email, FirstName, LastName, Password)
VALUES ('TC00001', 'johndoe', 'johndoe@gmail.com', 'John', 'Doe', 'password123');
GO

INSERT INTO QuestionCategory (Id, Category)
VALUES (1, 'MultipleChoices'), (2, 'TrueOrFalse'), (3, 'Open-ended');
GO

INSERT INTO Question (Id, Content, Weight, QuestionCategoryId, QuestionCreatorId)
VALUES ('Q00001', 'Lorem ipsum dolor sit amet', 10, 3, 'TC00001');
GO

INSERT INTO Answer (Id, Content, IsCorrect, QuestionId)
VALUES ('A00001', 'Answer to lorem ipsum', 1, 'Q00001');
GO