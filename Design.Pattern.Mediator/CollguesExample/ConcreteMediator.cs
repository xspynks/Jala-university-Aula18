namespace Design.Pattern.Mediator;

public class ConcreteMediator : Mediator
{
    private List<Collegue> _collegues = new();
    public override void Send(string message, Collegue collegue)
    {
        _collegues.Where(x => x != collegue).ToList().ForEach(x => x.HandleNotification(message));
    }

    public void Register(Collegue collegue)
    {
        collegue.SetMediator(this);
        _collegues.Add(collegue);
    }
    
    
}