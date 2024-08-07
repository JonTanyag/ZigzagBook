using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly ILogger<GetBooksQueryHandler> _logger;
    private readonly IGetBookService _bookService;

    public GetBooksQueryHandler(IGetBookService bookService, ILogger<GetBooksQueryHandler> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }
    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookService.GetBookAsync(cancellationToken);
        var booksDto = new List<BookDto>();

        foreach (var book in books)
        {
            booksDto.Add(book.ToDto());
        }

        return booksDto;
    }
}
