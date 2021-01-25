using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASPMVC.Controllers
{
    public class MyBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Books.ToListAsync());
        // }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        //
        // public IActionResult AvailableBooks()
        // {
        //     // get all books that dont have asigned user
        //     var availableBooks = 
        //         from book in _context.Books 
        //         where book.User == null 
        //         select book;
        //
        //     return View(availableBooks);
        // }
        //
        // public IActionResult MyBooks()
        // {
        //     var myBooks = _context.Books.Where(book => book.User.Username == "hsienk");
        //
        //     return View(myBooks);
        // }
        //
        // public IActionResult ByGenre()
        // {
        //     var booksGroupedByGenre =
        //         from book in _context.Books
        //         group book by book.Genre;
        //
        //     foreach (var booksGroup in booksGroupedByGenre)
        //     {
        //         
        //     }
        //
        //     return View(availableBooks);
        // }
        //
        // public IActionResult SuggestedBooks()
        // {
        //     Random rnd = new Random();
        //
        //     var suggestedBooks =
        //         (from book in _context.Books
        //         where book.User == null
        //         orderby rnd.Next()
        //         select book).Take(3);
        //
        //
        //     return View(suggestedBooks);
        // }
    }
}
