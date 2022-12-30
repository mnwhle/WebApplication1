namespace WebApplication1.Models;

public class Product : IPrimaryKey<int>
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
}
