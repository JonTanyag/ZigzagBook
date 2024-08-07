## Zigzag


*TechStack*
- .NET 8
- EF Core 8 (InMemory database)
- Mediatr
- NUnit & Shouldly

**Setup**
``` bash
git clone <repository-url>
cd <repository-folder>
dotnet clean
dotnet build

dotnet run
```

API Endpoints
Books
``` bash
GET /api/books: Retrieves a list of books.
GET /api/books/{id}: Retrieves a specific book by ID.
POST /api/books: Add a new book.
PUT /api/books/{id}: Updates an existing book.
DELETE /api/books/{id}: Deletes a book.
```

Running Test
``` bash
cd <unit-test-project-directory>
dotnet test
```
