A simple book lending system.

Before running the app you will need to create and fill the database with the required data.
First install the ef tools if you haven't.
```bash
dotnet install --global dotnet-ef
``` 
Then run the migrations.
```bash
dotnet ef database update
```

This will create the application database and fill it with development data (4 books in the id range 1-4).

The application is interacted with using several endpoints. Requests against `/api/Books` are used to manage and read books.
While requests against `/api/Lending` are used to manage book lendings.

- A `Get` request against `/api/Books` will return an array of all the books the system is holding.
- A `Post` request against `/api/Books` will add a new book to the system.
- A `Get` request against `/api/Books/{id}` will return data for the book with the specific `id`.
- A `Put` request against `/api/Books/{id}` can be used to modify the data stored for a given book.
- A `Delete` request against `/api/Books/{id}` will remove a book from the system.
- A `Get` request against `/api/Lending/{id}` will check if a book with the given `id` is being lent out.
- A `Post` request against `/api/Lending/{id}` will attempt to mark a book as being lent.
- A `Delete` request against `/api/Lending/{id}` will mark a book with the given `id` as returned.
