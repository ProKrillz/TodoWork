using TodoWork.BLL.DTOModels;
using TodoWork.Domain.SQLConnection;

namespace TodoWork.BLL.TodoServices
{
    public class TodoServices : ITodoServices
    {
        private readonly IConnection _connection;
        public List<DTOTodo> Todos { get; set; } = new();
        public TodoServices(IConnection connection)
        {
            _connection = connection;
            Todos = GetAllTask();
        }

        public void CompletTask(int id) =>
            _connection.CompletTask(id);

        public void CreateTask(DTOTodo todo) =>
            _connection.CreateTask(todo);

        public void DeleteTask(int id) =>
            _connection.DeleteTask(id);
        

        public List<DTOTodo> GetAllTask() =>
            _connection.GetAllTask();

        public void UpdateTask(DTOTodo todo) =>
            _connection.UpdateTask(todo);
    }
}
