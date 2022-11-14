namespace Design.Pattern.Mediator;

public class CollegueA : Collegue
{
    
    public override void HandleNotification(string message)
    {
        Console.WriteLine($"Object A Received {message}");
    }
}