using Zigzag.Core;

namespace Zigzag.Application;

public static class BookMapper
{
    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author= book.Author,
            ISBN= book.ISBN,
            PublishedDate= book.PublishedDate,
        };
    }

    public static Book FromDto(this BookDto bookDto)
    {
        return new Book
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            Author = bookDto.Author,
            ISBN = bookDto.ISBN,
            PublishedDate = bookDto.PublishedDate,
        };
    }
}
