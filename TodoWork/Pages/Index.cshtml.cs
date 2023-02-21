using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ITodoServices _todoServices;
        public IndexModel(ILogger<IndexModel> logger, ITodoServices iTodoServices)
        {
            _logger = logger;
            _todoServices = iTodoServices;
        }
        public List<DTOTodo> Todos { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public DTOTodo Todo { get; set; }

        public void OnGet()
        {
            Todos = _todoServices.GetAllTask()
                .OrderBy(x => x.TaskPriority).ToList();
        }
        public void OnPostCreate()
        {
            _todoServices.CreateTaskAsync(new DTOTodo()
            {
                Id = Guid.NewGuid(),
                Title = Todo.Title,
                Description = Todo.Description,
                TaskPriority = (DTOTodo.Priority)2,
                Created = DateTime.Now,
            });
            Todos = _todoServices.GetAllTask().OrderBy(x => x.TaskPriority).ToList();
        }
        public void OnPostDelete()
        {
            _todoServices.DeleteTaskAsync(Id);
            Todos = _todoServices.GetAllTask().OrderBy(x => x.TaskPriority).ToList();
        }
        public void OnPostCompleted()
        {
            _todoServices.CompletTaskAsync(Id);         
            Todos = _todoServices.GetAllTask().OrderBy(x => x.TaskPriority).ToList();          
        }
    }
}