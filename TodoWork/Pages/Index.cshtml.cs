using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;
using TodoWork.SessionHelper;

namespace TodoWork.Pages;

public class IndexModel : PageModel
{
    private readonly ITodoServices _todoServices;

    public IndexModel(ITodoServices todoServices)
    {
        _todoServices = todoServices;
    }
    [BindProperty, EmailAddress]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty, EmailAddress, MaxLength(50), Required]
    public string CreateEmail { get; set; }
    [BindProperty, MaxLength(50), Required]
    public string Name { get; set; }
    [BindProperty, MaxLength(100)]
    public string CreatePassword { get; set; }
    [BindProperty, MaxLength(100), Compare(nameof(CreatePassword), ErrorMessage = "Begge password skal være ens")]
    public string CreatePassword2 { get; set; }
    public DTOUser User { get; set; }
    public IActionResult OnGet()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            return RedirectToPage("/User/UserIndex");
        return Page();
    }
    public async Task<IActionResult> OnPostLogin()
    {
        User = await _todoServices.UserLoginAsync(Email, Password);
        if (User != null)
        {
            HttpContext.Session.SetSessionString(User.Email, "UserEmail");
            return RedirectToPage("/User/UserIndex");
        }
        return Page();
    }
    public void OnPostCreate()
    {
        if (CreatePassword == CreatePassword2)
        {
            User = new()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Email = CreateEmail,
                Password = CreatePassword2,
            };
            _todoServices.CreateUserAsync(User);
        }
    }
}