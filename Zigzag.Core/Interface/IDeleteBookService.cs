namespace Zigzag.Core;

public interface IDeleteBookService
{
    Task DeleteBook(Guid id, CancellationToken cancellationToken);
}
