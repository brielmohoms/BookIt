using BookIt.Domain.Users;

namespace BookIt.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

}