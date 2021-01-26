using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPMVC.Data;
using ASPMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASPMVC.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,PublicationDate,Genre,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PublicationDate,Genre,ISBN")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MyBooks()
        {
            ViewData["DisplayMine"] = true;

            var myBooks = _context.Books.Where(book => book.User.Username == User.Identity.Name);

            return View(myBooks);
        }

        [HttpPost]
        public IActionResult ReturnBook(int id)
        {
            ViewData["DisplayMine"] = true;

            var bookToReturn = _context.Books.Include(b => b.User).FirstOrDefault(book => book.User.Username == User.Identity.Name && book.Id == id);

            if (bookToReturn != null)
            {
                bookToReturn.User = null;
                _context.SaveChanges();
            }

            var myBooks = _context.Books.Where(book => book.User.Username == User.Identity.Name);

            return PartialView("BooksPartial", myBooks);
        }     
        
        public IActionResult AvailableBooks()
        {
            ViewData["DisplayMine"] = false;

            var availableBooks =
                from book in _context.Books
                where book.User == null
                select book;

            return View("MyBooks", availableBooks);
        }

        [HttpPost]
        public IActionResult BorrowBook(int id)
        {
            ViewData["DisplayMine"] = false;

            var bookToBorrow = _context.Books.FirstOrDefault(book => book.Id == id);

            var currentUser = _context.Users.FirstOrDefault(user => user.Username == User.Identity.Name);

            if (bookToBorrow != null && currentUser != null)
            {
                bookToBorrow.User = currentUser;
                _context.SaveChanges();
            }

            var availableBooks =
                from book in _context.Books
                where book.User == null
                select book;

            return PartialView("BooksPartial", availableBooks);
        }
        
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

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
