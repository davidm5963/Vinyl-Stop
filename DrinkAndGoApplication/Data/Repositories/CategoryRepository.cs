﻿using AlbumStore.Data.Interfaces;
using AlbumStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> Categories => _appDbContext.Categories;

        public Category GetCategoryById(int id) => _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
    }
}
