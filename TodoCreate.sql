USE master
GO

IF EXISTS(SELECT * FROM sysdatabases WHERE name = 'Todo')
ALTER DATABASE [YuGiOhDatabase] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE Todo
GO

CREATE DATABASE Todo
GO

USE Todo
GO

CREATE TABLE Users
(
	users_id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	users_name NVARCHAR(50),
	users_email NVARCHAR(50) UNIQUE,
	users_password NVARCHAR(MAX)
)
GO

CREATE TABLE Priorities
(
	priorities_id INT IDENTITY(1,1) PRIMARY KEY,
	priorities_name NVARCHAR(25)
)
GO

CREATE TABLE Task
(
	task_id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	priorities_id INT NOT NULL REFERENCES Priorities(priorities_id),
	users_id UNIQUEIDENTIFIER NOT NULL REFERENCES Users(users_id),
	task_title NVARCHAR(25),
	task_description NVARCHAR(100),
	task_created DATETIME,
	task_completed DATETIME
)
GO

INSERT INTO Priorities
(priorities_name)
VALUES
('High'),
('Medium'),
('Low')