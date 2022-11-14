using Design.Pattern.Command.Api.Commands;

namespace Design.Pattern.Command.Api;

public class Mediator : IMediator
{
    public Mediator(Func<Type, object> serviceProvider, IDictionary<Type, Type> receivers)
    {
        //We're holding the GetRequiredService as a function here
        _serviceProvider = serviceProvider;
        //Our dictionary with key=command value=receiver
        _receivers = receivers;
    }
    
    //Using a function instead of inject IServiceProvider decouple the mediator of the built in DI container from dotnet
    private readonly Func<Type, object> _serviceProvider;
    private readonly IDictionary<Type, Type> _receivers;
    
    public TResponse Send<TResponse>(ICommand<TResponse> command)
    {
        //Get the type of the command
        var commandType = command.GetType();
        if (!_receivers.ContainsKey(commandType))
            throw new Exception("Receiver not found in the container");

        //Get the receiver type from the dictionary 
        var receiverType = _receivers[commandType];
        //Now remember this function holds a reference to the GetRequiredService method
        //GetRequiredService receive a Type as parameter and return and object resolved from the DI container
        var receiver = _serviceProvider(receiverType);
        
        //Get the single method the has a parameter of the command type
        var handle = receiver.GetType().GetMethods()
            .Single(x => x.GetParameters().Any(x => x.ParameterType == commandType));
        //Finally we're invoking the method itself with the command parameter
        //This method could have more than one parameter that's why we are passing an array of objects
        return (TResponse)handle.Invoke(receiver, new []{command});
        //OFF TOPIC***
        //new[]{} array is been converted to ICommand<TResponse>,
        //this conversion from less detailed object (object) to a more detailed one (ICommand) is call covariant conversion
    }
}