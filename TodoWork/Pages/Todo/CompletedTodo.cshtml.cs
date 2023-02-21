using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;
using TodoWork.Domain.Entities;

namespace TodoWork.Pages.Todo
{
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
        public void OnGet()
        {
            CompletedTask = _todoServices.GetAllCompletedTask();
        }
        public void OnPostUnCompleted()
        {
            _todoServices.UnCompletedTaskAsync(Id);
            CompletedTask = _todoServices.GetAllCompletedTask().OrderBy(x => x.TaskPriority).ToList();
        
        }
        public void OnPostDelete()
        { 
            _todoServices.DeleteCompletedTaskAsync(Id);
            CompletedTask = _todoServices.GetAllCompletedTask().OrderBy(x => x.TaskPriority).ToList();
        }
    }
}
