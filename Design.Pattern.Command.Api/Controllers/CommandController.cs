using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.AspNetCore.Mvc;

namespace Design.Pattern.Command.Api.Controllers;

[Route("[controller]/[action]")]
public class CommandController : ControllerBase
{
    private readonly UserHandler _receiver;
    public CommandController(UserHandler receiver)
    {
        _receiver = receiver;
    }
    
    [HttpPost]
    public IActionResult UpdateUserName(string name)
    {
       var resultState = _receiver.Handle(new UpdateNameCommand(){Name = name});
       return StatusCode(resultState.StatusCode, resultState.Message);
    }

}