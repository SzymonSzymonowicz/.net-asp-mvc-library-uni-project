using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First name must contain only alphabetic characters and start with capital letter.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last name must contain only alphabetic characters and start with capital letter.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public ICollection<BookAuthor> Books { get; set; }
    }
}
