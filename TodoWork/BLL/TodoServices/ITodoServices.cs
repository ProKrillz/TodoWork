using TodoWork.BLL.DTOModels;

namespace TodoWork.BLL.TodoServices;
public interface ITodoServices
{
    /// <summary>
    /// Create Task with input title and description
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task CreateTaskAsync(DTOTodo todo);
    /// <summary>
    /// Update Task input Title, Description and Priority
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task UpdateTaskAsync(DTOTodo todo);
    /// <summary>
    /// Gett all uncompleted task
    /// </summary>
    /// <returns></returns>
    List<DTOTodo> GetAllTask();
    /// <summary>
    /// Get all Completed task
    /// </summary>
    /// <returns></returns>
    List<DTOTodo> GetAllCompletedTask();
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
}
