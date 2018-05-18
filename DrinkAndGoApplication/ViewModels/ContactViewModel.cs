using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.ViewModels
{
    public class ContactViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(70, ErrorMessage = "Please shorten your name")]
        public String Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(255)]
        public String UserEmail { get; set; }

        [MaxLength(100, ErrorMessage ="Please shorten your subject")]
        [Required]
        public String Subject { get; set; }

        [Required(AllowEmptyStrings = false)]
        public String Message { get; set; }
    }
}
