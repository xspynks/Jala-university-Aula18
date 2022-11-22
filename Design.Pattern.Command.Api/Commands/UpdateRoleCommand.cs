namespace Design.Pattern.Command.Api.Commands;

public class UpdateRoleCommand : ICommand<ResponseState>
{
    public int Id { get; set; }
    public string Role { get; set; }
    public void Execute()
    {
        try
        {
            var user = new User();
            user.Id = Id;
            user.Role = Role;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
       
    }
}