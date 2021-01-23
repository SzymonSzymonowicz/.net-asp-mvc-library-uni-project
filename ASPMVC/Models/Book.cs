using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Validators;

namespace ASPMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Genre must contains at least of 3 characters!")]
        public string Genre { get; set; }
        [Required]
        [ISBN(ErrorMessage = "ISBN13 not valid !")]
        public string ISBN { get; set; }
        public User User { get; set; }
        public ICollection<BookAuthor> Authors { get; set; }
    }
}
