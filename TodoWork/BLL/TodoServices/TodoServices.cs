using TodoWork.BLL.DTOModels;
using TodoWork.Domain.SQLConnection;

namespace TodoWork.BLL.TodoServices;

public class TodoServices : ITodoServices
{
    private readonly IConnection _connection;
    public List<DTOTodo> CompletedTodos { get; set; }
    public TodoServices(IConnection connection)
    {
        _connection = connection;
    }
    #region ------------------------------------------------------ Task ----------------------------------------------------------
    public async Task CreateTaskAsync(DTOTodo todo, Guid userId) =>
        await _connection.CreateTaskAsync(todo, userId);
    public async Task<DTOTodo> GetTaskByIdAsync(Guid id) =>
         await _connection.GetTaskByIdAsync(id);
    public async Task<List<DTOTodo>> GetAllCompletedTaskByUserIdAsync(Guid id) =>
         await _connection.GetAllCompletedTaskByUserIdAsync(id);
    public async Task UpdateTaskAsync(DTOTodo todo) =>
        await _connection.UpdateTaskAsync(todo);
    public async Task CompletTaskAsync(Guid id) =>
        await _connection.CompletTaskAsync(id);
    public async Task UnCompletedTaskAsync(Guid id) =>
        await _connection.UnCompletedTaskAsync(id);
    public async Task DeleteTaskAsync(Guid id) =>
        await _connection.DeleteTaskAsync(id);
    public async Task DeleteCompletedTaskAsync(Guid id) =>
        await _connection.DeleteTaskAsync(id);
    #endregion

    #region ------------------------------------------------------ User ----------------------------------------------------------
    public async Task CreateUserAsync(DTOUser user) =>
        await _connection.CreateUserAsync(user);
    public async Task UpdateUserAsync(DTOUser user) =>
        await _connection.UpdateUserAsync(user);
    public async Task DeleteUserAsync(Guid id) =>
        await _connection.DeleteUserAsync(id);
    public async Task<DTOUser> UserLoginAsync(string email, string password) =>
        await _connection.UserLoginAsync(email, password);
    public async Task<DTOUser> GetUserByEmailAsync(string email) =>
        await _connection.GetUserByEmailAsync(email);
    public List<DTOTodo> GetTodosByUserId(Guid id) =>
        _connection.GetTodosByUserId(id);
    #endregion
}