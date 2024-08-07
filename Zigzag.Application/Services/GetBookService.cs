using Zigzag.Core;

namespace Zigzag.Application;

public class GetBookService : IGetBookService
{
    private readonly IBookRepository _repository;

    public GetBookService(IBookRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Book>> GetBookAsync(CancellationToken cancellationToken)
    {
       var books = await _repository.GetAllAsync(cancellationToken);
       return books;
    }

    public async Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(id, cancellationToken);
        return book;
    }
}
