using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ETMP.Pages
{
    public class ManageUserAccountModel : PageModel
    {
        private List<ETMPUser> _userList;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ManageUserAccountModel> _logger;

        public ManageUserAccountModel(ApplicationDbContext context, UserManager<ETMPUser> userManager, ILogger<ManageUserAccountModel> logger)
        {
            _context = context;
            _userList = new List<ETMPUser>();
            _userManager = userManager;
            _logger = logger;
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
                case "NoFilter":
                    filterOptions.Add(new SelectListItem { Value = "-", Text = "-" });
                    break;
                case "Gender":
                    var genders = _userList.Select(u => u.Gender).Distinct();
                    genders = genders.Where(i => i != null).ToList();
                    filterOptions.AddRange(genders.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "OrganisationName":
                    var organisations = _userList.Select(u => u.OrganisationName).Distinct();
                    organisations = organisations.Where(i => i != null).ToList();
                    filterOptions.AddRange(organisations.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                case "TrainingTeamName":
                    var teams = _userList.Select(u => u.TrainingTeamName).Distinct();
                    teams = teams.Where(i => i != null).ToList();
                    filterOptions.AddRange(teams.Select(g => new SelectListItem { Value = g, Text = g }));
                    break;
                default:
                    break;
            }
            return new JsonResult(filterOptions);
        }

        public IActionResult OnGetFilterTable(string filterColumn, string filterValue)
        {
            _userList = _context.Users.ToList();
            var filteredUsers = _userList.Where(u => u.GetType().GetProperty(filterColumn)?.GetValue(u)?.ToString() == filterValue).ToList();
            _logger.LogInformation("Filter List Request Responded, Return Size: " + filteredUsers.Count);
            foreach(var user in filteredUsers)
            {
                _logger.LogInformation("Email: " + user.Email);
                _logger.LogInformation("First Name: " + user.FirstName);
                _logger.LogInformation("Last Name: " + user.LastName);
                _logger.LogInformation("Gender: " + user.Gender);
                _logger.LogInformation("Organisation: " + user.OrganisationName);
                _logger.LogInformation("Team: " + user.TrainingTeamName);
            }
            if (filterColumn == "NoFilter")
            {
                filteredUsers = _userList;
            }
            return new JsonResult(filteredUsers);  
        }
    }
}
