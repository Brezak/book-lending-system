using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Books",
                ["Id", "DateAdded", "Isbn", "Author", "Title", "PageCount", "Genre", "Publisher", "Edition"],
                [1, new DateTime(2024, 6, 3), "978-0-7653-4878-4", "Steven Erikson", "Gardens of the Moon", "712", "High Fantasy", "Tor Fantasy", "First Edition"]
            );

            // Handle a book made before isbns
            migrationBuilder.InsertData("Books",
                ["Id", "DateAdded", "Isbn", "Author", "Title", "PageCount", "Genre", "Publisher", "Edition"],
                [2, new DateTime(2001, 12, 6), null, "Herman Melville", "Moby-Dick; or, The Whale", "653", "Adventure fiction", "Harper & Brothers", "Second Edition"]
            );

            // Handle multiple books of the same kind
            migrationBuilder.InsertData("Books",
                ["Id", "DateAdded", "Isbn", "Author", "Title", "PageCount", "Genre", "Publisher", "Edition"],
                [3, new DateTime(2002, 12, 6), "978-0-321-22800-0", "Herman Melville", "Moby-Dick, A Longman Critical Edition", "653", "Adventure fiction", "Longman Publishing Group", "1"]
            );

            migrationBuilder.InsertData("Books",
                ["Id", "DateAdded", "Isbn", "Author", "Title", "PageCount", "Genre", "Publisher", "Edition"],
                [4, new DateTime(2002, 12, 6), "978-0-321-22800-0", "Herman Melville", "Moby-Dick, A Longman Critical Edition", "653", "Adventure fiction", "Longman Publishing Group", "1"]
            );

            migrationBuilder.InsertData("Lendings",
                ["Id", "BookId", "From", "To"],
                [0, 1, new DateTime(2020, 1, 1), null]
            );

            migrationBuilder.InsertData("Lendings",
                ["Id", "BookId", "From", "To"],
                [1, 3, new DateTime(2023, 2, 1), new DateTime(2023, 2, 2)]
            );

            migrationBuilder.InsertData("Lendings",
                ["Id", "BookId", "From", "To"],
                [2, 3, new DateTime(2023, 2, 3), new DateTime(2023, 2, 3)]
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Books", "Id", 1);
            migrationBuilder.DeleteData("Books", "Id", 2);
            migrationBuilder.DeleteData("Books", "Id", 3);
            migrationBuilder.DeleteData("Books", "Id", 4);

            migrationBuilder.DeleteData("Lendings", "Id", 0);
            migrationBuilder.DeleteData("Lendings", "Id", 1);
            migrationBuilder.DeleteData("Lendings", "Id", 2);
        }
    }
}
