using Microsoft.AspNetCore.Mvc;

namespace AuthenticatedClubManagerMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
