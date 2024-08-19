using MediatR;

namespace Zigzag.Application;

public class DeleteBookCommand : IRequest<DeleteBookResponse>
{
    public DeleteBookCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
