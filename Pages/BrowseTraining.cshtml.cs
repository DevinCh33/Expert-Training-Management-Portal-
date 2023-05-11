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
using System.Web.Helpers;

namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class BrowseTraining : PageModel
    {

        public List<TrainingModel> Trainings { get; set; }
        private readonly ApplicationDbContext _context;
        //newly added
        private readonly Services.IMailService _mailService;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        public string data { get; set; }
        public string userdata { get; set; }
        private List<TrainingModel> _trainingModels;
        public BrowseTraining(ApplicationDbContext context, Services.IMailService mailService, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            //newly added
            _mailService = mailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            Trainings = await _context.Trainings.ToListAsync();
            //newly added
            string subject = "Support Ticket On xd";
            string body = "Name of Sender: <br/><br/>Email of Sender: <br/><br/>Description of Issue: ";
            string toEmail = "emtpsdnbhd@gmail.com";
            var request = new MailRequest(toEmail, subject, body, null);
            await _mailService.SendEmailAsync(request);

            return Page();
        }

        
        
    }
}


