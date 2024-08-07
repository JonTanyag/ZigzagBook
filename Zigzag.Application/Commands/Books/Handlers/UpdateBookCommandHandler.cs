using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
{
    private readonly ILogger<UpdateBookCommandHandler> _logger;
    private readonly IUpdateBookService _bookService;
    public UpdateBookCommandHandler(IUpdateBookService bookService,
            ILogger<UpdateBookCommandHandler> logger)
    {
        _logger = logger;
        _bookService = bookService;
    }
    public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _bookService.UpdateBook(request.Book.FromDto(), cancellationToken);

            _logger.LogInformation("Book Updated");
            return new UpdateBookResponse(true, "Book Updated", 200);
        
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating book" + $"{ex.Message} - {ex.InnerException}");
            return new UpdateBookResponse(false, ex.Message, 500);
        }
    }
}
