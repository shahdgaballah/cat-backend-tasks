using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystemMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
