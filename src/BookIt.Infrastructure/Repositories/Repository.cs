﻿using BookIt.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Infrastructure.Repositories;

// generic repository base class
internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        this.DbContext = dbContext;
    }

    // fetches an entity by Id
    public async Task<T> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default) 
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    // adds an entity to the database
    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
}