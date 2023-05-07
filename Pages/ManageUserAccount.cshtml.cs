using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    }
}
