using Microsoft.AspNetCore.Mvc;

namespace ETMP.Pages.Modules
{
    public class ModulesController : Controller
    {
        public IActionResult Module1()
        {
            return View();
        }
        public IActionResult Module2()
        {
            return View();
        }

    }
}
