using System.Net;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class AddBookCommandHandlerTests
{
    private Mock<ILogger<AddBookCommandHandler>> _mockLogger;
    private Mock<IAddBookService> _mockService;
    private AddBookCommandHandler _mockHandler;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<AddBookCommandHandler>>();
        _mockService = new Mock<IAddBookService>();
        _mockHandler = new AddBookCommandHandler(_mockLogger.Object, _mockService.Object);
    }

    [Test]
    public async Task Handle_AddBook_Successfully_Returns_Success_Response()
    {
        // Arrange
        var bookDto = new BookDto {};
        var command = new AddBookCommand {Book = bookDto};

        _mockService.Setup(s => s.AddBook(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsCreated.ShouldBe(true);
        result.Message.ShouldBe("Book Added");
        result.StatusCode.ShouldBe((int)HttpStatusCode.OK);

        _mockService.Verify(s => s.AddBook(It.Is<Book>(b => b.Id != Guid.Empty), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_Throws_Exception_Returns_Error_Response()
    {
        // Arrange
        var bookDto = new BookDto{};
        var command = new AddBookCommand {Book = bookDto};
        var exceptionMessage = "Test Exception";
        _mockService.Setup(s => s.AddBook(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Throws(new Exception(exceptionMessage));

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsCreated.ShouldBe(false);
        result.Message.ShouldBe(exceptionMessage);
        result.StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);

    }
}
