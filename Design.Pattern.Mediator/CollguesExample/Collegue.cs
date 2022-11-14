namespace Design.Pattern.Mediator;

public abstract class Collegue
{
    private Mediator _mediator;
    
    public void SetMediator(Mediator mediator)
    {
        _mediator = mediator;
    }

    public virtual void Send(string message)
    {
        _mediator.Send(message, this);
    }

    public abstract void HandleNotification(string message);
}