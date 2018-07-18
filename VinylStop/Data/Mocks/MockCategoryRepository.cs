using DrinkAndGoApplication.Data.Interfaces;
using DrinkAndGoApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGoApplication.Data
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category {CategoryName = "Alcoholic", Description = "All alcoholic drinks" },
                    new Category {CategoryName = "Non-Alcoholic", Description = "All non-alcoholic drinks" }
                };
            }
        }
    }
}
