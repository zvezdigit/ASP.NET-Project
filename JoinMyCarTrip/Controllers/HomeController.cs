using JoinMyCarTrip.Application.Constants;
using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Data.Entities;
using JoinMyCarTrip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;


        public HomeController(
            ILogger<HomeController> _logger, 
            SignInManager<ApplicationUser> _signInManager, 
            UserManager<ApplicationUser> _userManager) 
        {
            logger = _logger;
            signInManager = _signInManager;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData[MessageConstant.SuccessMessage] = "Welcome";

            if (signInManager.IsSignedIn(User))
            {
                ApplicationUser user = await userManager.GetUserAsync(User);

                if (await IsAdminAsync(user))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<bool> IsAdminAsync(ApplicationUser user)
        {
            return await userManager.IsInRoleAsync(user, SuperAdminRole) ||
                    await userManager.IsInRoleAsync(user, AdminRole);
        }
    }
}