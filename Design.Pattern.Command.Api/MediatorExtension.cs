using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Design.Pattern.Command.Api;

public static class MediatorExtension
{
    /// <summary>
    /// Scan all the types (including the c# internals types) defined in the assembly (Program.cs class in this case)
    /// then get all types that implement ICommand and IReceiver to be resolved by the mediator class.
    /// </summary>
    /// <param name="services">
    /// The type been extended by this extension method
    /// </param>
    /// <param name="assemnlyTypes">
    /// Generally the startup class of the application
    /// </param>
    /// <returns></returns>
    public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] assemnlyTypes)
    {
        //This dictionary is used to hold the ICommand and IReceiver pairs
        //The key is the command and value is the Receiver which handle the command
        var receiversDict = new Dictionary<Type, Type>();
        foreach (var types in assemnlyTypes) // In this case we have only one type in this array, the program.cs
        {
            var assembly = types.Assembly; // the assembly for program.cs is Design.Pattern.Mediator
            //Get all the types in the assembly and filter the result using the where clause
             var commands = assembly.GetTypes().Where(x =>
             {
                 //Let`s assume the the type we get in this iteration of the where clause is UpdateNameCommand
                 var requestImplementation = 
                     //Get all interfaces implemented by the command
                     x.GetInterfaces()
                         //Only if those interface is generics(aka parameter interfaces)
                         .Where(y => y.IsGenericType)
                     //Return true if any of this interfaces found are of type ICommand
                     //This class could implement any other interfaces but we're interested only if it also implement ICommand
                     .Any(z => z.GetGenericTypeDefinition() == typeof(ICommand<>));
                 //Then return this type if it is not an interface(as interfaces also can implement others interfaces)
                 //if is not an abstract class
                 //If requestImplementation is true (that means that it implements the command interface)
                 return !x.IsInterface && !x.IsAbstract && requestImplementation;
             }).ToList();
             
             //Does the same thing as above but for the IReceiver interface
             //In this case the only type that implement IReceiver interface is USerHandler
            var receivers = assembly.GetTypes().Where(x =>
            {
                var requestImplentation = x.GetInterfaces().Where(y => y.IsGenericType)
                    .Any(z => z.GetGenericTypeDefinition() == typeof(IReceiver<,>));
                return !x.IsInterface && !x.IsAbstract && requestImplentation;
            }).ToList();

            //Iterate through the commands found in the first part
            commands.ForEach(cmd =>
            {

                //This is redundant actually
                // var handles = receivers.Where(rec =>
                //     rec.GetInterfaces()
                //         .Where(x => x.Equals(typeof(IReceiver<,>))) != null);

                //Iterate through the receivers and get the one that is a match with the cmd command
                var receiver = receivers.SingleOrDefault(rc =>
                {
                    //We know so far that this list has only one receiver which is UserHandler
                    return rc.GetMethods()
                        //Get all the methods in userHandler
                        //Handle(UpdateNameCommand updateNameCommand) and Handle(UndoUserAction command)
                        //Then check if any of this methods has a parameter of type cmd Command
                        //x here is the method itself
                        .Any(x => x.GetParameters().Any(y => y.ParameterType == cmd));
                });
                
                if(receiver != null)
                    //The key is the command and the value is the receiver which handle the command
                    receiversDict.TryAdd(cmd, receiver); 
                
            });

            //Add the UserHandler to the container of dependence injection
            //ServiceDescriptor will describe how our service will be resolved from the container
            //replace x with there meaning we endUp with new ServiceDescriptor(TheInterface, TheConcreteImplementation, TheLifeCycleOfThisService)
            var serviceDescriptor = receivers.Select(x => new ServiceDescriptor(x, x, ServiceLifetime.Transient));
            //This method allows us to add many ServiceDescriptor at the same time to the container
            //Try add the service if it is already there ignores it
            services.TryAdd(serviceDescriptor);
           
        }
        
        //Add the mediator class to the container
        //We're providing our own instance if the mediator as we need to inject this two parameter that the container can't resolve
        //The first parameter is an extension method that extend the IServiceProvider class with resolve the dependencies and return an instance of it
        //The signature is like that public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        services.AddSingleton<IMediator>( x => new Mediator(x.GetRequiredService, receiversDict));
        return services;
    }
}