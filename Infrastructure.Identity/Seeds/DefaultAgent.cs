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
    public static class DefaultAgent
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new ApplicationUser
            {
                Name = "",
                LastName = "",
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                Email = "",
                EmailConfirmed = true,
                ImageProfile =""
            };
            if(userManager.Users.All(u=>u.Id != user.Id))
            {
                var userEmail = userManager.FindByEmailAsync(user.Email);
                if(userEmail == null)
                {
                    await userManager.CreateAsync(user,"Password123!");
                    await userManager.AddToRoleAsync(user,Roles.Agente.ToString());
                }
            }
        }
    }
}
