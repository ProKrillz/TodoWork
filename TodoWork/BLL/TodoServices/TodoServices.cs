using System.Reflection.Metadata.Ecma335;
using TodoWork.BLL.DTOModels;
using TodoWork.Domain.SQLConnection;

namespace TodoWork.BLL.TodoServices
{
    public class TodoServices : ITodoServices
    {
        private readonly IConnection _connection;
        public List<DTOTodo> Todos { get; set; }
        public TodoServices(IConnection connection)
        {
            _connection = connection;
            Todos = _connection.GetAllTask();
        }

        public void CompletTask(Guid id)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id == id).FirstOrDefault();
            if (foundTodo != null)
            {
                _connection.CompletTask(id);
                foundTodo.Completed = DateTime.Now;
            }
        }

        public void CreateTask(DTOTodo todo)
        {
            _connection.CreateTask(todo);
            Todos.Add(todo);
        }

        public void DeleteTask(Guid id)
        {
            _connection.DeleteTask(id);
            Todos.RemoveAll(x => x.Id == id);
        }

        public List<DTOTodo> GetAllTask() => Todos;   

        public void UpdateTask(DTOTodo todo)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id!= todo.Id).FirstOrDefault();
            if (foundTodo != null)
            {
                _connection.UpdateTask(todo);
                foundTodo.Title = todo.Title;
                foundTodo.Description = todo.Description;
                foundTodo.TaskPriority = todo.TaskPriority;
            }
        }
    }
}
