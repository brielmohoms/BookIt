using BookIt.Application.Abstractions.Behaviors;
using BookIt.Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookIt.Application;

public static class DependencyInjection // responsible to register the services specific to the application layer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>)); // once we send our command, it is going to enter the logging behavior, run the logging
                                                                       // statement and execute the command handler before returning the response
                                                                       
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<PricingService>();
        
        return services;
    }
}