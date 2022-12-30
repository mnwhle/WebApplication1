namespace WebApplication1.Interfaces;

public interface IPrimaryKey<T> where T : struct
{
    T Id { get; set; }
}
