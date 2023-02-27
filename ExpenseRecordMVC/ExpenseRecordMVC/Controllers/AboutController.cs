using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecordMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View("About");
        }
    }
}
