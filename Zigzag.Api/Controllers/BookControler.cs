using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zigzag.Api.Filters;
using Zigzag.Application;

namespace Zigzag.Api;

[Route("/book")]
public class BookControler : Controller
{
    private readonly IMediator _mediatr;
    public BookControler(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("/books")]
    [ApiKey]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    [SwaggerOperation(
            Summary = "Get all books",
            Description = "Retrieve a list of all books in the library.",
            OperationId = "GetBooks",
            Tags = new[] { "LibraryAPI" }
        )]
    public async Task<IActionResult> Get()
    {
        var response = await _mediatr.Send(new GetBooksQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ApiKey]
    [SwaggerOperation(
            Summary = "Get a specific book",
            Description = "Retrieve a specific book by its ID.",
            OperationId = "GetBook",
            Tags = new[] { "LibraryAPI" }
        )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediatr.Send(new GetBookByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    [ApiKey]
    [Produces("application/json")]
    [SwaggerOperation(
            Summary = "Add a new book",
            Description = "Add a new book to the library.",
            OperationId = "Post",
            Tags = new[] { "LibraryAPI" }
        )]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] AddBookCommand command)
    {
        var response = await _mediatr.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ApiKey]
    [Produces("application/json")]
    [SwaggerOperation(
            Summary = "Update a book",
            Description = "Update an existing book by its ID.",
            OperationId = "Put",
            Tags = new[] { "LibraryAPI" }
        )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Book.Id)
            return NotFound("Id mismatch");

        var response = await _mediatr.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ApiKey]
    [Produces("application/json")]
    [SwaggerOperation(
            Summary = "Delete a book",
            Description = "Delete an existing book by its ID.",
            OperationId = "Delete",
            Tags = new[] { "LibraryAPI" }
        )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediatr.Send(new DeleteBookCommand(id));
        return Ok(response);
    }
}
