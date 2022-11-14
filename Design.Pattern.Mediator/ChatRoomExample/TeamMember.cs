namespace Design.Pattern.Mediator.ChatRoomExample;

public abstract class TeamMember
{
    private ChatRoom _ChatRoom;

    public TeamMember(string name)
    {
        Name = name;
    }
    public string Name { get; set; }

    public void SetChatRoom(ChatRoom chatRoom)
    {
        _ChatRoom = chatRoom;
    }

    public void Send(string message)
    {
        _ChatRoom.Send(Name, message);
    }

    public void Send<TMemberType>(string message) where TMemberType: TeamMember
    {
        _ChatRoom.Send<TMemberType>(this.Name, message);
    }

    public virtual void ReceiveMessage(string from, string message)
    {
        Console.WriteLine($"Receive message from {from}, message: {message}");
    }
}