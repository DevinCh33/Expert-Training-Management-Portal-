using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace ETMP.Pages
{

    [Authorize(Roles = "Admin, Member")]
    public class UserListOfTrainingModel : PageModel
    {
        public TrainingModel PurchasedTraining { get; set; }
        private List<TrainingModel> _trainingList;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserListOfTrainingModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _context = context;
            _trainingList = new List<TrainingModel>();
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<TrainingModel> TrainingList
        {
            get { return _trainingList; }
            set { _trainingList = value; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user.PurchasedTraining != null){
                _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);
            }
        }
    }
}
