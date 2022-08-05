using Core.Application.Enum;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultClient
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user =  new ApplicationUser
            {
                Name ="",
                LastName = "",
                Email = "",
                PhoneNumber = "",
                ImageProfile = "",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            if(userManager.Users.All(u=>u.Id != user.Id))
            {
                var userEmail = userManager.FindByEmailAsync(user.Email);
                if (userEmail == null)
                {
                    await userManager.CreateAsync(user,"123Pa$$word!");
                    await userManager.AddToRoleAsync(user,Roles.Cliente.ToString());
                 }

            }
        }
    }
}
