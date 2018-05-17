using AlbumStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.Data.Interfaces
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> Albums { get; }

        IEnumerable<Album> PreferredAlbums { get; }

        Album GetAlbumById(int albumId);

        void DeleteAlbum(int albumId);

        void CreateAlbum(Album album);
        void EditAlbum(Album album);

        void AddCategories();
    }
}
