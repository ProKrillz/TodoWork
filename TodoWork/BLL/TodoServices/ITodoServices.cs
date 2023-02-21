using TodoWork.BLL.DTOModels;

namespace TodoWork.BLL.TodoServices
{
    public interface ITodoServices
    {
        void CreateTask(DTOTodo todo);
        void UpdateTask(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        List<DTOTodo> GetAllCompletedTask();
        void CompletTask(Guid id);
        void UnCompletedTask(Guid id);
        void DeleteTask(Guid id);
        void DeleteCompletedTask(Guid id);
    }
}
