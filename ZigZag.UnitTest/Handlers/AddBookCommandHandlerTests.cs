using System.Net;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Application.Common.Helper;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class AddBookCommandHandlerTests
{
    private Mock<ILogger<AddBookCommandHandler>> _mockLogger;
    private Mock<IAddBookService> _mockService;
    private AddBookCommandHandler _mockHandler;
    private Mock<IdGenerator> _mockIdGenerator;

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
        var bookDto = new BookDto { };
        var book = bookDto.FromDto();
        var responseBook = new Book { };
        var expectedResponse = responseBook.ToDto();
        var command = new AddBookCommand { Book = bookDto };

        _mockService.Setup(s => s.AddBook(It.IsAny<Book>(), It.IsAny<CancellationToken>())).ReturnsAsync(responseBook);

        // Act
        var result = await _mockHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(expectedResponse);


        _mockService.Verify(s => s.AddBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_Throws_Exception_Returns_Error_Response()
    {
        // Arrange
        var bookDto = new BookDto { /* Initialize with test data */ };
        var exceptionMessage = "An error occurred";
        var exception = new Exception(exceptionMessage);

        _mockService.Setup(x => x.AddBook(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception(exceptionMessage)); // Ensure this throws a Task

        // Act
        var ex = Assert.ThrowsAsync<Exception>(async () =>
            await _mockHandler.Handle(new AddBookCommand { Book = bookDto }, CancellationToken.None));


        // Assert
        ex.Message.ShouldBe("An error occurred while adding book");
        _mockLogger.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>
                v.ToString().Contains($"An error occurred while adding book{exceptionMessage} - {exception.InnerException}")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

}

