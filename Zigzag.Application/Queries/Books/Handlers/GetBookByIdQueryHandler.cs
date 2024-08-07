using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly ILogger<GetBookByIdQueryHandler> _logger;
    private readonly IGetBookService _bookService;

    public GetBookByIdQueryHandler(IGetBookService bookService, ILogger<GetBookByIdQueryHandler> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookService.GetBookByIdAsync(request.Id, cancellationToken);

        return books.ToDto();
    }
}
