using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zigzag.Application;

namespace Zigzag.Api;

[Route("api/books")]
public class BookControler : Controller
{
    private readonly IMediator _mediatr;
    public BookControler(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediatr.Send(new GetBooksQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _mediatr.Send(new GetBookByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddBookCommand command)
    {
        var response = await _mediatr.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Book.Id)
                return BadRequest("Id mismatch");
            
        var response = await _mediatr.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediatr.Send(new DeleteBookCommand(id));
        return Ok(response);
    }
}
