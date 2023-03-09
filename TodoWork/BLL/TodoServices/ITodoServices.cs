using TodoWork.BLL.DTOModels;

namespace TodoWork.BLL.TodoServices;
public interface ITodoServices
{
    /// <summary>
    /// Create Task with input title and description
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task CreateTaskAsync(DTOTodo todo, Guid userId);
    /// <summary>
    /// Get Task by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DTOTodo> GetTaskByIdAsync(Guid id);
    /// <summary>
    /// Get all completed task from user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<DTOTodo>> GetAllCompletedTaskByUserIdAsync(Guid id);
    /// <summary>
    /// Update Task input Title, Description and Priority
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task UpdateTaskAsync(DTOTodo todo);
    /// <summary>
    /// Set completed to Datetime.Now
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task CompletTaskAsync(Guid id);
    /// <summary>
    /// Set completed to null
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task UnCompletedTaskAsync(Guid id);
    /// <summary>
    /// remove task from list and set value in db
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteTaskAsync(Guid id);
    /// <summary>
    /// remove task from list and set value in db
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteCompletedTaskAsync(Guid id);
    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="dtoUser"></param>
    /// <returns></returns>
    Task CreateUserAsync(DTOUser dtoUser);
    /// <summary>
    /// Delete User by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteUserAsync(Guid id);
    /// <summary>
    /// Update User need all parameter
    /// </summary>
    /// <param name="dtoUser"></param>
    /// <returns></returns>
    Task UpdateUserAsync(DTOUser dtoUser);
    /// <summary>
    /// Login and return dtouser if found in db
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<DTOUser> UserLoginAsync(string email, string password);
    /// <summary>
    /// Get user by email for sessions
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<DTOUser> GetUserByEmailAsync(string email);
    /// <summary>
    /// Get todos by user id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    List<DTOTodo> GetTodosByUserId(Guid id);
}
