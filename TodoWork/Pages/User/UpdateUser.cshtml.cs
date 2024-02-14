using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.Pages.User
{
    public class UpdateUserModel : PageModel
    {
        private readonly ITodoServices _todoServices;

        public UpdateUserModel(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }
        [BindProperty]
        public DTOUser EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                EditUser = await _todoServices.GetUserByEmailAsync(HttpContext.Session.GetString("UserEmail"));
                return Page();
            }
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            await _todoServices.UpdateUserAsync(EditUser);
            return RedirectToPage("/User/UserIndex");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _todoServices.DeleteUserAsync(EditUser.users_id);
            HttpContext.Session.Remove("UserEmail");
            return RedirectToPage("/Index");
        }
    }
}
