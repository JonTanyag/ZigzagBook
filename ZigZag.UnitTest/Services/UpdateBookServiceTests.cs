using Moq;
using Shouldly;
using Zigzag.Application;
using Zigzag.Core;

namespace ZigZag.UnitTest;

public class UpdateBookServiceTests
{
    private Mock<IBookRepository> _mockRepository;
    private UpdateBookService _mockService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IBookRepository>();
        _mockService = new UpdateBookService(_mockRepository.Object);
    }

    [Test]
    public async Task UpdateBook_Service_Should_Call_Repository()
    {
        // Arrange
        var book = new Book {Id = Guid.NewGuid() };
        var cancellationToken = CancellationToken.None;

        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _mockService.UpdateBook(book, cancellationToken);

        // Assert
        _mockRepository.Verify(r => r.UpdateAsync(book, cancellationToken), Times.Once);
    }

     [Test]
    public async Task UpdateBook_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid() };
        var cancellationToken = CancellationToken.None;
        var exceptionMessage = "Test Exception";

        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>())).Throws(new NullReferenceException(exceptionMessage));

        // Act & Assert
        await Should.ThrowAsync<NullReferenceException>(async () => await _mockService.UpdateBook(It.IsAny<Book>(), cancellationToken));
    }
}
