namespace Design.Pattern.Mediator;

public class CollegueC : Collegue
{
    public override void HandleNotification(string message)
    {
        Console.WriteLine($"Object C Received {message}");
    }
}