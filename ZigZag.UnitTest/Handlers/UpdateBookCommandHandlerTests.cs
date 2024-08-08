using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class UpdateBookCommandHandlerTests
{
    private Mock<ILogger<UpdateBookCommandHandler>> _mockLogger;
    private Mock<IUpdateBookService> _mockService;
    private UpdateBookCommandHandler _mockHandler;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<UpdateBookCommandHandler>>();
        _mockService = new Mock<IUpdateBookService>();
        _mockHandler = new UpdateBookCommandHandler(_mockService.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Handle_AddBook_Successfully_Returns_Success_Response()
    {
        // Arrange
        var bookDto = new BookDto {};
        var command = new UpdateBookCommand {Book = bookDto};

        _mockService.Setup(s => s.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsUpdated.ShouldBe(true);
        result.Message.ShouldBe("Book Updated");
        result.StatusCode.ShouldBe((int)HttpStatusCode.OK);

        _mockService.Verify(s => s.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_Throws_Exception_Returns_Error_Response()
    {
        // Arrange
        var bookDto = new BookDto{};
        var command = new UpdateBookCommand {Book = bookDto};
        var exceptionMessage = "Test Exception";
        _mockService.Setup(s => s.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Throws(new Exception(exceptionMessage));

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsUpdated.ShouldBe(false);
        result.Message.ShouldBe(exceptionMessage);
        result.StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);

    }
}
