using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Components
{
    public class CategoryMenu : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categories = CategoryRepository.Categories.OrderBy(p => p.CategoryName);

            return View(categories);

        }
    }
}
