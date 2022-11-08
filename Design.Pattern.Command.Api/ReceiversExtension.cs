using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Design.Pattern.Command.Api;

public static class ReceiversExtension
{
    public static IServiceCollection AddReceivers(this IServiceCollection services, params Type[] assemnlyTypes)
    {
        //var receiversDict = Dictionary<Type, Type>();
        foreach (var types in assemnlyTypes)
        {
            var assembly = types.Assembly;
            // var commands = assembly.GetTypes().Where(x =>
            // {
            //     var requestImplentation = x.GetInterfaces().Where(y => y.IsGenericType)
            //         .Any(z => z.GetGenericTypeDefinition() == typeof(ICommand<>));
            //     return !x.IsInterface && !x.IsAbstract && requestImplentation;
            // }).ToList();

            var receivers = assembly.GetTypes().Where(x =>
            {
                var requestImplentation = x.GetInterfaces().Where(y => y.IsGenericType)
                    .Any(z => z.GetGenericTypeDefinition() == typeof(IReceiver<,>));
                return !x.IsInterface && !x.IsAbstract && requestImplentation;
            }).ToList();
            
           //commands.ForEach(cmd =>
           //{
           //    receiversDict[cmd] =
           //        receivers.SingleOrDefault(rec => cmd == rec.GetInterface("IReceiver`2")!.GetGenericArguments()[0]);
           //});

           var serviceDescriptor = receivers.Select(x => new ServiceDescriptor(x, x, ServiceLifetime.Scoped));
           services.TryAdd(serviceDescriptor);
        }

        return services;
    }
}