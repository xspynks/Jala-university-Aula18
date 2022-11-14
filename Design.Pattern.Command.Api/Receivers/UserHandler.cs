using Design.Pattern.Command.Api.Commands;
using Microsoft.Extensions.Caching.Memory;

namespace Design.Pattern.Command.Api.Receivers;

public class UserHandler :
    IReceiver<UpdateNameCommand, ResponseState>,
    IReceiver<UndoUserAction, bool>
    //We can implement as many IReceiver<> interfaces as we want as long as they defer in their pairs of parameters
{
    private readonly IMemoryCache _memoryCache;

    public UserHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public ResponseState Handle(UpdateNameCommand updateNameCommand)
    {
        updateNameCommand.Execute();
        _memoryCache.Set(updateNameCommand.Id, updateNameCommand, TimeSpan.FromMinutes(2));
        return new ResponseState(200,"Nome alterado com sucesso");
    }
    
    public bool Handle(UndoUserAction command)
    { 
        _memoryCache.Remove(command.Id);
        Console.WriteLine("Change undo");
        return true;
    }
}



