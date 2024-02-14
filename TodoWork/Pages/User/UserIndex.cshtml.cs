using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages.User;
public class UserIndexModel : PageModel
{
    private readonly ITodoServices _todoServices;
    public UserIndexModel(ITodoServices todoServices) => 
        _todoServices = todoServices;

    [BindProperty]
    public new DTOUser User { get; set; }
    [BindProperty]
    public DTOTodo Todo { get; set; }
    [BindProperty]
    public Guid Id { get; set; }
    [BindProperty]
    public Guid UserId { get; set; }
    public bool Created { get; set; }
    public bool Deleted { get; set; }
    public bool Completed { get; set; }
    public async Task OnGet()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
        {
            User = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
            User.Todos = _todoServices.GetTodosByUserId(User.users_id);
        }
    }
    public async Task OnPostCreate()
    {
        await _todoServices.CreateTaskAsync(new()
        {
            Title = Todo.Title,
            Description = Todo.Description,
            TaskPriority = Todo.TaskPriority,
            Created = DateTime.Now,
        }, UserId);
        User = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
        User.Todos = _todoServices.GetTodosByUserId(UserId);
        Created = true;
    }
    public async Task OnPostDelete()
    {
        await _todoServices.DeleteTaskAsync(Id);
        User = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
        User.Todos = _todoServices.GetTodosByUserId(UserId);
        Deleted = true;
    }
    public async Task OnPostCompleted()
    {
        await _todoServices.CompletTaskAsync(Id);
        User = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
        User.Todos = _todoServices.GetTodosByUserId(UserId);
        Completed = true;
    }
    public IActionResult OnPostLogOut()
    {
        HttpContext.Session.Remove("UserEmail");
        return RedirectToPage("/Index");
    }
}