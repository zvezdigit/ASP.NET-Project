using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class UserController: Controller
    {

        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Redirect("/Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Redirect("/Home");
            }

            return View();
        }
    }
}
