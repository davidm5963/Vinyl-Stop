using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStop.Data.Models;

namespace VinylStop.Controllers
{
    [Authorize(Roles="Admin")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> AllUsers()
        {
            var users = _userManager.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>(); 
            foreach (var user in users)
            {
                var UserVm = new UserViewModel
                {
                    User = user,
                    UserRoles = await _userManager.GetRolesAsync(user)
                };
                userViewModels.Add(UserVm);
            }
            var userListViewModel = new UserListViewModel()
            {
                Users = userViewModels
            };

            return View(userListViewModel);
        }
    }
}