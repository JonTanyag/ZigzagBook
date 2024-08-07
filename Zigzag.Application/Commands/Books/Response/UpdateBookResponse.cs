namespace Zigzag.Application;

public class UpdateBookResponse : BaseResponse
{
    public UpdateBookResponse(bool isUpdated, string message, int statusCode)
    {
        IsUpdated = isUpdated;
        Message = message;
        StatusCode = statusCode;
    }
    public bool IsUpdated { get; set; }
}
