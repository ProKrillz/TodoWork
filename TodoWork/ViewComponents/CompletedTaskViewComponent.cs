using Microsoft.AspNetCore.Mvc;
using TodoWork.BLL.DTOModels;

namespace TodoWork.ViewComponents;

public class CompletedTaskViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(DTOTodo Todo)
    {
        return View(Todo);
    }
}
