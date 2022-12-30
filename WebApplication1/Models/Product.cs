namespace WebApplication1.Models;

public class Product : IPrimaryKeyGuid
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
}
