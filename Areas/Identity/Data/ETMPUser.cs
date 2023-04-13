using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ETMP.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ETMPUser class
public class ETMPUser : IdentityUser
{
    [PersonalData]
    public string ? CompanyName { get; set; }

}

