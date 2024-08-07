using MediatR;

namespace Zigzag.Application;

public class UpdateBookCommand : IRequest<UpdateBookResponse>
{
    public BookDto Book { get; set; }
}
