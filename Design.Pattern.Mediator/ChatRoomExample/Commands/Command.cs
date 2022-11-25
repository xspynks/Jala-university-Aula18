using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.Extensions.Caching.Memory;

namespace Design.Pattern.Mediator.ChatRoomExample.Commands;

public class RegisterTeamMemberCommand : ICommand<bool>
{
   public TeamMember TeamMember { get; set; }
   
   public class RegisterHandle : IReceiver<RegisterTeamMemberCommand, bool>
   {
      private readonly IMemoryCache _memoryCache;
      public RegisterHandle(IMemoryCache cache)
      {
         _memoryCache = cache;
      }

      public bool Handle(RegisterTeamMemberCommand command)
      {
         _memoryCache.GetOrCreate(command.TeamMember.Name, entry =>
         {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                
            return command.TeamMember;
         });

         return true;
      }
   }

   public class SendMessageHandle : IReceiver<MessageCommand, bool>
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
   }
   public class MessageCommand : ICommand<bool>
   {
      public TeamMember From { get; set; }
      public TeamMember To { get; set; }
      public string Message { get; set; }
   }
}
