using Microsoft.AspNetCore.Identity;

namespace ETMP.Models
{
    public class ETMPUser : IdentityUser
    {
        public string? OrganisationName { get; set; }
        public string? OrganisationMailingAddress { get; set; }
        public string? TrainingTeamName { get; set; }
        public string? Gender { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
