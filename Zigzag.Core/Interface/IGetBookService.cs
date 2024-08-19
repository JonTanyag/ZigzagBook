namespace Zigzag.Core;

public interface IGetBookService
{
    Task<IEnumerable<Book>> GetBookAsync(CancellationToken cancellationToken);
    Task<Book> GetBookByIdAsync(int id, CancellationToken cancellationToken);
}
