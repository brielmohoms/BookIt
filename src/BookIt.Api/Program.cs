using BookIt.Api.Extensions;
using BookIt.Application;
using BookIt.Application.Abstractions.Data;
using BookIt.Infrastructure;
using Dapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => // UseSerilog is an extension method
        configuration.ReadFrom.Configuration(context.Configuration)); // configure serilog configuration from the application settings

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
    
    //app.SeedData();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseSerilogRequestLogging(); // this is going to introduce a middleware that is going to hook into the api incoming
                                // request and start logging useful infos

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();