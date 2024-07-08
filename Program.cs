using LibrarySystem;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<BookLendingDb>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteDb")));
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "LendingApi";
            config.Title = "TodoAPI 1.0";
            config.Version = "1.0";
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();
            app.UseSwaggerUi(config =>
            {
                config.DocumentTitle = "LendingApi";
                config.Path = "/swagger";
                config.DocumentPath = "/swagger/{documentName}/swagger.json";
                config.DocExpansion = "list";
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<BookLendingDb>();
            context.Database.Migrate();
        }

        app.MapPost("/lend_book", async (int book_id, BookLendingDb db) =>
        {
            if (!await db.Books.AnyAsync(book => book.Id == book_id))
            {
                return Results.NotFound("Book doesn't exist");
            }

            if (await db.IsBookLentOut(book_id)) {
                return Results.NotFound("Book has already been lent out");
            }

            db.Lendings.Add(new Lending { BookId = book_id, From = DateTime.Now });
            await db.SaveChangesAsync();

            return Results.Ok();
        });
        app.MapPost("/return_book", async (int book_id, BookLendingDb db) =>
        {
            if (!await db.IsBookLentOut(book_id))
            {
                throw new ArgumentException("Book isn't being lent out");
            }

            var lending = await db.Lendings.SingleAsync(lending => lending.BookId == book_id && lending.To == null);

            lending.To = DateTime.Now;
            await db.SaveChangesAsync();
        });

        app.Run();
    }
}
