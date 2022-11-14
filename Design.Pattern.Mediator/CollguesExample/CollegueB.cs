namespace Design.Pattern.Mediator;

public class CollegueB : Collegue
{
    public override void HandleNotification(string message)
    {
        Console.WriteLine($"Object B Received {message}");
    }
}