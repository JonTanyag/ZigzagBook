namespace Zigzag.Core;

public interface IGetBookService
{
    Task<IEnumerable<Book>> GetBookAsync(CancellationToken cancellationToken);
    Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
}
