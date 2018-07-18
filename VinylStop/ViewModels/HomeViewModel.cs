using VinylStop.Data.Models;
using System.Collections.Generic;

namespace VinylStop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Album> PreferredAlbums { get; set; }
    }
}
