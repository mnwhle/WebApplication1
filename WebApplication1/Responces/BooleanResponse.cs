namespace WebApplication1.Responses;

public class BooleanResponse
{
    public bool Success { get; set; }

    public BooleanResponse(bool success)
    {
        Success = success;
    }
}
