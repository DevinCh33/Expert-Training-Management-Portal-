using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ETMP.Areas.Identity.Pages.Account.Roles
{
    public class ETMPRoles : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ETMPRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        // List roles
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }

            return RedirectToAction("Index");
        }
    }
}
