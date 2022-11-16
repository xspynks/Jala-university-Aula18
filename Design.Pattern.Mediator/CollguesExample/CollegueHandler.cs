using Design.Pattern.Command.Api.Receivers;

namespace Design.Pattern.Mediator;

public class CollegueHandler : 
    IReceiver<CollegueA, string>,
    IReceiver<CollegueB, string>,
    IReceiver<CollegueC, string>
{
    public string Handle(CollegueA command)
    {
        return "CollegueA sent a message";
    }

    public string Handle(CollegueB command)
    {
        return "CollegueB sent a message";
    }

    public string Handle(CollegueC command)
    {
        return "CollegueC sent a message";
    }
}