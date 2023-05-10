using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
/*using Microsoft.AspNetCore.Mvc;*/
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Rotativa;
using System.Data;
using System.Web.Mvc;





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

        public Test1Model(ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
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
            if (user.PurchasedTraining != null)
            {
                _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);
            }
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

        }

        public class HomeController : Controller
        {
            

            public ActionResult ConvertToPDF()
            {
                var printpdf = new ActionAsPdf("TEST1");
                return printpdf;
            }
        }



    }
}




