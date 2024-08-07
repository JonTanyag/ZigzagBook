using MediatR;

namespace Zigzag.Application;

public class DeleteBookCommand : IRequest<DeleteBookResponse>
{
    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
