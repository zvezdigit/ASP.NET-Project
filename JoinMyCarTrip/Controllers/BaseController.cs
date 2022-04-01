using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JoinMyCarTrip.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        protected ApplicationUser ApplicationUser { get; private set; }

        public BaseController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                ApplicationUser = await userManager.GetUserAsync(context.HttpContext.User);

                ViewBag.UserProfile = new
                {
                    ApplicationUser.Id,
                    ApplicationUser.FullName,
                    ApplicationUser.Email
                };
            }

            await next();
        }
    }
}
