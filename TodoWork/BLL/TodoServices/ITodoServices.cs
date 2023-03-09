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
    Task CreateUserAsync(DTOUser dtoUser);
    List<DTOUser> GetAllUsers();
    Task DeleteUserAsync(Guid id);
    Task UpdateUserAsync(DTOUser dtoUser);
    Task<DTOUser> UserLoginAsync(string email, string password);
    Task<DTOUser> GetUserByEmailAsync(string email);
    List<DTOTodo> GetTodosByUserId(Guid id);
}
