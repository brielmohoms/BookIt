using BookIt.Domain.Users;

namespace BookIt.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public override void Add(User user)
    {
        foreach (var userRole in user.Roles)
        {
            DbContext.Attach(userRole);
        }
        
        DbContext.Add(user);
    }
}