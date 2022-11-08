using Design.Pattern.Command.Api.Commands;
using Design.Pattern.Command.Api.Receivers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Design.Pattern.Command.Api;

public static class ReceiversExtension
{
    public static IServiceCollection AddReceivers(this IServiceCollection services, params Type[] assemnlyTypes)
    {
        foreach (var types in assemnlyTypes)
        {
            var assembly = types.Assembly;

            var receivers = assembly.GetTypes().Where(x =>
            {
                var requestImplentation = x.GetInterfaces().Where(y => y.IsGenericType)
                    .Any(z => z.GetGenericTypeDefinition() == typeof(IReceiver<,>));
                return !x.IsInterface && !x.IsAbstract && requestImplentation;
            }).ToList();
            
           var serviceDescriptor = receivers.Select(x => new ServiceDescriptor(x, x, ServiceLifetime.Scoped));
           services.TryAdd(serviceDescriptor);
        }

        return services;
    }
}