using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


/*using iTextSharp.text;
using iTextSharp.text.pdf;*/
/*using PuppeteerSharp;
using MimeKit;*/
/*using PdfSharp;
using PdfSharp.Pdf;*/
/*using PdfSharp.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;*/


/*using System.Web.Mvc;
using Rotativa;*/



namespace ETMP.Pages
{
    public class UserEditTrainingModel : PageModel
    {
        [BindProperty]
        public TrainingModel EditTraining { get; set; }
        public string ErrorMsg { get; set; }
        public System.DateTime ReleaseDate { get; set; } = System.DateTime.Now;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly Services.IMailService _mailService;

        /*private readonly IUserStore<ETMPUser> _userStore;
        private readonly IUserEmailStore<ETMPUser> _emailStore;
        private readonly ILogger<UserEditTrainingModel> _logger;
        private readonly Services.IMailService _mailService;*/

        public UserEditTrainingModel(Services.IMailService mailService, ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mailService = mailService;


        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user.PurchasedTraining != null)
            {
                var _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);

                foreach (var i in _trainingList)
                {
                    if (i.Id == id)
                    {
                        EditTraining = i;
                        break;
                    }
                }
            }
            return Page();
        }

        public IActionResult OnGetDownload(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest();
            }

            var path = Path.Combine("uploads", filePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }


        public IActionResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest();
            }

            var path = Path.Combine("uploads", filePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

        public IActionResult OnPostCancelButton()
        {
            return RedirectToPage("/UserListOfTraining");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);

            if (EditTraining.TrainingStartDateTime >= EditTraining.TrainingEndDateTime)
            {
                ErrorMsg = "Please select an appropriate date";
                return Page();
            }

            foreach (var edited in _trainingList)
            {
                if (edited.Id == id)
                {
                    edited.TrainingItinerary = "To be Removed";
                    edited.TrainingVenue = EditTraining.TrainingVenue;
                    edited.TrainingStartDateTime = EditTraining.TrainingStartDateTime;
                    edited.TrainingEndDateTime = EditTraining.TrainingEndDateTime;

                    foreach (var role in _context.UserRoles.ToList())
                    {
                        foreach (var person in _context.Users.ToList())
                        {
                            if (person.Id == role.UserId)
                            {
                                if (role.RoleId == "af6b77bf-e0f4-4df2-839b-b1c1fd3b62c4")
                                {
                                    Notification notiToAdmin = new Notification();
                                    notiToAdmin.ToUserId = person.Id;
                                    notiToAdmin.ToUserName = person.UserName;
                                    notiToAdmin.NotificationHeader = "Training Venue and/or Training date changed";
                                    notiToAdmin.NotificationBody = user.UserName + " with the id of " + user.Id + " has changed his/her training!\n" +
                                    "Training Edited: " + edited.TrainingName + "\n" +
                                    "New Training Venue " + edited.TrainingVenue + "\n" +
                                    "New Training Start Date " + edited.TrainingStartDateTime + "\n" +
                                    "New Training End Date " + edited.TrainingEndDateTime + "\n";

                                    notiToAdmin.NotificationDate = DateTime.Now;
                                    _context.Notification.Add(notiToAdmin);

                                    string subj = notiToAdmin.NotificationHeader;
                                    string Body = notiToAdmin.NotificationBody;
                                    string toAdminEmail = person.Email;
                                    var mailRequest = new MailRequest(toAdminEmail, subj, Body, null);
                                    await _mailService.SendEmailAsync(mailRequest);

                                }
                            }
                        }
                    }
                }
            }
            var toSave = JsonConvert.SerializeObject(_trainingList);

            user.PurchasedTraining = toSave;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("./UserListOfTraining");
        }
    }
}  
