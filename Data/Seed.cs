using JobApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member" };

            IdentityResult roleResult;
            try
            {
                foreach (var roleName in roleNames)
                {
                    // creating the roles and seeding them to the database
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // creating a super user who could maintain the web app
                var poweruser = new ApplicationUser
                {
                    UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                    Email = Configuration.GetSection("UserSettings")["UserEmail"]
                };
                
                string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
                var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

                if (user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                    if (createPowerUser.Succeeded)
                    {
                        // here we assign the "Admin" role to the new user 
                        await UserManager.AddToRoleAsync(poweruser, "Admin");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
