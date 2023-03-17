using Microsoft.AspNetCore.Mvc;
using TodoWork.BLL.DTOModels;

namespace TodoWork.ViewComponents
{
    public class ToastViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string[] text)
        {
            return View(text);
        }
    }
}
