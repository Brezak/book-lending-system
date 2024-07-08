using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookLendingDb _context;

        public BooksController(BookLendingDb context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicBookData>>> GetBooks()
        {
            return await _context.Books.Select(book => new PublicBookData(book)).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(
            int id,
            PublicBookData data
        )
        {
            var book = await _context.Books.SingleAsync(book => book.Id == id);

            book.Isbn = data.Isbn;
            book.Author = data.Author;
            book.Title = data.Title;
            book.PageCount = data.PageCount;
            book.Genre = data.Genre;
            book.Publisher = data.Publisher;
            book.Edition = data.Edition;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<PublicBookData>> PostBook(
            string? isbn,
            string author,
            string title,
            int page_count,
            string genre,
            string publisher,
            string edition
        )
        {
            if (isbn != null && !Book.IsValidIsbn(isbn))
            {
                return BadRequest("Invalid isbn");
            }

            var book = new Book
            {
                Isbn = isbn,
                Author = author,
                Title = title,
                PageCount = page_count,
                Genre = genre,
                Publisher = publisher,
                Edition = edition,
                DateAdded = DateTime.UtcNow,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, new PublicBookData(book));
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
