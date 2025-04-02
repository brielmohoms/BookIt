using BookIt.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BookIt.Application;

public static class DependencyInjection // responsible to register the services specific to the application layer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<PricingService>();
        
        return services;
    }
}