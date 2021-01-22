using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public User User { get; set; }
        public ICollection<BookAuthor> Authors { get; set; }
    }
}
