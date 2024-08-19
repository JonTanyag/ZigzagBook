namespace Zigzag.Core;

public interface IDeleteBookService
{
    Task DeleteBook(int id, CancellationToken cancellationToken);
}
