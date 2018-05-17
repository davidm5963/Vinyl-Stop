using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICategoryRepository _categoryRepository;

        public AlbumRepository(AppDbContext dbContext, ICategoryRepository categoryRepository)
        {
            _appDbContext = dbContext;
            _categoryRepository = categoryRepository;
        }
        public IEnumerable<Album> Albums => _appDbContext.Albums.Include(c => c.Category).OrderBy(c => c.Name);
        public IEnumerable<Album> PreferredAlbums => _appDbContext.Albums.Where(p => p.IsPreferredAlbum).Include(p => p.Category);

        public Album GetAlbumById(int albumId) => _appDbContext.Albums.Include(c => c.Category).FirstOrDefault(p => p.AlbumId == albumId);

        public void DeleteAlbum(int albumId)
        {
            Album albumToBeDeleted = GetAlbumById(albumId);
            _appDbContext.Albums.Remove(albumToBeDeleted);
            _appDbContext.SaveChanges();
        }

        public void CreateAlbum(Album album)
        {
            var newAlbum = new Album
            {
                Name = album.Name,
                Category = _categoryRepository.GetCategoryById(album.CategoryId),
                CategoryId = album.CategoryId,
                Artist = album.Artist,
                LongDescription = album.LongDescription,
                ImageUrl = album.ImageUrl,
                IsPreferredAlbum = album.IsPreferredAlbum,
                Price = album.Price,
                InStock = album.InStock,
                YearReleased = album.YearReleased,
                AlbumLabel = album.AlbumLabel
            };

            _appDbContext.Albums.Add(newAlbum);
            _appDbContext.SaveChanges();
        }

        public void EditAlbum(Album newAlbum)
        {
            var oldAlbum = GetAlbumById(newAlbum.AlbumId);

            oldAlbum.Name = newAlbum.Name;
            oldAlbum.Category = newAlbum.Category;
            oldAlbum.CategoryId = newAlbum.CategoryId;
            oldAlbum.Artist = newAlbum.Artist;
            oldAlbum.LongDescription = newAlbum.LongDescription;
            oldAlbum.ImageUrl = newAlbum.ImageUrl;
            oldAlbum.IsPreferredAlbum = newAlbum.IsPreferredAlbum;
            oldAlbum.Price = newAlbum.Price;
            oldAlbum.InStock = newAlbum.InStock;

            _appDbContext.SaveChanges();
        }

        public void AddCategories()
        {
            foreach(var album in Albums)
            {
                if(album.Category == null)
                {
                    album.Category = _categoryRepository.GetCategoryById(album.CategoryId);
                    _appDbContext.SaveChanges();
                }
            }
        }
    }
}
