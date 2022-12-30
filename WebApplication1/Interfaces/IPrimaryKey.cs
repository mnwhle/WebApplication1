namespace WebApplication1.Interfaces;

public interface IPrimaryKey<T>
{
    T Id { get; set; }
}
