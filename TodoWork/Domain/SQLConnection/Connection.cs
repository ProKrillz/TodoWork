using Microsoft.Data.SqlClient;
using System.Data;
using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;
using static TodoWork.BLL.DTOModels.DTOTodo;

namespace TodoWork.Domain.SQLConnection;

public class Connection : IConnection
{
    private readonly string _connectionString;
    public Connection(string connectionString) =>
        _connectionString = connectionString;
    
    #region --------------------------------------------------------- Sp ---------------------------------------------------------
    /// <summary>
    /// Create Stored Procedure Async
    /// </summary>
    /// <param name="StoredProcedure"></param>
    /// <returns></returns>
    private Task<SqlCommand> CallSpAsync(string StoredProcedure, SqlConnection con)
    {
        SqlCommand spCommand = new(StoredProcedure)
        {
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        return Task.FromResult(spCommand);
    }
    /// <summary>
    /// Create Stored Procedure Async
    /// </summary>
    /// <param name="StoredProcedure"></param>
    /// <returns></returns>
    private SqlCommand CallSp(string StoredProcedure, SqlConnection con)
    {
        SqlCommand spCommand = new(StoredProcedure)
        {
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        return spCommand;
    }
    #endregion

    #region ------------------------------------------------------ Mapping -------------------------------------------------------
    /// <summary>
    /// Mappping object
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    private DTOTodo TodoMappingToDTOTodo(Todo todo)
    {
        return new()
        {
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
    /// Mapping object
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    private Todo DTOTodoMappingToTodo(DTOTodo todo)
    {
        return new()
        {
            Id = todo.Id,
            UserId = todo.UserId,
            Title = todo.Title,
            Description = todo.Description,
            TaskPriority = (int)todo.TaskPriority,
            CreatedDate = todo.Created,
            CompletedDate = todo.Completed
        };
    }
    /// <summary>
    /// Mapping object
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private DTOUser UserMappingDTOUser(User user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
    /// <summary>
    /// Mapping object
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private User DTOUserMappingUser(DTOUser user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
    #endregion

    #region -------------------------------------------------------- Task --------------------------------------------------------
    public async Task CreateTaskAsync(DTOTodo dtoTodo, Guid userId)
    {
        Todo todo = DTOTodoMappingToTodo(dtoTodo);
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("AddTask", con);
            cmd.Parameters.AddWithValue("@Titel", todo.Title);
            cmd.Parameters.AddWithValue("@Description", todo.Description);
            cmd.Parameters.AddWithValue("@Priorities", todo.TaskPriority);
            cmd.Parameters.AddWithValue("@Created", todo.CreatedDate);
            cmd.Parameters.AddWithValue("@UserId", userId);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task<DTOTodo> GetTaskByIdAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("GetTaskById", con);
            cmd.Parameters.AddWithValue("@TaskId", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return TodoMappingToDTOTodo(new()
                {
                    Id = reader.GetGuid("task_id"),
                    UserId = reader.GetGuid("users_id"),
                    Title = reader.GetString("task_title"),
                    Description = reader.GetString("task_description"),
                    TaskPriority = reader.GetInt32("priorities_id"),
                    CreatedDate = reader.GetDateTime("task_created")
                });
            }
            return new();
        }
    }
    public async Task<List<DTOTodo>> GetAllCompletedTaskByUserIdAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("GetAllCompletedTaskByUserId", con);
            cmd.Parameters.AddWithValue("@UserId", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<DTOTodo> list = new();
            while (reader.Read())
            {
                list.Add(TodoMappingToDTOTodo(new()
                {
                    Id = reader.GetGuid("task_id"),
                    UserId = reader.GetGuid("users_id"),
                    Title = reader.GetString("task_title"),
                    Description = reader.GetString("task_description"),
                    TaskPriority = reader.GetInt32("priorities_id"),
                    CreatedDate = reader.GetDateTime("task_created"),
                    CompletedDate = reader.GetDateTime("task_completed")
                }));
            }
            return list;
        }
    }
    public async Task UpdateTaskAsync(DTOTodo todox)
    {
        Todo todo = DTOTodoMappingToTodo(todox);
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("UpdateTask", con);
            cmd.Parameters.AddWithValue("@TaskId", todo.Id);
            cmd.Parameters.AddWithValue("@Titel", todo.Title);
            cmd.Parameters.AddWithValue("@Description", todo.Description);
            cmd.Parameters.AddWithValue("@PrioritiesId", todo.TaskPriority);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task CompletTaskAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("CompletTask", con);
            cmd.Parameters.AddWithValue("@TaskId", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task DeleteTaskAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("DeleteTask", con);
            cmd.Parameters.AddWithValue("@TaskId", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task UnCompletedTaskAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("UnCompletedTask", con);
            cmd.Parameters.AddWithValue("@TaskId", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    #endregion

    #region -------------------------------------------------------- User -------------------------------------------------------- 
    public async Task CreateUserAsync(DTOUser dtoUser)
    {
        User user = DTOUserMappingUser(dtoUser);
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("CreateUser", con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task UpdateUserAsync(DTOUser dtoUser)
    {
        User user = DTOUserMappingUser(dtoUser);
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("UpdateUser", con);
            cmd.Parameters.AddWithValue("@UserId", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task DeleteUserAsync(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("DeleteUser", con);
            cmd.Parameters.AddWithValue("@UserId", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public async Task<DTOUser> UserLoginAsync(string email, string password)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("UserLogin", con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                return UserMappingDTOUser(new()
                {
                    Id = myReader.GetGuid("users_id"),
                    Name = myReader.GetString("users_name"),
                    Email = email,
                    Password = password
                });
            }
            return new();
        }
    }
    public async Task<DTOUser> GetUserByEmailAsync(string email)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = await CallSpAsync("GetUserByEmail", con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                return UserMappingDTOUser(new()
                {
                    Id = myReader.GetGuid("users_id"),
                    Name = myReader.GetString("users_name"),
                    Email = myReader.GetString("users_email"),
                    Password = myReader.GetString("users_password")
                });
            }
            return new();
        }
    }
    public List<DTOTodo> GetTodosByUserId(Guid id)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand cmd = CallSp("GetAllTaskById", con);
            cmd.Parameters.AddWithValue("@UserId", id);
            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            List<DTOTodo> list = new();
            while (myReader.Read())
            {
                list.Add(TodoMappingToDTOTodo(new()
                {
                    Id = myReader.GetGuid("task_id"),
                    UserId = myReader.GetGuid("users_id"),
                    Title = myReader.GetString("task_title"),
                    Description = myReader.GetString("task_description"),
                    TaskPriority = myReader.GetInt32("priorities_id"),
                    CreatedDate = myReader.GetDateTime("task_created")
                }));
            }
            return list;
        }
    }
    #endregion
}