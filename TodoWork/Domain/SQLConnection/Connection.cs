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
    private SqlCommand CallSp(string StoredProcedure)
    {
        SqlCommand spCommand = new(StoredProcedure)
        {
            CommandType = CommandType.StoredProcedure,
            Connection = _sqlConnection
        };
        return spCommand;
    }
    private DTOTodo TodoTransferToDTOTodo(Todo todo)
    {
        return new DTOTodo() { 
            Id = todo.Id, 
            Title = todo.Title, 
            Description = todo.Description, 
            TaskPriority = (Priority)todo.TaskPriority, 
            Created = todo.CreatedDate, 
            Completed = todo.CompletedDate };
    }
    private Todo DTOTodoTransferToTodo(DTOTodo todo)
    {
        return new Todo() { 
            Id = todo.Id, 
            Title = todo.Title,
            Description = todo.Description,
            TaskPriority = (int)todo.TaskPriority,
            CreatedDate = todo.Created,
            CompletedDate = todo.Completed
        };          
    }
    public void CreateTask(DTOTodo todo)
    {
        SqlCommand cmd = CallSp("AddTask");
        cmd.Parameters.AddWithValue("@Titel", todo.Title);
        cmd.Parameters.AddWithValue("@Description", todo.Description);
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
                    Id = myReader.GetInt32("task_id"),
                    Title = myReader.GetString("task_title"),
                    Description = myReader.GetString("task_description"),
                    TaskPriority = myReader.GetInt32("priorities_id"),
                    CompletedDate = myReader.GetDateTime("task_completed"),
                    CreatedDate = myReader.GetDateTime("task_created")
                })) ;
            }
            return list;
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
    public void UpdateTask(DTOTodo todox)
    {
        Todo todo = DTOTodoTransferToTodo(todox);
        SqlCommand cmd = CallSp("UpdateTask");
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
    public void CompletTask(int id)
    {
        SqlCommand cmd = CallSp("CompletTask");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
    public void DeleteTask(int id)
    {
        SqlCommand cmd = CallSp("DeleteTask");
        cmd.Parameters.AddWithValue("@TaskId", id);
        try
        {
            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
        }
        finally { _sqlConnection.Close(); }
    }
}
