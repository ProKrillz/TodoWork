using Microsoft.AspNetCore.Mvc;
using TodoWork.BLL.DTOModels;

namespace TodoWork.ViewComponents
{
    public class ModalDeleteViewComponent : ViewComponent
    {
        public DTOTodo Dto { get; set; } = new DTOTodo();
        public ModalDeleteViewComponent()
        {
                
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DTOTodo Dto  = new DTOTodo();
            return View(Dto);
        }
    }
}
