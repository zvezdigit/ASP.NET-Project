using JoinMyCarTrip.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController:Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService _userService)
        {
            userService = _userService;
        }
        public async Task<IActionResult> Users()
        {
            var users = await userService.GetUsers();

            return View(users);
        }


        public IActionResult Trips()
        {
            return View();
        }
    }
}
