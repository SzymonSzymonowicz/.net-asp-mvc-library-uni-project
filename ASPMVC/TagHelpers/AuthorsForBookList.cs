using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Data;
using ASPMVC.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace ASPMVC.TagHelpers
{
    [HtmlTargetElement("authors", Attributes = "bookId")]
    public class AuthorsForBookList : TagHelper
    {
        private readonly ApplicationDbContext _context;
        
        public AuthorsForBookList(ApplicationDbContext context)
        {
            _context = context;
        }

        [HtmlAttributeName("bookId")]
        public int BookId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var authors = _context.BookAuthors
                .Include(ba => ba.Author)
                .Where(ba => ba.BookId == BookId)
                .Select(a => a.Author);

            List<string> names = new List<string>();
            
            if (authors.Any())
            {
                foreach (var author in authors)
                {
                    var authorName = author.FirstName + " " + author.LastName;
                    names.Add(authorName);
                }
            }
            
            output.Content.SetContent(names.Any() ? string.Join(", ", names) : "No authors");
        }

    }
}
