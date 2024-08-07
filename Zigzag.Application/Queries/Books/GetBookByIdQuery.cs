using MediatR;

namespace Zigzag.Application;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public GetBookByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
