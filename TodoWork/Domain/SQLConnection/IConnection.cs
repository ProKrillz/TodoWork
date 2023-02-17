using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;

namespace TodoWork.Domain.SQLConnection
{
    public interface IConnection
    {
        void CreateTask(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        void UpdateTask(DTOTodo todox);
        void CompletTask(int id);
        void DeleteTask(int id);
    }
}
