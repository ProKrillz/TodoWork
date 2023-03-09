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
	users_id NVARCHAR(255) NOT NULL PRIMARY KEY,
	users_name NVARCHAR(50),
	users_email NVARCHAR(50) UNIQUE,
	users_password NVARCHAR(MAX)
)
GO

CREATE TABLE Priorities
(
	priorities_id INT IDENTITY(1,1) PRIMARY KEY,
	priorities_name NVARCHAR(MAX)
)
GO

CREATE TABLE Task
(
	task_id NVARCHAR(255) NOT NULL PRIMARY KEY,
	priorities_id INT NOT NULL REFERENCES Priorities(priorities_id),
	users_id NVARCHAR(255) NOT NULL REFERENCES Users(users_id),
	task_title NVARCHAR(MAX),
	task_description NVARCHAR(MAX),
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