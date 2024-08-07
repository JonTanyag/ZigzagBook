namespace Zigzag.Core;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedWhen { get; set; }
    public DateTime UpdatedWhen { get; set; }
}
