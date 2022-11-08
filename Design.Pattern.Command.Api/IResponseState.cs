namespace Design.Pattern.Command.Api;

public interface IResponseState
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
}