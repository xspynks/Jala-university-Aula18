namespace Design.Pattern.Mediator.ChatRoomExample;

public class TeamChatRoom : ChatRoom
{
    private List<TeamMember> _members = new();
    public override void Register(TeamMember member)
    {
        member.SetChatRoom(this);
        _members.Add(member);
    }

    public override void Register(TeamMember[] teamMembers)
    {
        foreach (var member in teamMembers)
        {
            member.SetChatRoom(this);
        }
        _members.AddRange(teamMembers.ToList());
    }

    public override void Send(string from, string message)
    {
        foreach (var member in _members)
        {
            if (member.Name != from)
            {
                member.ReceiveMessage(from, message);
            }
        }
    }

    public override void Send<TMemberType>(string from, string message)
    {
        _members.OfType<TMemberType>()
            .Where(x => x.Name != from).ToList()
            .ForEach(x =>  x.ReceiveMessage(from, message));
    }
}