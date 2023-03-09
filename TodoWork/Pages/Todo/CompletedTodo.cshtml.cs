using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages.Todo;
public class CompletedTodoModel : PageModel
{
    private readonly ITodoServices _todoServices;
    public CompletedTodoModel(ITodoServices service)
    {
        _todoServices= service;
    }
    public List<DTOTodo> CompletedTask { get; set; }
    [BindProperty]
    public Guid Id { get; set; }
    [BindProperty]
    public Guid UserId { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
        {
            DTOUser user =  await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
            CompletedTask = await _todoServices.GetAllCompletedTaskByUserIdAsync(user.Id);
            return Page();
        }
        return RedirectToPage("/Error/NotFound");
    }
    public async Task OnPostUnCompleted()
    {
        await _todoServices.UnCompletedTaskAsync(Id);
        CompletedTask = await _todoServices.GetAllCompletedTaskByUserIdAsync(UserId);
    }
    public async Task OnPostDelete()
    { 
        await _todoServices.DeleteCompletedTaskAsync(Id);
        CompletedTask = await _todoServices.GetAllCompletedTaskByUserIdAsync(UserId);
    }
}
