using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Exceptions;
using BookIt.Domain.Abstractions;
using BookIt.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookIt.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    
    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) 
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
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
            AddDomainEventsAsOutboxMessages();
            
            var result = await base.SaveChangesAsync(cancellationToken); // base async save changes method

            return result;
        }
        catch (DbUpdateException dbUpdateException)
        {
            throw new ConcurrencyException("Concurrency exception occured.", dbUpdateException);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvent = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvent;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                _dateTimeProvider.Now, 
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();
        
        AddRange(outboxMessages);
    }
}