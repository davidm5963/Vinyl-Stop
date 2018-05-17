using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using VinylStop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.ViewModels
{
    public class AlbumViewModel
    {
        public ICategoryRepository _categoryRepository { get; set; }

        public AlbumViewModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
