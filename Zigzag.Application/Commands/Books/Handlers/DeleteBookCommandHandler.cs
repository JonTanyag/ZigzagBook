using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeleteBookResponse>
{
    private readonly ILogger<DeleteBookCommandHandler> _logger;
    private readonly IDeleteBookService _bookService;
    public DeleteBookCommandHandler(IDeleteBookService bookService,
        ILogger<DeleteBookCommandHandler> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }
    public async Task<DeleteBookResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _bookService.DeleteBook(request.Id, cancellationToken);

            _logger.LogInformation("Book deleted successfully.");
            return new DeleteBookResponse(true, "Book deleted successfully.", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting book." + $"{ex.Message} - {ex.InnerException}");
            return new DeleteBookResponse(false, ex.Message, 500);
        }
    }
}
