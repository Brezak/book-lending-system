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
    public class LendingController : ControllerBase
    {
        private readonly BookLendingDb _context;

        public LendingController(BookLendingDb context)
        {
            _context = context;
        }

        [HttpGet("{book_id}")]
        public async Task<ActionResult<bool>> IsBookLentOut(int book_id)
        {
            if (await _context.Books.FindAsync(book_id) == null)
            {
                return NotFound();
            }

            return await _context.IsBookLentOut(book_id);
        }

        [HttpPost]
        public async Task<ActionResult<Lending>> LendBook(int book_id, DateTime from)
        {
            if (await _context.IsBookLentOut(book_id))
            {
                return Conflict("Book is already being lent out");
            }

            var lending = new Lending { BookId = book_id, From = from };

            _context.Lendings.Add(lending);
            await _context.SaveChangesAsync();

            return CreatedAtAction("LendBook", new { id = lending.Id }, lending);
        }

        [HttpDelete("{book_id}")]
        public async Task<IActionResult> ReturnBook(int book_id)
        {
            var lending = await _context.Lendings.Where(lending => lending.BookId == book_id && lending.To == null).SingleOrDefaultAsync();

            if (lending == null)
            {
                return NotFound(); 
            }

            lending.To = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
