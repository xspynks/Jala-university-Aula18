namespace Design.Pattern.Mediator.ChatRoomExample;

public class Developer : TeamMember
{
    public Developer(string name) : base(name)
    {
    }

    public override void ReceiveMessage(string from, string message)
    {
        Console.Write($"{Name} ");
        base.ReceiveMessage(from, message);
    }
}