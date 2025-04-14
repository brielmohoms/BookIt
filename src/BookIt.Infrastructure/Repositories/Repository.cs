using BookIt.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Infrastructure.Repositories;

// generic repository base class
internal abstract class Repository<T> 
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    // fetches an entity by Id
    public async Task<T?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default) 
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    // adds an entity to the database
    public virtual void Add(T entity) // virtual to allow us override the entity in the user repository 
    {
        DbContext.Set<T>().Add(entity);
    }
}