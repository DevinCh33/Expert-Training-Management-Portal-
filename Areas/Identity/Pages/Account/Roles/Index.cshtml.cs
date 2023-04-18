using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ETMP.Models;

namespace ETMP.Areas.Identity.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;



        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }

        private async Task LoadAsync(IdentityRole role)
        {
            var roleId = await _roleManager.GetRoleIdAsync(role);
            var roleName = await _roleManager.GetRoleNameAsync(role);

            RoleId = roleId;
            RoleName = roleName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var roles = _roleManager.Roles;

            //await LoadAsync(roles);
            return Page();
        }
    }
}
