namespace Design.Pattern.Command.Api;

public struct ResponseState : IResponseState
{
    public ResponseState(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }


    public int StatusCode { get; set; }
    public string Message { get; set; }
}

