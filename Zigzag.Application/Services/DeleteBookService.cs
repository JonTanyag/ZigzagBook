using Zigzag.Core;

namespace Zigzag.Application;

public class DeleteBookService : IDeleteBookService
{
    private readonly IBookRepository _repository;

    public DeleteBookService(IBookRepository repository)
    {
        _repository = repository;
    }
    public async Task DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
