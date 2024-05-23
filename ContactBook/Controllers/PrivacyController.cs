using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
