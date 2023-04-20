using System.Linq;
using System.Threading.Tasks;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Data
{
    public class IdentitySeedData
    {
        public static async Task InitRolesAndAccount(
            ApplicationDbContext context,
            UserManager<ETMPUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // All the roles within the page
            string adminRole = "Admin";
            string memberRole = "Member";
            string internalPassword = "expert123";
            var hasher = new PasswordHasher<ETMPUser>();

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if(await userManager.FindByNameAsync("admin@internal") == null)
            {
                var user = new ETMPUser
                {
                    UserName = "admin@internal",
                    Email = "admin@internal",
                    PhoneNumber = "1234567890",
                    PasswordHash = hasher.HashPassword(null, internalPassword)
                };

                var result = await userManager.CreateAsync(user);
                if(result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, internalPassword);
                    await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

            if (await userManager.FindByNameAsync("test@internal") == null)
            {
                var user = new ETMPUser
                {
                    UserName = "test@internal",
                    Email = "test@internal",
                    PhoneNumber = "2468101214",
                    PasswordHash = hasher.HashPassword(null, internalPassword)
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, internalPassword);
                    await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }
    }
}
