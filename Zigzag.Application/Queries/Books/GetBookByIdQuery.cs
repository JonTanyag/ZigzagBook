using MediatR;

namespace Zigzag.Application;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
