using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new ApplicationUser 
            {
                Name="",
                LastName="",
                UserName="",
                Email="",
                PhoneNumber="",
                EmailConfirmed=true
            };
            if(userManager.Users.All(u=>u.Id != user.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(user.Email);
                if(userEmail == null)
                {
                    await userManager.CreateAsync(user,"123Pa$$word!");
                }
            }
        }
    }
}
