using Design.Pattern.Command.Api.Commands;

namespace Design.Pattern.Command.Api.Receivers;
public interface IReceiver<in TCommand, out TResponse> where TCommand: ICommand<TResponse>
{ 
     TResponse Handle(TCommand command);
}