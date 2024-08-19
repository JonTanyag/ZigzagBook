namespace Zigzag.Core;

public interface IAddBookService
{
    Task<Book> AddBook(Book book, CancellationToken cancellationToken);
}
