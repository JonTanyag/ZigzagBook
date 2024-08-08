using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class AddBookServiceTests
{
    private Mock<IBookRepository> _mockRepository;
    private AddBookService _mockService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IBookRepository>();
        _mockService = new AddBookService(_mockRepository.Object);
    }

    [Test]
    public async Task AddBook_Service_Should_Call_Repository()
    {
        // Arrange
        var book = new Book {};
        var cancellationToken = CancellationToken.None;

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _mockService.AddBook(book, cancellationToken);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(book, cancellationToken), Times.Once);
    }

     [Test]
    public async Task AddBook_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        var book = new Book { /* Initialize properties as needed */ };
        var cancellationToken = CancellationToken.None;
        var exceptionMessage = "Test Exception";

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Throws(new Exception(exceptionMessage));

        // Act & Assert
        await Should.ThrowAsync<Exception>(async () => await _mockService.AddBook(It.IsAny<Book>(), cancellationToken));
    }
}
