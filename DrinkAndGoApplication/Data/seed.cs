using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using AlbumStore.ViewModels;
using System.Threading.Tasks;
namespace AlbumStore.Data
{
    public class seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //creating a super user who could maintain the web app
            var admin = new ApplicationUser
            {
                FirstName = Configuration.GetSection("AdminSettings")["FirstName"],
                LastName = Configuration.GetSection("AdminSettings")["LastName"],
                UserName = Configuration.GetSection("AdminSettings")["UserEmail"],
                Email = Configuration.GetSection("AdminSettings")["UserEmail"]
            };

            string UserPassword = Configuration.GetSection("AdminSettings")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("AdminSettings")["UserEmail"]);

            if (_user == null)
            {
                var createAdmin = await UserManager.CreateAsync(admin, UserPassword);
                if (createAdmin.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
