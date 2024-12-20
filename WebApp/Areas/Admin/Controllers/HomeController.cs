using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("admin/homeadmin")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }

    }
}
