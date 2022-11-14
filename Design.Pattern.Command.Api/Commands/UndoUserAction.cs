namespace Design.Pattern.Command.Api.Commands;

public class UndoUserAction : ICommand<bool>
{
    public int Id { get; set; }
}