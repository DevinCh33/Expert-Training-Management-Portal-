using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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

        public TrainingPurchasedModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            _trainingModels = new List<TrainingModel>();
            _userManager = userManager;
            _signInManager = signInManager;
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

            if(user.PurchasedTraining != null)
            {
                string userExistingTraining = user.PurchasedTraining;

                _trainingModels = JsonConvert.DeserializeObject<List<TrainingModel>>(userExistingTraining);
                _trainingModels.Add(PurchasedTraining);

                user.PurchasedTraining = JsonConvert.SerializeObject(_trainingModels);
                userdata = user.PurchasedTraining;

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