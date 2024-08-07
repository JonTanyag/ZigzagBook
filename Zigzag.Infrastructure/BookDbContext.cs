using Microsoft.EntityFrameworkCore;
using Zigzag.Core;

namespace Zigzag.Infrastructure;

public class BookDbContext : DbContext
{
    public BookDbContext()
    { }
    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Book> Books { get; set; }
}
