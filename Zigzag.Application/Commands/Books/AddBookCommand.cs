using MediatR;

namespace Zigzag.Application;

public class AddBookCommand : IRequest<AddBookResponse>
{
    public BookDto Book { get; set; }   
}
