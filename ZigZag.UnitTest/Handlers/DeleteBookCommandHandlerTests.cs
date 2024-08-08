using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class DeleteBookCommandHandlerTests
{
    private Mock<ILogger<DeleteBookCommandHandler>> _mockLogger;
    private Mock<IDeleteBookService> _mockService;
    private DeleteBookCommandHandler _mockHandler;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<DeleteBookCommandHandler>>();
        _mockService = new Mock<IDeleteBookService>();
        _mockHandler = new DeleteBookCommandHandler(_mockService.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Handle_Delete_Book_Should_Return_Success_Response()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var command = new DeleteBookCommand(bookId) { Id = bookId};
        _mockService.Setup(s => s.DeleteBook(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsDeleted.ShouldBe(true);
        result.Message.ShouldBe("Book deleted successfully.");
        result.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        _mockService.Verify(s => s.DeleteBook(bookId, It.IsAny<CancellationToken>()), Times.Once);
    }

        [Test]
    public async Task Handle_DeleteBookThrowsException_ReturnsErrorResponse()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var command = new DeleteBookCommand(bookId) { Id = bookId };
        var exceptionMessage = "Test Exception";
        _mockService.Setup(s => s.DeleteBook(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Throws(new Exception(exceptionMessage));

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
        result.IsDeleted.ShouldBe(false);
        result.Message.ShouldBe(exceptionMessage);
        result.StatusCode.ShouldBe(500);
    }
}
