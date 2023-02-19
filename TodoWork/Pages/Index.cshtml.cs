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
        [BindProperty, Display(Name = "Titel"), Required, MaxLength(100)]
        public string? Title { get; set; }
        [BindProperty, Display(Name = "Beskrivelse"), Required]
        public string? Description { get; set; }
        public DTOTodo Todo { get; set; }

        public void OnGet()
        {
            Todos = _todoServices.GetAllTask();
        }
        public void OnPostCreate()
        {
            _todoServices.CreateTask(new DTOTodo()
            {
                Id = Guid.NewGuid(),
                Title = Title,
                Description = Description,
                TaskPriority = (DTOTodo.Priority)2,
                Created = DateTime.Now,
            });
            Todos = _todoServices.GetAllTask();
        }
        public void OnPostDelete()
        {
            _todoServices.DeleteTask(Id);
            Todos = _todoServices.GetAllTask();
        }
        public void OnPostCompleted()
        {
            _todoServices.CompletTask(Id);
            Todos = _todoServices.GetAllTask();

        }
    }
}