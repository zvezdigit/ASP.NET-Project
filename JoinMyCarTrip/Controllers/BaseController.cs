using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public BaseController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        protected async Task<string> GetUserIdAsync()
        {
            return (await userManager.GetUserAsync(User)).Id;
        }
    }
}
