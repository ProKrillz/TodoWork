using System.Reflection.Metadata.Ecma335;
using TodoWork.BLL.DTOModels;
using TodoWork.Domain.SQLConnection;

namespace TodoWork.BLL.TodoServices
{
    public class TodoServices : ITodoServices
    {
        private readonly IConnection _connection;
        public List<DTOTodo> Todos { get; set; }
        public List<DTOTodo> CompletedTodos { get; set; }
        public TodoServices(IConnection connection)
        {
            _connection = connection;
            Todos = _connection.GetAllTask();
            CompletedTodos = _connection.GetAllCompletedTask();
        }

        public void CompletTask(Guid id)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id == id).FirstOrDefault();
            if (foundTodo != null)
            {
                _connection.CompletTask(id);
                foundTodo.Completed = DateTime.Now;
                CompletedTodos.Add(foundTodo);
                Todos.RemoveAll(x => x.Id == id);
            }
        }
        public void UnCompletedTask(Guid id)
        {
            DTOTodo? foundTodo = CompletedTodos.Where(x => x.Id == id).FirstOrDefault();
            if (foundTodo != null)
            {
                _connection.UnCompletedTask(id);
                foundTodo.Completed = null;
                Todos.Add(foundTodo);
                CompletedTodos.RemoveAll(x => x.Id == id);
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

        public List<DTOTodo> GetAllCompletedTask() => CompletedTodos;

        public void UpdateTask(DTOTodo todo)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id == todo.Id).FirstOrDefault();
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
