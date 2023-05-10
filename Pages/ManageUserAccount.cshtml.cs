using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETMP.Pages
{
    public class ManageUserAccountModel : PageModel
    {
        private List<ETMPUser> _userList;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManageUserAccountModel(ApplicationDbContext context, UserManager<ETMPUser> userManager)
        {
            _context = context;
            _userList = new List<ETMPUser>();
            _userManager = userManager;
        }

        public List<ETMPUser> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            _userList = _context.Users.ToList();
        }

        public IActionResult OnGetFilterOptions(string filterType)
        {
            _userList = _context.Users.ToList();
            var filterOptions = new List<SelectListItem>();

            switch (filterType)
            {
                case "No filter":
                    filterOptions.Add(new SelectListItem { Value = "-", Text = "-" });
                    break;
                case "Gender":
                    var genders = _userList.Select(u => u.Gender).Distinct();
                    genders = genders.Where(i => i != null).ToList();
                    filterOptions.AddRange(genders.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "Organisation":
                    var organisations = _userList.Select(u => u.OrganisationName).Distinct();
                    organisations = organisations.Where(i => i != null).ToList();
                    filterOptions.AddRange(organisations.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "Training Team":
                    var teams = _userList.Select(u => u.TrainingTeamName).Distinct();
                    teams = teams.Where(i => i != null).ToList();
                    filterOptions.AddRange(teams.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                default:
                    break;
            }
            return new JsonResult(filterOptions);
        }
    }
}
