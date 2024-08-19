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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id); // Set Id as primary key
        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Ensure Id is auto-generated
    }
}
