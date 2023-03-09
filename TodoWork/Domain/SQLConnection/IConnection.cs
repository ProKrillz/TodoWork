using TodoWork.BLL.DTOModels;

namespace TodoWork.Domain.SQLConnection;

public interface IConnection
{
    /// <summary>
    /// Load all uncompleted todo from db Task Table
    /// </summary>
    /// <returns></returns>
    List<DTOTodo> GetAllTask();
    /// <summary>
    /// Get task by id from db Task Table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DTOTodo> GetTaskByIdAsync(Guid id);
    /// <summary>
    /// Get all completed task from user in task table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<DTOTodo>> GetAllCompletedTaskByUserIdAsync(Guid id);
    /// <summary>
    /// Load all completed todo from db Task Table
    /// </summary>
    /// <returns></returns>
    List<DTOTodo> GetAllCompletedTask();
    /// <summary>
    /// Create a copy of DTO to Todo object and write to db Task table 
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task CreateTaskAsync(DTOTodo todo, Guid userId);
    /// <summary>
    /// Create a copy of DTO to Todo object and update ti db Task Table (by guid)
    /// </summary>
    /// <param name="todox"></param>
    /// <returns></returns>
    Task UpdateTaskAsync(DTOTodo todox);
    /// <summary>
    /// Set datetime in db Task table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task CompletTaskAsync(Guid id);
    /// <summary>
    /// Set is_deleted to 1 in db Task table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteTaskAsync(Guid id);
    /// <summary>
    /// Set datetime in db Taskm table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task UnCompletedTaskAsync(Guid id);
    Task CreateUserAsync(DTOUser dtoUser);
    List<DTOUser> GetAllUsers();
    Task DeleteUserAsync(Guid id);
    Task UpdateUserAsync(DTOUser dtoUser);
    Task<DTOUser> UserLoginAsync(string email, string password);
    Task<DTOUser> GetUserByEmailAsync(string email);
    List<DTOTodo> GetTodosByUserId(Guid id);
}
