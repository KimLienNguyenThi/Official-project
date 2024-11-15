using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    public class PhieuTraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
