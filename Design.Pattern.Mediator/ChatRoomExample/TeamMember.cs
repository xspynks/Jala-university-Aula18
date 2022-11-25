namespace Design.Pattern.Mediator.ChatRoomExample;

public abstract class TeamMember
{
    public TeamMember(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    
    public virtual void ReceiveMessage(string from, string message)
    {
        Console.WriteLine($"Receive message from {from}, message: {message}");
    }
}