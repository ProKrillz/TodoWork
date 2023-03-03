using Microsoft.AspNetCore.Mvc;

namespace TodoWork.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
