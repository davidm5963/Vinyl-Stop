using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Interfaces;
using VinylStop.Data.Repositories;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VinylStop.Controllers
{
    public class HomeController : Controller
    {
        //Constructor to inject album repository because they are used on home page
        private readonly IAlbumRepository _albumRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(IAlbumRepository albumRepository, UserManager<ApplicationUser> userManager)
        {
            _albumRepository = albumRepository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredAlbums = _albumRepository.PreferredAlbums
            };

            return View(homeViewModel);

        }
    }
}