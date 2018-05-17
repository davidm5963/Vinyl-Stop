using AlbumStore.Data.Interfaces;
using AlbumStore.Data.Models;
using AlbumStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.ViewModels
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
