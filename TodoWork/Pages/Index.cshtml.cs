﻿using Microsoft.AspNetCore.Mvc;
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
    [BindProperty, MaxLength(100), DataType(DataType.Password)]
    public string Password { get; set; }
    [BindProperty, EmailAddress, MaxLength(50), Required]
    public string CreateEmail { get; set; }
    [BindProperty, MaxLength(50), Required]
    public string Name { get; set; }
    [BindProperty, MaxLength(100), DataType(DataType.Password)]
    public string CreatePassword { get; set; }
    [BindProperty, MaxLength(100), DataType(DataType.Password), Compare(nameof(CreatePassword), ErrorMessage = "Begge password skal være ens")]
    public string CreatePassword2 { get; set; }
    public new DTOUser User { get; set; }
    public bool Login { get; set; } = true;
    public bool Created { get; set; } = false;
    public IActionResult OnGet()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            return RedirectToPage("/User/UserIndex");
        return Page();
    }
    public async Task<IActionResult> OnPostLogin()
    {
        User = await _todoServices.UserLoginAsync(Email, Password);
        if (User.users_email is not null)
        {
            HttpContext.Session.SetSessionString(User.users_email, "UserEmail");
            return RedirectToPage("/User/UserIndex");
        }
        Login = false;
        return Page();
    }
    public void OnPostCreate()
    {
        if (CreatePassword == CreatePassword2)
        {
            _todoServices.CreateUserAsync(new()
            {
                users_name = Name,
                users_email = CreateEmail,
                users_password = CreatePassword2
            });
            Created = true;
        }
    }
}