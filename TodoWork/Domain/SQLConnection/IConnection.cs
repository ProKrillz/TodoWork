using TodoWork.BLL.DTOModels;

namespace TodoWork.Domain.SQLConnection;

public interface IConnection
{
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
    /// <summary>
    /// Set data in db User table
    /// </summary>
    /// <param name="dtoUser"></param>
    /// <returns></returns>
    Task CreateUserAsync(DTOUser dtoUser);
    /// <summary>
    /// Delete User from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteUserAsync(Guid id);
    /// <summary>
    /// Update user for db User table
    /// </summary>
    /// <param name="dtoUser"></param>
    /// <returns></returns>
    Task UpdateUserAsync(DTOUser dtoUser);
    /// <summary>
    /// Load data from db User table
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<DTOUser> UserLoginAsync(string email, string password);
    /// <summary>
    /// Load data from db User Table
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<DTOUser> GetUserByEmailAsync(string email);
    /// <summary>
    /// Load data from db Task table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    List<DTOTodo> GetTodosByUserId(Guid id);
}
