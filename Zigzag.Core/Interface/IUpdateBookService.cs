namespace Zigzag.Core;

public interface IUpdateBookService
{
    Task<Book> UpdateBook(Book book, CancellationToken cancellationToken);
}
