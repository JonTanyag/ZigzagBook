using Zigzag.Core;

namespace Zigzag.Application;

public class UpdateBookService : IUpdateBookService
{
    private readonly IBookRepository _repository;

    public UpdateBookService(IBookRepository repository)
    {
        _repository = repository;
    }
    public async Task UpdateBook(Book book, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(book, cancellationToken);
    }
}
