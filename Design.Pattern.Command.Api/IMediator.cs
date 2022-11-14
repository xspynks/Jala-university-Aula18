using Design.Pattern.Command.Api.Commands;

namespace Design.Pattern.Command.Api;

public interface IMediator
{
    public TResponse Send<TResponse>(ICommand<TResponse> command);
}