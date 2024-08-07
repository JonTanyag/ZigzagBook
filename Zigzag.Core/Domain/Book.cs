namespace Zigzag.Core;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishedDate { get; set; }
}
