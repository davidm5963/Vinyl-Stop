﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using VinylStop.Data.Repositories;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VinylStop.Controllers
{
    public class AlbumController : Controller
    {

        private readonly IAlbumRepository _albumRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AlbumController(IAlbumRepository albumRepository, ICategoryRepository categoryRepository)
        {
            _albumRepository = albumRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(int categoryId = 0)
        {
            var currentCategory = string.Empty;
            IEnumerable<Album> albums;

            if(categoryId != 0)
            {
                albums = _albumRepository.Albums.OrderBy(n => n.Name).Where(n => n.Category.CategoryId == categoryId).ToList();
                try
                {
                    currentCategory = _categoryRepository.GetCategoryById(categoryId).CategoryName;
                }
                catch
                {
                    ViewData["NoResults"] = "Sorry! There are currently no albums in that category";
                }

                if (albums.Count() <= 0)
                {
                    ViewData["NoResults"] = "Sorry! There are currently no albums in that category";
                }
            }
            else
            {
                albums = _albumRepository.Albums.OrderBy(n => n.Name);
                currentCategory = "All Albums";
            }

            AlbumListViewModel vm = new AlbumListViewModel
            {
                Albums = albums,
                CurrentCategory = currentCategory
            };

            return View(vm);
        }

        [HttpPost]
        public ViewResult List(string searchString)
        {
            string currentCategory = string.Empty;
            IEnumerable<Album> albums = null;

            if (string.IsNullOrEmpty(searchString))
            {
                albums = _albumRepository.Albums.OrderBy(n => n.AlbumId);
                currentCategory = "All Albums";
            }
            else
            {
                albums = _albumRepository.Albums.OrderBy(d => d.Name).Where(d => d.Name.ToLower().Contains(searchString.ToLower()) ||
                                                                                   d.Artist.ToLower().Contains(searchString.ToLower()));

                currentCategory = "Search Result";

                if(albums.Count() <= 0)
                {
                    ViewBag.NoResults = "Sorry! We could find any albums matching the search: " + searchString;
                }
            }

            AlbumListViewModel vm = new AlbumListViewModel
            {
                Albums = albums,
                CurrentCategory = currentCategory
            };

            return View(vm);
        }

        public ViewResult AlbumDetails(int id)
        {
            Album album = _albumRepository.Albums.FirstOrDefault(d => d.AlbumId == id);
            return View(album);
        }

        [Authorize(Roles = "Admin")]
        public ViewResult AdminList()
        {
            AlbumListViewModel albumListViewModel = new AlbumListViewModel
            {
                Albums = _albumRepository.Albums,
                CurrentCategory = "All Albums"
            };
            return View(albumListViewModel);
        }

        [Authorize(Roles="Admin")]
        public ViewResult CreateAlbum() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateAlbum(Album album)
        {
            
            if(ModelState.IsValid)
            {

                _albumRepository.CreateAlbum(album);
                return RedirectToAction("AdminList");
            }
            else
            {
                return View(album);
            }
        }

        [Authorize(Roles = "Admin")]
        public ViewResult EditAlbum(int albumId) => View(_albumRepository.GetAlbumById(albumId));

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                _albumRepository.EditAlbum(album);
                return RedirectToAction("AdminList");
            }
            else
            {
                return View(album);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAlbumConfirmation(int albumId)
        {
            var albumToBeDeleted = _albumRepository.GetAlbumById(albumId);

            return View(albumToBeDeleted);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAlbumConfirmed(int albumId)
        {
            try
            {
                _albumRepository.DeleteAlbum(albumId);
            }
            catch
            {

            }
            return RedirectToAction("AdminList");
        }

    }
}