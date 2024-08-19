using MediatR;

namespace Zigzag.Application;

public class AddBookCommand : IRequest<BookDto>
{
    public BookDto Book { get; set; }   
}
