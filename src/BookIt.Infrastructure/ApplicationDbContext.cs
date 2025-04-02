using BookIt.Application.Exceptions;
using BookIt.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) 
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken); // base async save changes method

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw new ConcurrencyException("Concurrency exception occured.", dbUpdateException);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvent = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvent;
            }).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}