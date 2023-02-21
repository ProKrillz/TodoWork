using TodoWork.BLL.DTOModels;

namespace TodoWork.BLL.TodoServices
{
    public interface ITodoServices
    {
        Task CreateTaskAsync(DTOTodo todo);
        Task UpdateTaskAsync(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        List<DTOTodo> GetAllCompletedTask();
        Task CompletTaskAsync(Guid id);
        Task UnCompletedTaskAsync(Guid id);
        Task DeleteTaskAsync(Guid id);
        Task DeleteCompletedTaskAsync(Guid id);
    }
}
