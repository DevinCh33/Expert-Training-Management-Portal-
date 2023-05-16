using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Construction;

namespace ETMP.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ETMPUser> _userManager;
		private readonly SignInManager<ETMPUser> _signInManager;
        public string? role { get; set; }
		public IndexModel(ILogger<IndexModel> logger,ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _logger = logger;
            _context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

        public async Task OnGetAsync()
        {
			var user = await _userManager.GetUserAsync(User);
            if (user != null) {
                foreach (var _user in _context.UserRoles.ToList())
                {
                    if (user.Id == _user.UserId)
                    {
                        if (_user.RoleId == "af6b77bf-e0f4-4df2-839b-b1c1fd3b62c4")
                        {
                            role = "Admin";
                        }
                        else
                        {
                            role = "Member";
                        }
                    }
                }
            }
            else
            {
                role = "Guest";
            }

        }
    }
}