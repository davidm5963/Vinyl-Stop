using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> Categories => _appDbContext.Categories;

        public Category GetCategoryById(int id) => Categories.FirstOrDefault(c => c.CategoryId == id);
    }
}
