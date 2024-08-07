namespace Zigzag.Core;

public interface IUpdateBookService
{
    Task UpdateBook(Book book, CancellationToken cancellationToken);
}
