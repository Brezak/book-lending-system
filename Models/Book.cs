using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

        #region Book description
        // A ISBN13 will contain 4 hyphens so it's length is 13 + 4
        [MaxLength(17)]
        [Unicode(false)]
        public string? Isbn { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }
        #endregion

        public ICollection<Lending> Lendings { get; } = [];

        private enum IsbnFormat
        {
            Isbn10,
            Isbn13,
        }

        public static bool IsValidIsbn(string isbn)
        {
            if (isbn.Any(c => c != '-' && !char.IsAsciiDigit(c)))
            {
                return false;
            }

            IsbnFormat isbnFormat;
            switch (isbn.Length)
            {
                case 13:
                    isbnFormat = IsbnFormat.Isbn10;
                    break;
                case 17:
                    isbnFormat = IsbnFormat.Isbn13;
                    break;
                default: return false;
            }

            var weight = isbnFormat switch
            {
                IsbnFormat.Isbn10 => 10,
                IsbnFormat.Isbn13 => 1,
            };

            var sum = 0;
            var hyphenCount = 0;

            foreach (var part in isbn.AsSpan(0, isbn.Length))
            {
                if (part == '-')
                {
                    hyphenCount++;
                    continue;
                }

                sum += weight * (part - '0');

                weight = (isbnFormat) switch
                {
                    IsbnFormat.Isbn10 => weight - 1,
                    IsbnFormat.Isbn13 => weight == 1 ? 3 : 1,
                };
            }

            var totalHyphenCount = isbnFormat switch
            {
                IsbnFormat.Isbn10 => 3,
                IsbnFormat.Isbn13 => 4,
            };

            if (hyphenCount != totalHyphenCount)
            {
                return false;
            }

            return isbnFormat switch
            {
                IsbnFormat.Isbn10 => (weight % 11) == 0,
                IsbnFormat.Isbn13 => (weight % 10) == 0,
            };
        }
    }

    public class PublicBookData(Book book)
    {
        public string? Isbn { get; set; } = book.Isbn;
        public string Author { get; set; } = book.Author;
        public string Title { get; set; } = book.Title;
        public int PageCount { get; set; } = book.PageCount;
        public string Genre { get; set; } = book.Genre;
        public string Publisher { get; set; } = book.Publisher;
        public string Edition { get; set; } = book.Edition;
    }
}
