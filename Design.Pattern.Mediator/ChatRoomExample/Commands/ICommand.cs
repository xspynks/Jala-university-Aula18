namespace Design.Pattern.Mediator.ChatRoomExample.Commands;

public interface ICommand<TResponse>
{
    void Execute();
}