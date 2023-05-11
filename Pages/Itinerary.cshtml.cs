using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using ETMP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;




namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class Test1Model : PageModel
    {

        public TrainingModel PurchasedTraining { get; set; }
        private List<TrainingModel> _trainingList;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }



        public Test1Model(IWebHostEnvironment environment, IEmailSender emailSender, ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            _trainingList = new List<TrainingModel>();
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _emailSender = emailSender;
        }

        public List<TrainingModel> TrainingList
        {
            get { return _trainingList; }
            set { _trainingList = value; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.PurchasedTraining != null)
            {
                _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);
            }
        }



        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (UploadedFile != null && UploadedFile.Length > 0)
                {
                    // Get the file name
                    var fileName = Path.GetFileName(UploadedFile.FileName);

                    // Get the file path
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    // Create a stream to save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        // Save the file to the stream
                        await UploadedFile.CopyToAsync(fileStream);
                    }
                }

                string subject = "Test Email";
                string body = "<html><body><p>Please find the content of the script tag below:</p><pre>";

                // Get the content of the script tag
                var scriptContent = "<insert script content here>";

                // Append the script content to the body of the email
                body += scriptContent;

                // Close the HTML tags
                body += "</pre></body></html>";

   /*             var user = await _userManager.GetUserAsync(User);*/
                // Send the email
                await _emailSender.SendEmailAsync(user.Email, subject, body);

                return RedirectToPage("./Index");
            }
        }

        /*        public async Task<IActionResult> OnPostAsync()
                {
                    var user = await _userManager.GetUserAsync(User);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }
                    else
                    {
                        if (UploadedFile != null && UploadedFile.Length > 0)
                        {
                            // Get the file name
                            var fileName = Path.GetFileName(UploadedFile.FileName);

                            // Get the file path
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                            // Create a stream to save the file
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                // Save the file to the stream
                                await UploadedFile.CopyToAsync(fileStream);
                            }
                        }

                        string subject = "Test Email";
                        string body = "<html><body><p>Please find the content of the script tag below:</p><pre>";

                        // Get the content of the script tag
                        var scriptContent = "<insert script content here>";

                        // Append the script content to the body of the email
                        body += scriptContent;

                        // Close the HTML tags
                        body += "</pre></body></html>";

                        // Send the email using the EmailSender class
                        var message = new Message(new string[] { user.Email }, subject, body);
                        await _emailSender.SendEmailAsync(message);

                        return RedirectToPage("./Index");
                    }
                }
        */
    }
}



