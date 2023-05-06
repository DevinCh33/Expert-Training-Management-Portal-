using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class SupportTicketModel : PageModel
    {
        private string _name;
        private string _email;
        private string _about;
        private string _description;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string About
        {
            get { return _about; }
            set { _about = value; }
        }

        private string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public void OnGet()
        {
        }
    }
}
