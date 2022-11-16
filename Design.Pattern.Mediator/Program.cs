using Design.Pattern.Command.Api;
using Design.Pattern.Mediator;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediator(typeof(Program));

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

Console.WriteLine(mediator.Send(new CollegueA()));
Console.WriteLine(mediator.Send(new CollegueB()));
Console.WriteLine(mediator.Send(new CollegueC()));
// var receivers = new Dictionary<Type, Type>()
// {
//     {typeof(CollegueA), typeof(CollegueHandler)},
//     {typeof(CollegueB), typeof(CollegueHandler)},
//     {typeof(CollegueC), typeof(CollegueHandler)}
//     
// };

// var collegueHandler = new CollegueHandler();
//
// var collegueList = new List<(Type, object)>()
// {
//     (typeof(CollegueHandler), collegueHandler)
// };
//
// object GetRequiredService(Type type)
// {
//     return collegueList.FirstOrDefault(x => x.Item1 == type).Item2;
// }
//
// var getRequiredServiceFunc = 
//     new Func<Type, object>(type => collegueList.FirstOrDefault(x => x.Item1 == type).Item2);
//
// var mediator = new Mediator(GetRequiredService ,receivers);

