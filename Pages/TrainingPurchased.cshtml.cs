using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using PuppeteerSharp;

namespace ETMP.Pages
{
    [BindProperties(SupportsGet = true)]
    public class TrainingPurchasedModel : PageModel
    {
        public TrainingModel PurchasedTraining { get; set; }
        public string data { get; set; }
        public string userdata { get; set; }
        private List<TrainingModel> _trainingModels;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly Services.IMailService _mailService;
        public Notification notification { get; set; }

        public TrainingPurchasedModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager, Services.IMailService mailService)
        {
            _context = context;
            _trainingModels = new List<TrainingModel>();
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        public List<TrainingModel> TrainingModels
        {
            get { return _trainingModels; }
            set { _trainingModels = value; }
        }

        public async Task OnGetAsync()
        {
            data = Request.Query["Training"];
            PurchasedTraining = (TrainingModel)JsonConvert.DeserializeObject(data, typeof(TrainingModel));

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                if (user.PurchasedTraining != null)
                {
                    string userExistingTraining = user.PurchasedTraining;
                    
                    _trainingModels = JsonConvert.DeserializeObject<List<TrainingModel>>(userExistingTraining);

                    //notification
                    notification = new Notification();
                    notification.ToUserId = user.Id;
                    notification.ToUserName = user.UserName;
                    notification.NotificationHeader = "Training Purchased!";
                    notification.NotificationBody = "Training " + PurchasedTraining.TrainingName + " has been purchased! The date for the training is "+PurchasedTraining.TrainingStartDateTime;
                    notification.NotificationDate = DateTime.Now;
                    _context.Notification.Add(notification);
                    //send email after purchasing training
                    string subject = notification.NotificationHeader;
                    string body = notification.NotificationBody;
                    string toEmail = user.Email;
                    var request = new MailRequest(toEmail, subject, body, null);
                    await _mailService.SendEmailAsync(request);
                    //notification ends
                    _trainingModels.Add(PurchasedTraining);
                    
 
                    user.PurchasedTraining = JsonConvert.SerializeObject(_trainingModels);
                    userdata = user.PurchasedTraining;
                    //
                    

                    foreach (TrainingModel training in _trainingModels)
                    {
                        //if training is starting within 7 days
                        if (training.TrainingStartDateTime >= DateTime.Today &&
                            training.TrainingStartDateTime <= DateTime.Today.AddDays(7))
                        {
                            Notification noti = new Notification();
                            noti.ToUserId = user.Id;
                            noti.ToUserName = user.UserName;
                            noti.NotificationHeader = "Your training is coming soon!";
                            noti.NotificationBody = "Training " + training.TrainingName + " is coming soon! The date for the training is " + training.TrainingStartDateTime;
                            noti.NotificationDate = DateTime.Now;
                            _context.Notification.Add(noti);

                            string subj = noti.NotificationHeader;
                            string Body = noti.NotificationBody;
                            string toUserEmail = user.Email;
                            var mailRequest = new MailRequest(toUserEmail, subj, Body, null);
                            await _mailService.SendEmailAsync(mailRequest);
                        }
                    }
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                }
                else
                {
                    user.PurchasedTraining = "[" + JsonConvert.SerializeObject(PurchasedTraining) + "]";                    
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                }
            }
        }   
    }
}
