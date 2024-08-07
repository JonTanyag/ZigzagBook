using MediatR;

namespace Zigzag.Application;

public class GetBooksQuery : IRequest<List<BookDto>>
{

}
