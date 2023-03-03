using Microsoft.AspNetCore.Mvc;
using TodoWork.BLL.DTOModels;
using TodoWork.BLL.TodoServices;

namespace TodoWork.ViewComponents;

public class TaskViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(DTOTodo Todo)
	{
		return View(Todo);
	}
}
