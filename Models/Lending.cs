using System;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Models
{
    [Index(nameof(BookId), nameof(From), nameof(To), IsUnique = true)]
    public class Lending
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
