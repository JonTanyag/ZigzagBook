namespace Zigzag.Application;

public class DeleteBookResponse : BaseResponse
{
    public DeleteBookResponse(bool isDeleted, string message, int statusCode)
    {
        IsDeleted = isDeleted;
        Message = message;
        StatusCode = statusCode;
    }
    public bool IsDeleted { get; set; }
}
