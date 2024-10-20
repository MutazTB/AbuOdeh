using Microsoft.AspNetCore.Mvc;

namespace AbuOdeh_Electromechanical.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
