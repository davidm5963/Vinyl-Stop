using VinylStop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.ViewModels
{
    public class AlbumListViewModel
    {
        public IEnumerable<Album> Albums { get; set; }

        public string CurrentCategory { get; set; }

    }
}
