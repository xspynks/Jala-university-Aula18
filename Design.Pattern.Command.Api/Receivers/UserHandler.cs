using Design.Pattern.Command.Api.Commands;

namespace Design.Pattern.Command.Api.Receivers;

public class UserHandler :
    IReceiver<UpdateNameCommand, ResponseState>
{
    private Dictionary<int, UpdateNameCommand> _Stack = new();
    public ResponseState Handle(UpdateNameCommand updateNameCommand)
    {
        updateNameCommand.Execute();
        _Stack.Add(updateNameCommand.Id, updateNameCommand);
        return new ResponseState(200,"Nome alterado com sucesso");
    }

}



