using Microsoft.AspNetCore.Mvc;
using AlbumStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.Components
{
    public class AddToCartButton : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IAlbumRepository _albumRepository;

        public AddToCartButton(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public IViewComponentResult Invoke(int amountLeft)
        {
            return View(amountLeft);
        }
    }
}
