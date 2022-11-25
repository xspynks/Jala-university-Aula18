using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.Extensions.Caching.Memory;

namespace Design.Pattern.Mediator.ChatRoomExample.Handlers;

public class SendMessageHandle : IReceiver<SendMessageHandle.MessageCommand, bool>
{
    private readonly IMemoryCache _memoryCache;
    public SendMessageHandle(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public bool Handle(MessageCommand command)
    {
        var cache = _memoryCache.Get<TeamMember>(command.To.Name);
        cache.ReceiveMessage(command.From.Name, command.Message);
        return true;
    }

    public class MessageCommand : ICommand<bool>
    {
        public TeamMember From { get; set; }
        public TeamMember To { get; set; }
        public string Message { get; set; }
    }
}