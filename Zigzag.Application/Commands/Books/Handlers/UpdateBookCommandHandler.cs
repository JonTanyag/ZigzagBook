using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Core;

namespace Zigzag.Application;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly ILogger<UpdateBookCommandHandler> _logger;
    private readonly IUpdateBookService _bookService;
    public UpdateBookCommandHandler(IUpdateBookService bookService,
            ILogger<UpdateBookCommandHandler> logger)
    {
        _logger = logger;
        _bookService = bookService;
    }
    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookService.UpdateBook(request.Book.FromDto(), cancellationToken);

            _logger.LogInformation("Book Updated");
            return result.ToDto();
        
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating book" + $"{ex.Message} - {ex.InnerException}");
            throw new Exception("An error occurred while updating book.", ex);
        }
    }
}
