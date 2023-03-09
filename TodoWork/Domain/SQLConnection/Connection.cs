using Microsoft.Data.SqlClient;
using System.Data;
using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;
using static TodoWork.BLL.DTOModels.DTOTodo;

namespace TodoWork.Domain.SQLConnection;

public class Connection : IConnection
{
    private readonly string _connectionString;
    private readonly SqlConnection _sqlConnection;
    public Connection(string connectionString)
    {
        _connectionString = connectionString;
        _sqlConnection = new SqlConnection(_connectionString);
    }
    /// <summary>
    /// Create Stored Procedure Async
    /// </summary>
    /// <param name="StoredProcedure"></param>
    /// <returns></returns>
    private Task<SqlCommand> CallSpAsync(string StoredProcedure)
    {
        SqlCommand spCommand = new(StoredProcedure)
        {
            CommandType = CommandType.StoredProcedure,
            Connection = _sqlConnection
        };
        return Task.FromResult(spCommand);
    }
    /// <summary>
    /// Create Stored Procedure Async
    /// </summary>
    /// <param name="StoredProcedure"></param>
    /// <returns></returns>
    private SqlCommand CallSp(string StoredProcedure)
    {
        SqlCommand spCommand = new(StoredProcedure)
        {
            CommandType = CommandType.StoredProcedure,
            Connection = _sqlConnection
        };
        return spCommand;
    }
    /// <summary>
    /// Transfer object
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    private DTOTodo TodoTransferToDTOTodo(Todo todo)
    {
        return new DTOTodo() { 
            Id = todo.Id, 
            UserId = todo.UserId,
            Title = todo.Title, 
            Description = todo.Description, 
            TaskPriority = (Priority)todo.TaskPriority, 
            Created = todo.CreatedDate, 
            Completed = todo.CompletedDate  
        };      
    }
    /// <summary>
    /// Transfer object
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    private Todo DTOTodoTransferToTodo(DTOTodo todo)
    {
        return new Todo() { 
            Id = todo.Id, 
            UserId = todo.UserId,
            Title = todo.Title,
            Description = todo.Description,
            TaskPriority = (int)todo.TaskPriority,
            CreatedDate = todo.Created,
            CompletedDate = todo.Completed
        };          
    }
    private DTOUser UserMappingDTOUser(User user)
    {
        return new DTOUser() { 
            Id= user.Id,
            Name= user.Name,
            Email= user.Email,
            Password= user.Password,
        };
    }
    private User DTOUserMappingUser(DTOUser user)
    {
        return new User() { 
            Id= user.Id,
            Name= user.Name,
            Email= user.Email,
            Password= user.Password,
        };
    }
    public List<DTOTodo> GetAllTask()
    {
        SqlCommand cmd = CallSp("GetAllTask");
        try
        {
            _sqlConnection.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            List<DTOTodo> list = new List<DTOTodo>();
            while (myReader.Read())
            {
                list.Add(TodoTransferToDTOTodo(new Todo
                {
                    Id = Guid.Parse(myReader.GetString("task_id")),
                    UserId = Guid.Parse(myReader.GetString("users_id")),
                    Title = myReader.GetString("task_title"),
                    Description = myReader.GetString("task_description"),
                    TaskPriority = myReader.GetInt32("priorities_id"),
                    CreatedDate = myReader.GetDateTime("task_created")
                }));
            }
            return list;
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task CreateTaskAsync(DTOTodo dtoTodo, Guid userId)
    {
        Todo todo = DTOTodoTransferToTodo(dtoTodo);
        SqlCommand cmd = await CallSpAsync("AddTask");
        cmd.Parameters.AddWithValue("@TaskId", todo.Id);
        cmd.Parameters.AddWithValue("@Titel", todo.Title);
        cmd.Parameters.AddWithValue("@Description", todo.Description);
        cmd.Parameters.AddWithValue("@Priorities", todo.TaskPriority);
        cmd.Parameters.AddWithValue("@Created", todo.CreatedDate);
        cmd.Parameters.AddWithValue("@UserId", userId);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
    public async Task<DTOTodo> GetTaskByIdAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("GetTaskById");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return TodoTransferToDTOTodo(new Todo() {
                    Id = Guid.Parse(reader.GetString("task_id")),
                    UserId = Guid.Parse(reader.GetString("users_id")),
                    Title = reader.GetString("task_title"),
                    Description = reader.GetString("task_description"),
                    TaskPriority = reader.GetInt32("priorities_id"),
                    CreatedDate = reader.GetDateTime("task_created")
                });
            }
            return null;
        }
        finally { _sqlConnection.Close(); } 
    }
    public async Task<List<DTOTodo>> GetAllCompletedTaskByUserIdAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("GetAllCompletedTaskByUserId");
        cmd.Parameters.AddWithValue("@UserId", id);
        try
        {
            _sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<DTOTodo> list = new();   
            while (reader.Read())
            {
                list.Add( TodoTransferToDTOTodo(new Todo()
                {
                    Id = Guid.Parse(reader.GetString("task_id")),
                    UserId = Guid.Parse(reader.GetString("users_id")),
                    Title = reader.GetString("task_title"),
                    Description = reader.GetString("task_description"),
                    TaskPriority = reader.GetInt32("priorities_id"),
                    CreatedDate = reader.GetDateTime("task_created"),
                    CompletedDate = reader.GetDateTime("task_completed")
                }));
            }
            return list;
        }
        finally { _sqlConnection.Close(); }
    }
    public List<DTOTodo> GetAllCompletedTask()
    {
        SqlCommand cmd = CallSp("GetAllCompletedTask");
        try
        {
            _sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<DTOTodo> list = new List<DTOTodo>();
            while (reader.Read())
            {
                list.Add(TodoTransferToDTOTodo(new Todo
                {
                    Id = Guid.Parse(reader.GetString("task_id")),
                    Title = reader.GetString("task_title"),
                    Description = reader.GetString("task_description"),
                    TaskPriority = reader.GetInt32("priorities_id"),
                    CompletedDate = reader.GetDateTime("task_completed"),
                    CreatedDate = reader.GetDateTime("task_created")
                }));
            }
            return list;
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
    public async Task UpdateTaskAsync(DTOTodo todox)
    {
        Todo todo = DTOTodoTransferToTodo(todox);
        SqlCommand cmd = await CallSpAsync("UpdateTask");
        cmd.Parameters.AddWithValue("@TaskId", todo.Id);
        cmd.Parameters.AddWithValue("@Titel", todo.Title);
        cmd.Parameters.AddWithValue("@Description", todo.Description);
        cmd.Parameters.AddWithValue("@PrioritiesId", todo.TaskPriority);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
    public async Task CompletTaskAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("CompletTask");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task DeleteTaskAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("DeleteTask");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task UnCompletedTaskAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("UnCompletedTask");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    #region User
    public async Task CreateUserAsync(DTOUser dtoUser)
    {
        User user = DTOUserMappingUser(dtoUser);
        SqlCommand cmd = await CallSpAsync("CreateUser");
        cmd.Parameters.AddWithValue("@UserId", user.Id);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task UpdateUserAsync(DTOUser dtoUser)
    {
        User user = DTOUserMappingUser(dtoUser);
        SqlCommand cmd = await CallSpAsync("UpdateUser");
        cmd.Parameters.AddWithValue("@UserId", user.Id);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public List<DTOUser> GetAllUsers()
    {
        SqlCommand cmd = CallSp("GetAllUsers");
        try
        {
            _sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<DTOUser> users = new List<DTOUser>();
            while (reader.Read())
            {
                users.Add(UserMappingDTOUser(new User
                {
                    Id = Guid.Parse(reader.GetString("users_id")),
                    Name = reader.GetString("users_name"),
                    Email = reader.GetString("users_email"),
                    Password = reader.GetString("Password")
                }));
            }
            return users;
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task DeleteUserAsync(Guid id)
    {
        SqlCommand cmd = await CallSpAsync("DeleteUser");
        cmd.Parameters.AddWithValue("@UserId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task<DTOUser> UserLoginAsync(string email, string password)
    {
        SqlCommand cmd = await CallSpAsync("UserLogin");
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@Password", password);
        try
        {
            _sqlConnection.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while(myReader.Read())
            {
                return UserMappingDTOUser(new User()
                {
                    Id = Guid.Parse(myReader.GetString("users_id")),
                    Name = myReader.GetString("users_name"),
                    Email = email,
                    Password = password
                });
            }
            return null;
        }
        finally { _sqlConnection.Close(); }
    }
    public async Task<DTOUser> GetUserByEmailAsync(string email)
    {
        SqlCommand cmd = await CallSpAsync("GetUserByEmail");
        cmd.Parameters.AddWithValue("@Email", email);
        try
        {
            _sqlConnection.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while(myReader.Read())
            {
                return UserMappingDTOUser(new User()
                {
                    Id = Guid.Parse( myReader.GetString("users_id")),
                    Name = myReader.GetString("users_name"),
                    Email = myReader.GetString("users_email"),
                    Password = myReader.GetString("users_password")
                });
            }
        }
        finally { _sqlConnection.Close(); }
        return null;
    }
    public List<DTOTodo> GetTodosByUserId(Guid id)
    {
        SqlCommand cmd = CallSp("GetAllTaskById");
        cmd.Parameters.AddWithValue("@UserId", id);
        try
        {
            _sqlConnection.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            List<DTOTodo> list = new();
            while(myReader.Read())
            {
                list.Add(TodoTransferToDTOTodo(new Todo()
                {
                    Id = Guid.Parse(myReader.GetString("task_id")),
                    UserId = Guid.Parse(myReader.GetString("users_id")),
                    Title = myReader.GetString("task_title"),
                    Description = myReader.GetString("task_description"),
                    TaskPriority = myReader.GetInt32("priorities_id"),
                    CreatedDate = myReader.GetDateTime("task_created")
                }));
            }
            return list;
        }
        finally { _sqlConnection.Close(); }
    }
    #endregion



}
