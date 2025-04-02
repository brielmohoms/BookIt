using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Abstractions.Data;
using BookIt.Application.Abstractions.Email;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Apartments;
using BookIt.Domain.Bookings;
using BookIt.Domain.Users;
using BookIt.Infrastructure.Clock;
using BookIt.Infrastructure.Data;
using BookIt.Infrastructure.Email;
using BookIt.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookIt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // register date time provider
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        // register email service
        services.AddTransient<IEmailService, EmailService>();
        
        var connectionString = 
            configuration.GetConnectionString("BookItDb") ??
            throw new ArgumentNullException(nameof(configuration));
        
        // to register EF Core
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IApartmentRepository, ApartmentRepository>();

        services.AddScoped<IBookingRepository, BookingRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        
        return services;
    }
}