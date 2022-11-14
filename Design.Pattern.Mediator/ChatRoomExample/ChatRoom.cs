namespace Design.Pattern.Mediator.ChatRoomExample;

public abstract class ChatRoom
{
    public abstract void Register(TeamMember teamMember);
    public abstract void Register(TeamMember[] teamMembers);
    public abstract void Send(string from, string message);

    public abstract void Send<TMemberType>(string from, string message) where TMemberType : TeamMember;

}