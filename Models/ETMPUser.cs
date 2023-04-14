using Microsoft.AspNetCore.Identity;

namespace ETMP.Models
{
    public class ETMPUser : IdentityUser
    {
        public string CompanyName { get; set; }
        public string CompanyMailingAddress { get; set; }
    }
}
