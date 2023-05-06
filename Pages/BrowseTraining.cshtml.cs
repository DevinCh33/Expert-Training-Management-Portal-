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
using ETMP.Hubs;
using System.Configuration;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class BrowseTraining : PageModel
    {

        public List<TrainingModel> Trainings { get; set; }
        private readonly ApplicationDbContext _context;
        private Services.IMailService _mailService;
        private MailRequest _mailRequest;



        public BrowseTraining(ApplicationDbContext context, Services.IMailService mailService, IConfiguration configuration)
        {
            _context = context;
            _mailService = mailService;
            
        }


        public async Task<IActionResult> OnGetAsync()
        {
            Trainings = await _context.Trainings.ToListAsync();
            _mailRequest = new MailRequest("wwonggabriel07@gmail.com", "TestSubject1", "TestBody1", null);
            await _mailService.SendEmailAsync(_mailRequest);
            return Page();
        }

        
        
    }
}


