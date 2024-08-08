using Microsoft.EntityFrameworkCore;
using Zigzag.Core;

namespace Zigzag.Infrastructure;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        var existingBook = await _context.Books.FindAsync(book.Id);
        
        if (existingBook is null) 
            throw new NullReferenceException("Book not found");
        
        _context.Entry(existingBook).State = EntityState.Detached;
        _context.Books.Update(book);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Books;
    }

    public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(id, cancellationToken);
        if (book is null)
            throw new NullReferenceException("Book not found");

        return book;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
            throw new NullReferenceException($"{nameof(book)} cannot be deleted.");

        _context.Books.Remove(book);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
