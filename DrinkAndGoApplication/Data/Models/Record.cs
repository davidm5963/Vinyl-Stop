using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Data.Models
{
    public class Album
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string AlbumLabel { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public int YearReleased { get; set; }

        [Required]
        public string LongDescription { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public bool IsPreferredAlbum { get; set; }

        [Required]
        [Range(0, 5000)]
        public int InStock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
