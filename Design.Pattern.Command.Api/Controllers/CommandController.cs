using Design.Pattern.Command.Api.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Design.Pattern.Command.Api.Controllers;

[Route("[controller]/[action]")]
public class CommandController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("UpdateUserName")]
    public IActionResult UpdateUserName(string name)
    {
        try
        {
            var resultState = _mediator.Send(new UpdateNameCommand(){Name = name, Id = 1});
            return StatusCode(resultState.StatusCode, resultState.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok();
        }
     
    }
    
    [HttpPost("UpdateRole")]
    public IActionResult UpdateRole(string role)
    {
        try
        {
            var resultState = _mediator.Send(new UpdateRoleCommand(){Role = role, Id = 2});
            return StatusCode(resultState.StatusCode, resultState.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok();
        }
     
    }

    [HttpPost("UndoChange")]
    public IActionResult UndoChange(UndoUserAction userAction)
    {
        _mediator.Send(userAction);
        return Ok();
    }

}