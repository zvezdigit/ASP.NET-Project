using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{
    [Authorize(Roles = AdminAllRoles)]
    [Area("Admin")]
    public class BaseController:Controller
    {

    }
}
