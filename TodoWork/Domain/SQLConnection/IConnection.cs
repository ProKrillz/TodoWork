using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;

namespace TodoWork.Domain.SQLConnection
{
    public interface IConnection
    {
        void CreateTask(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        List<DTOTodo> GetAllCompletedTask();
        void UpdateTask(DTOTodo todox);
        void CompletTask(Guid id);
        void UnCompletedTask(Guid id);
        void DeleteTask(Guid id);
    }
}
