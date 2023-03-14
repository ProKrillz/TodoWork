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
@TaskId NVARCHAR(255),
@UserId NVARCHAR(255),
@Titel NVARCHAR(50),
@Description NVARCHAR(MAX),
@Priorities INT,
@Created DATETIME
AS
INSERT INTO Task
(task_id, priorities_id, users_id, task_title, task_description, task_created, task_completed)
VALUES
(@TaskId, @Priorities, @UserId, @Titel, @Description, @Created, null)
GO

CREATE OR ALTER PROCEDURE DeleteTask
@TaskId NVARCHAR(255)
AS
DELETE FROM Task
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE UpdateTask
@TaskId NVARCHAR(255),
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
@TaskId NVARCHAR(255)
AS
UPDATE Task
SET task_completed = GETDATE()
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE GetTaskById
@TaskId NVARCHAR(255)
AS
SELECT *
FROM GetAllTaskView
WHERE task_id = @TaskId
GO

CREATE OR ALTER PROCEDURE UnCompletedTask
@TaskId NVARCHAR(255)
AS
UPDATE Task
SET task_completed = NULL
WHERE task_id = @TaskId
GO

--------------------------------------------------- Users procedure ---------------------------------------------------

CREATE OR ALTER PROCEDURE CreateUser
@UserId NVARCHAR(255),
@Name NVARCHAR(50),
@Email NVARCHAR(50),
@Password NVARCHAR(MAX)
AS
INSERT INTO
Users
(users_id, users_email, users_name, users_password)
VALUES
(@UserId, @Email, @Name, @Password)
GO

CREATE OR ALTER PROCEDURE DeleteUser
@UserId NVARCHAR(255)
AS
DELETE FROM Task
WHERE users_id = @UserId
DELETE FROM Users
WHERE users_id = @UserId
GO

CREATE OR ALTER PROCEDURE UpdateUser
@UserId NVARCHAR(255),
@Name NVARCHAR(50),
@Email NVARCHAR(50),
@Password NVARCHAR(255)
AS
UPDATE Users
SET users_name = @Name,
users_email = @Email,
users_password = @Password
WHERE users_id = @UserId
GO 

--------------------------------------------------- User task ---------------------------------------------------

CREATE OR ALTER PROCEDURE GetAllTaskById
@UserId NVARCHAR(255)
AS
SELECT task_id, users_id, task_title, task_description, task_created, priorities_id
FROM GetAllTaskView 
WHERE users_id = @UserId
AND task_completed IS NULL
GO

CREATE OR ALTER PROCEDURE GetAllCompletedTaskByUserId
@UserId NVARCHAR(255)
AS
SELECT task_id, users_id, task_title, task_description, task_completed, task_created, priorities_id
FROM GetAllTaskView 
WHERE users_id = @UserId
AND task_completed IS NOT NULL
GO

CREATE OR ALTER PROCEDURE GetUserByEmail
@Email NVARCHAR(255)
AS
SELECT *
FROM Users
WHERE users_email = @Email
GO

CREATE OR ALTER PROCEDURE UserLogin
@Email NVARCHAR(50),
@Password NVARCHAR(MAX)
AS
SELECT users_id, users_name
FROM Users
WHERE users_email = @Email
AND users_Password = @Password
GO