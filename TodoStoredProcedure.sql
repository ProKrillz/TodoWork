USE master
GO

USE Todo
GO

--------------------------------------------------- Task procedure ---------------------------------------------------
CREATE OR ALTER VIEW GetAllTaskView
AS
SELECT *
FROM Task
GO

CREATE OR ALTER PROCEDURE AddTask
@UserId CHAR(36),
@Titel NVARCHAR(50),
@Description NVARCHAR(MAX),
@Priorities INT,
@Created DATETIME
AS
INSERT INTO Task
(priorities_id, users_id, task_title, task_description, task_created, task_completed)
VALUES
(@Priorities, @UserId, @Titel, @Description, @Created, null)
GO

CREATE OR ALTER PROCEDURE DeleteTask
@TaskId CHAR(36)
AS
DELETE FROM Task
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE UpdateTask
@TaskId CHAR(36),
@Titel NVARCHAR(50),
@Description NVARCHAR(255),
@PrioritiesId INT
AS
UPDATE Task
SET task_title = @Titel,
task_description = @Description,
priorities_id = @PrioritiesId
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE CompletTask
@TaskId CHAR(36)
AS
UPDATE Task
SET task_completed = GETDATE()
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE GetTaskById
@TaskId CHAR(36)
AS
SELECT *
FROM GetAllTaskView
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE UnCompletedTask
@TaskId CHAR(36)
AS
UPDATE Task
SET task_completed = NULL
WHERE task_id = @TaskId
GO

--------------------------------------------------- Users procedure ---------------------------------------------------

CREATE OR ALTER PROCEDURE CreateUser
@Name NVARCHAR(50),
@Email NVARCHAR(50),
@Password NVARCHAR(MAX)
AS
INSERT INTO
Users
(users_email, users_name, users_password)
VALUES
(@Email, @Name, HASHBYTES('SHA2_512', @Password))
GO

CREATE OR ALTER PROCEDURE DeleteUser
@UserId CHAR(36)
AS
DELETE FROM Task
WHERE users_id = @UserId
DELETE FROM Users
WHERE users_id = @UserId
GO

CREATE OR ALTER PROCEDURE UpdateUser
@UserId CHAR(36),
@Name NVARCHAR(50),
@Email NVARCHAR(50),
@Password NVARCHAR(255)
AS
UPDATE Users
SET users_name = @Name,
users_email = @Email,
users_password = HASHBYTES('SHA2_512', @Password)
WHERE users_id = @UserId
GO 

--------------------------------------------------- User task ---------------------------------------------------

CREATE OR ALTER PROCEDURE GetAllTaskById
@UserId CHAR(36)
AS
SELECT task_id, users_id, task_title, task_description, task_created, priorities_id
FROM GetAllTaskView 
WHERE users_id = @UserId
AND task_completed IS NULL
GO

CREATE OR ALTER PROCEDURE GetAllCompletedTaskByUserId
@UserId CHAR(36)
AS
SELECT task_id, users_id, task_title, task_description, task_completed, task_created, priorities_id
FROM GetAllTaskView 
WHERE users_id = @UserId
AND task_completed IS NOT NULL
GO

CREATE OR ALTER PROCEDURE GetUserByEmail
@Email NVARCHAR(50)
AS
BEGIN
	SELECT users_id, users_name, users_email, users_password
	FROM Users
	WHERE users_email = @Email
END
GO

CREATE OR ALTER PROCEDURE UserLogin
@Email NVARCHAR(50),
@Password NVARCHAR(MAX)
AS
SELECT users_id, users_name
FROM Users
WHERE users_email = @Email
AND users_Password = HASHBYTES('SHA2_512', @Password)
GO