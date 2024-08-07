using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
{
    private readonly ILogger<AddBookCommandHandler> _logger;
    private readonly IAddBookService _bookService;
    public AddBookCommandHandler(ILogger<AddBookCommandHandler> logger, IAddBookService bookService)
    {
        _bookService = bookService;
        _logger = logger;
    }
    public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _bookService.AddBook(request.Book.FromDto(), cancellationToken);

            _logger.LogInformation("Book Added");
            return new AddBookResponse(true, "Book Added", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while adding book" + $"{ex.Message} - {ex.InnerException}");
            return new AddBookResponse(false, ex.Message, 500);
        }
    }
}
