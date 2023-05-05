using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using ETMP.Services;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class BrowseTraining : PageModel
    {
        
        public List<TrainingModel> Trainings { get; set; }
        private readonly ApplicationDbContext _context;


        public BrowseTraining(ApplicationDbContext context)
        {
            
            _context = context;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            Trainings = await _context.Trainings.ToListAsync();
            return Page();
        }
    }
}


