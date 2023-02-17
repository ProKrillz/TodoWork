using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;

namespace TodoWork.BLL.TodoServices
{
    public interface ITodoServices
    {
        void CreateTask(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        void UpdateTask(DTOTodo todo);
        void CompletTask(int id);
        void DeleteTask(int id);
    }
}
