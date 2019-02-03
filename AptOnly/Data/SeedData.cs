using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Data
{
    public class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("GenericUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "GenericUser";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(userManager.FindByNameAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost.com";

                IdentityResult result = userManager.CreateAsync(user, "Kos100##").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if(userManager.FindByNameAsync("generic@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "generic@localhost";
                user.Email = "generic@localhost.com";

                IdentityResult result = userManager.CreateAsync(user, "Kos200##").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "GenericUser").Wait();
                }
            }
        }


    }
}
