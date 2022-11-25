using Design.Pattern.Command.Api;
using Design.Pattern.Mediator;
using Design.Pattern.Mediator.ChatRoomExample;
using Design.Pattern.Mediator.ChatRoomExample.Commands;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediator(typeof(Program));
services.AddMemoryCache();

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

Console.WriteLine(mediator.Send(new CollegueA()));
Console.WriteLine(mediator.Send(new CollegueB()));
Console.WriteLine(mediator.Send(new CollegueC()));

var from = new Developer("Dev");
var to = new Tester("Tester");
var message = "Olá, mundo!";
mediator.Send(new RegisterTeamMemberCommand() { TeamMember = from });
mediator.Send(new RegisterTeamMemberCommand() { TeamMember = to });
mediator.Send(new RegisterTeamMemberCommand.MessageCommand() { From = from, To = to, Message = message });