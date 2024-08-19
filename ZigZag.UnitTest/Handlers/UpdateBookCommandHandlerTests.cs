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
    public async Task Handle_UpdateBook_Successfully_Returns_Success_Response()
    {
        // Arrange
        var bookDto = new BookDto { /* Initialize with test data */ };
        var book = bookDto.FromDto();
        var updatedBook = new Book { };
        var expectedResponseDto = updatedBook.ToDto();

        _mockService.Setup(x => x.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updatedBook);

        // Act
        var result = await _mockHandler.Handle(new UpdateBookCommand { Book = bookDto }, CancellationToken.None);

        // Assert
        result.ShouldBe(expectedResponseDto);
        _mockService.Verify(x => x.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockLogger.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>
                v.ToString().Contains($"Book Updated")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Test]
    public async Task Handle_Throws_Exception_Returns_Error_Response()
    {
        // Arrange
        var bookDto = new BookDto { /* Initialize with test data */ };
        var exceptionMessage = "An error occurred";
        var exception = new Exception(exceptionMessage);

        _mockService.Setup(x => x.UpdateBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception(exceptionMessage)); // Ensure this throws a Task

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(async () => 
            await _mockHandler.Handle(new UpdateBookCommand { Book = bookDto }, CancellationToken.None));

        // Assert
        ex.Message.ShouldBe("An error occurred while updating book.");
        _mockLogger.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>
                v.ToString().Contains($"An error occurred while updating book{exceptionMessage} - {exception.InnerException}")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

    }
}
