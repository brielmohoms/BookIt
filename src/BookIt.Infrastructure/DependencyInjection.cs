﻿using Asp.Versioning;
using BookIt.Application.Abstractions.Authentication;
using BookIt.Application.Abstractions.Caching;
using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Abstractions.Data;
using BookIt.Application.Abstractions.Email;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Apartments;
using BookIt.Domain.Bookings;
using BookIt.Domain.Reviews;
using BookIt.Domain.Users;
using BookIt.Infrastructure.Authentication;
using BookIt.Infrastructure.Authorization;
using BookIt.Infrastructure.Caching;
using BookIt.Infrastructure.Clock;
using BookIt.Infrastructure.Data;
using BookIt.Infrastructure.Email;
using BookIt.Infrastructure.Outbox;
using BookIt.Infrastructure.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using AuthenticationOptions = BookIt.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = BookIt.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = BookIt.Application.Abstractions.Authentication.IAuthenticationService;

namespace BookIt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // register date time provider
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        // register email service
        services.AddTransient<IEmailService, EmailService>();
        
        AddPersistence(services, configuration);

        AddAuthentication(services, configuration);
        
        AddAuthorization(services);
        
        AddCaching(services, configuration);
        
        AddHealthChecks(services, configuration);
        
        AddApiVersioning(services);
        
        AddBackgroundJobs(services, configuration);

        return services;
    }
    
    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = 
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));
        
        // to register EF Core
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IApartmentRepository, ApartmentRepository>();

        services.AddScoped<IBookingRepository, BookingRepository>();
        
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
        
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(); // used to set up the jwt authentication options

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>(); // used to set up the jwt authentication options()
        
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });
        
        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();

        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
        
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!)
            .AddUrlGroup(new Uri(configuration["KeyCloak:BaseUrl"]!), HttpMethod.Get, "keycloak");
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc()
            .AddApiExplorer(options => 
            { 
                options.GroupNameFormat = "'v'V"; 
                options.SubstituteApiVersionInUrl = true; 
            });
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}