using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin , Customer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
