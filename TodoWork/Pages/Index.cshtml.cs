using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {
            Todos = _todoServices.GetAllTask();
        }
    }
}