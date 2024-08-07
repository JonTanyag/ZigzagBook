namespace Zigzag.Core;

public interface IAddBookService
{
    Task AddBook(Book book, CancellationToken cancellationToken);
}
