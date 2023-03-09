using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;
using TodoWork.Domain.Entities;

namespace TodoWork.Pages.User
{
    public class UserIndexModel : PageModel
    {
        private readonly ITodoServices _todoServices;
        public UserIndexModel(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }
        [BindProperty]
        public DTOUser User { get; set; }
        [BindProperty]
        public DTOTodo Todo { get; set; }
        [BindProperty]
        public Guid Id { get; set; }
        [BindProperty]
        public Guid UserId { get; set; }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                User = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
                User.Todos = _todoServices.GetTodosByUserId(User.Id);
            }
        }
        public void OnPostCreate()
        {
            _todoServices.CreateTaskAsync(new DTOTodo()
            {
                Id = Guid.NewGuid(),
                Title = Todo.Title,
                Description = Todo.Description,
                TaskPriority = Todo.TaskPriority,
                Created = DateTime.Now,
            }, UserId);
            User.Todos = _todoServices.GetTodosByUserId(UserId);

        }
        public void OnPostDelete()
        {
            _todoServices.DeleteTaskAsync(Id);
            User.Todos = _todoServices.GetTodosByUserId(UserId);
        }
        public void OnPostCompleted()
        {
            _todoServices.CompletTaskAsync(Id);
            User.Todos = _todoServices.GetTodosByUserId(UserId);
        }
    }
}
