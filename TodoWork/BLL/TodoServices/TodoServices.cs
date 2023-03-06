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

        public async Task CompletTaskAsync(Guid id)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id == id).FirstOrDefault();
            if (foundTodo != null)
            {
                Task task = Task.Run(() => _connection.CompletTaskAsync(id));
                foundTodo.Completed = DateTime.Now;
                CompletedTodos.Add(foundTodo);
                Todos.RemoveAll(x => x.Id == id);
                await task;
            }
        }
        public async Task UnCompletedTaskAsync(Guid id)
        {
            DTOTodo? foundTodo = CompletedTodos.Where(x => x.Id == id).FirstOrDefault();
            if (foundTodo != null)
            {
                Task task = Task.Run(() => _connection.UnCompletedTaskAsync(id));
                foundTodo.Completed = null;
                Todos.Add(foundTodo);
                CompletedTodos.RemoveAll(x => x.Id == id);
                await task;
            }
        }

        public async Task CreateTaskAsync(DTOTodo todo)
        {
            Task task = Task.Run(() => _connection.CreateTaskAsync(todo));
            Todos.Add(todo);
            await task;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            Task task = Task.Run(() => _connection.DeleteTaskAsync(id));
            Todos.RemoveAll(x => x.Id == id);
            await task;
        }
        public async Task DeleteCompletedTaskAsync(Guid id)
        {
            Task task = Task.Run(() => _connection.DeleteTaskAsync(id));
            CompletedTodos.RemoveAll(x => x.Id == id);
            await task;
        }

        public List<DTOTodo> GetAllTask() => Todos;

        public List<DTOTodo> GetAllCompletedTask() => CompletedTodos;

        public async Task UpdateTaskAsync(DTOTodo todo)
        {
            DTOTodo? foundTodo = Todos.Where(x => x.Id == todo.Id).FirstOrDefault();
            if (foundTodo != null)
            {
                Task task = Task.Run(() => _connection.UpdateTaskAsync(todo));
                foundTodo.Title = todo.Title;
                foundTodo.Description = todo.Description;
                foundTodo.TaskPriority = todo.TaskPriority;
                await task;
            }
        }
    }
    public async Task CreateUser()
    {

    }
}
