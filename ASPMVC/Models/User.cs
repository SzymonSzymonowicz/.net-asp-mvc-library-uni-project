using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Validators;

namespace ASPMVC.Models
{
    public class User
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
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [ValidatePesel("DateOfBirth")]
        public string PESEL { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
