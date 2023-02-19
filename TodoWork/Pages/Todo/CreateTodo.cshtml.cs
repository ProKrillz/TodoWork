using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoWork.Pages.Todo
{
    public class CreateTodoModel : PageModel
    {
        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Description { get; set; }
        public void OnGet()
        {

        }
        public void OnPost() { 
            
        }
    }
}
