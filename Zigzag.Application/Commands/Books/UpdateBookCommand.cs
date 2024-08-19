using MediatR;

namespace Zigzag.Application;

public class UpdateBookCommand : IRequest<BookDto>
{
    public BookDto Book { get; set; }
}
