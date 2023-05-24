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
        /*public string? role { get; set; }*/
		public IndexModel(ILogger<IndexModel> logger,ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _logger = logger;
            _context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public void OnGet()
        {
        }

    }
}