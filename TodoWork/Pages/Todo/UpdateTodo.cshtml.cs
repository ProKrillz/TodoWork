using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages.Todo
{
    public class UpdateTodoModel : PageModel
    {
        private readonly ITodoServices _todoServices;
        public UpdateTodoModel(ITodoServices service)
        {
            _todoServices= service;
        }
        [BindProperty(SupportsGet = true)]
        public Guid id { get; set; }
        [BindProperty]
        public DTOTodo? Todo { get; set; }
        public void OnGet()
        {
            Todo = _todoServices.GetAllTask().Find(x => x.Id == id);
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _todoServices.UpdateTask(Todo);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
