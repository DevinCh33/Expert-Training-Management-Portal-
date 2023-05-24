using ETMP.Models;
using Microsoft.AspNetCore.Identity;

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
                    PasswordHash = hasher.HashPassword(null, internalPassword),
                    EmailConfirmed = true
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
                    PasswordHash = hasher.HashPassword(null, internalPassword),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, internalPassword);
                    await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("user1@domain") == null)
            {
                var user = new ETMPUser
                {
                    UserName = "user1@domain",
                    Email = "user1@domain",
                    FirstName = "AAA",
                    LastName = "BBB",
                    Gender = "CCC",
                    OrganisationName = "DDD",
                    TrainingTeamName = "EEE",
                    PasswordHash = hasher.HashPassword(null, internalPassword),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, internalPassword);
                    await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("user2@domain") == null)
            {
                var user = new ETMPUser
                {
                    UserName = "user2@domain",
                    Email = "user2@domain",
                    FirstName = "EEE",
                    LastName = "DDD",
                    Gender = "CCC",
                    OrganisationName = "BBB",
                    TrainingTeamName = "AAA",
                    PasswordHash = hasher.HashPassword(null, internalPassword),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //await userManager.AddPasswordAsync(user, internalPassword);
                    await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("eltong85@gmail.com") == null)
            {
                var user = new ETMPUser
                {
                    UserName = "eltong85@gmail.com",
                    Email = "eltong85@gmail.com",
                    FirstName = "Eiton",
                    LastName = "Ng",
                    Gender = "Male",
                    OrganisationName = "Swinburne",
                    TrainingTeamName = "ETMP",
                    PasswordHash = hasher.HashPassword(null, internalPassword),
                    EmailConfirmed = true
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
