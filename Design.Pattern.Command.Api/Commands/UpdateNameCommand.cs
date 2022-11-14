namespace Design.Pattern.Command.Api.Commands;

public class UpdateNameCommand : ICommand<ResponseState>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Execute()
    {
        try
        {
            var user = new User();
            user.Id = Id;
            user.Name = Name;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
       
    }
}





