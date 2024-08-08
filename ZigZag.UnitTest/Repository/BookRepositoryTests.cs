using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Shouldly;
using Zigzag.Core;
using Zigzag.Infrastructure;

namespace ZigZag.UnitTest;

public class BookRepositoryTests
{
        private Mock<BookDbContext> _mockContext;
    private Mock<DbSet<Book>> _mockDbSet;
    private BookRepository _repository;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<BookDbContext>();
        _mockDbSet = new Mock<DbSet<Book>>();
        _mockContext.Setup(c => c.Books).Returns(_mockDbSet.Object);

        _repository = new BookRepository(_mockContext.Object);
    }

    [Test]
    public async Task AddAsync_ShouldAddBook()
    {
        // Arrange
        var book = new Book { };

        // Act
        await _repository.AddAsync(book, CancellationToken.None);

        // Assert
        _mockDbSet.Verify(m => m.Add(book), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateBook()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = new Book { Id = bookId };
        _mockDbSet.Setup(m => m.FindAsync(bookId, It.IsAny<CancellationToken>())).ReturnsAsync(book);

        // Act
        await _repository.UpdateAsync(book, CancellationToken.None);

        // Assert
        _mockDbSet.Verify(m => m.Update(book), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_ShouldThrowException_WhenBookNotFound()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), /* Initialize other properties as needed */ };
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Book)null);

        // Act & Assert
        var exception = await Should.ThrowAsync<NullReferenceException>(async () => await _repository.UpdateAsync(book, CancellationToken.None)); 
        
        // Asert
        exception.Message.ShouldBe("Book not found");
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnBooks()
    {
        // Arrange
        var books = new List<Book> { new Book { } }.AsQueryable();
        _mockDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
        _mockDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
        _mockDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
        _mockDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

        // Act
        var result = await _repository.GetAllAsync(CancellationToken.None);

        // Assert
        result.ShouldNotBe(null);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid() };
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);

        // Act
        var result = _repository.GetByIdAsync(book.Id, CancellationToken.None).Result;

        // Assert
        result.ShouldBe(book);
    }

    [Test]
    public async Task GetByIdAsync_ShouldThrowException_WhenBookNotFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<Func<Book, bool>>())).ReturnsAsync((Book)null);

        // Act & Assert
        Should.Throw<NullReferenceException>(async () => await _repository.GetByIdAsync(bookId, CancellationToken.None));
            // .Message.ShouldBe("Book not found");
    }

    [Test]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        // Arrange
        var book = new Book { Id = Guid.NewGuid(), /* Initialize other properties as needed */ };
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(book);

        // Act
        await _repository.DeleteAsync(book.Id, CancellationToken.None);

        // Assert
        _mockDbSet.Verify(m => m.Remove(book), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldThrowException_WhenBookNotFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _mockDbSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Book)null);

        // Act
        var exception = await Should.ThrowAsync<NullReferenceException>(async () => await _repository.DeleteAsync(bookId, CancellationToken.None)); 
        
        // Asert
        exception.Message.ShouldBe("book cannot be deleted.");
    }
}
