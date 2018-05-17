using AlbumStore.Data.Models;
using System.Collections.Generic;

namespace AlbumStore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Album> PreferredAlbums { get; set; }
    }
}
