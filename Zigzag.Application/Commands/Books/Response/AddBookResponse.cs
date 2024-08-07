namespace Zigzag.Application;

public class AddBookResponse : BaseResponse
{
    public AddBookResponse(bool isCreated, string message, int statusCode)
    {
        IsCreated = isCreated;
        Message = message;
        StatusCode = statusCode;
    }
    public bool IsCreated { get; set; }
}
