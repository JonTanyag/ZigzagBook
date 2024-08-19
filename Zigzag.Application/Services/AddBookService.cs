using Zigzag.Core;

namespace Zigzag.Application;

public class AddBookService : IAddBookService
{
    private readonly IBookRepository _repository;

    public AddBookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book> AddBook(Book book, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(book,cancellationToken);
        return book;
    }
}
