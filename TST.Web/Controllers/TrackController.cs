using Microsoft.AspNetCore.Mvc;

namespace TST.Web.Controllers
{
    public class TrackController : Controller
    {
        public IActionResult Tracks()
        {
            return View();
        }
    }
}
