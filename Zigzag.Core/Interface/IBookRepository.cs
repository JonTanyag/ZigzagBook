namespace Zigzag.Core;

public interface IBookRepository
{
    Task AddAsync(Book book, CancellationToken cancellationToken);
    Task UpdateAsync(Book book, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken);
    Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
