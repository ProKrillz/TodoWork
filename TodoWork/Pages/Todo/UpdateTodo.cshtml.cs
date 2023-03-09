using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages.Todo;
public class UpdateTodoModel : PageModel
{
    private readonly ITodoServices _todoServices;
    public UpdateTodoModel(ITodoServices service)
    {
        _todoServices= service;
    }
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public DTOTodo Todo { get; set; }
    [BindProperty]
    public DTOUser User { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
        {
            Todo = await _todoServices.GetTaskByIdAsync(Id);
            return Page();
        }
        return RedirectToPage("/Error/NotFound");
    }
    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _todoServices.UpdateTaskAsync(Todo);
            return RedirectToPage("/User/UserIndex");
        }
        return Page();
    }
}
