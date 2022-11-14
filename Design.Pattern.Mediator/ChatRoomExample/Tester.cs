namespace Design.Pattern.Mediator.ChatRoomExample;

public class Tester : TeamMember
{
    public Tester(string name) : base(name)
    {
    }
    public override void ReceiveMessage(string from, string message)
    {
        Console.Write($"{Name} ");
        base.ReceiveMessage(from, message);
    }
}