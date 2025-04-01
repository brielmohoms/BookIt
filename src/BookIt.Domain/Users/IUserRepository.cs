namespace BookIt.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void AddUser(User user);
}