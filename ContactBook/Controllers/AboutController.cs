using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
