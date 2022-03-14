using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{
    public class HomeController: Controller
    {

        public IActionResult Index()
        {

            return View();
        }
    }
}
