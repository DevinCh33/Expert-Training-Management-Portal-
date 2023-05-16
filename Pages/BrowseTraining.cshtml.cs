using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class BrowseTraining : PageModel
    {
        [BindProperty]
        public List<TrainingModel> Trainings { get; set; }
        private readonly ApplicationDbContext _context;
        //newly added
        private readonly Services.IMailService _mailService;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        public string data { get; set; }
        public string userdata { get; set; }
        private List<TrainingModel> _trainingModels;
        public BrowseTraining(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            //newly added
            
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            Trainings = await _context.Trainings.ToListAsync();


            return Page();
        }

        
        
    }
}


