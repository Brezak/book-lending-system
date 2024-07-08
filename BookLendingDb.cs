using System.Diagnostics;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibrarySystem
{
    public class BookLendingDb(DbContextOptions<BookLendingDb> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Lending> Lendings => Set<Lending>();

        public async Task<bool> IsBookLentOut(int bookId)
        {
            return await Lendings.AnyAsync(lending => lending.BookId == bookId && lending.To == null);
        }
    }
}
