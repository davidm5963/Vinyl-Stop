using AlbumStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.Data.Interfaces
{

    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        Category GetCategoryById(int id);
    }
}
