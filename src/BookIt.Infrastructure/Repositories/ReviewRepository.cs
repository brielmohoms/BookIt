﻿using BookIt.Domain.Reviews;

namespace BookIt.Infrastructure.Repositories;

internal sealed class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}