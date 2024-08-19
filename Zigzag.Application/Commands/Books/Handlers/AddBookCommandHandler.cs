using MediatR;
using Microsoft.Extensions.Logging;
using Zigzag.Application.Common.Helper;
using Zigzag.Core;

namespace Zigzag.Application;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly ILogger<AddBookCommandHandler> _logger;
    private readonly IAddBookService _bookService;
    //private readonly IdGenerator _idGenerator;
    public AddBookCommandHandler(ILogger<AddBookCommandHandler> logger, IAddBookService bookService)
    {
        _bookService = bookService;
        //_idGenerator = idGenerator;
        _logger = logger;
    }
    public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //request.Book.Id = _idGenerator.GenerateId();
            var response = await _bookService.AddBook(request.Book.FromDto(), cancellationToken);

            _logger.LogInformation("Book Added");
            return response.ToDto(); 
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while adding book" + $"{ex.Message} - {ex.InnerException}");
            throw new Exception("An error occurred while adding book", ex);
        }
    }
}
