using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            this._db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Books.FindAsync(id);

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Books.FindAsync(Book.Id);
                if (BookFromDb != null)
                {
                    BookFromDb.Name = Book.Name;
                    BookFromDb.Author = Book.Author;
                    BookFromDb.ISBN = Book.ISBN;

                    await _db.SaveChangesAsync();
                    return RedirectToPage("Index");

                }
                return RedirectToPage(Book);
            }
            return RedirectToPage(Book);
        }
    }
}