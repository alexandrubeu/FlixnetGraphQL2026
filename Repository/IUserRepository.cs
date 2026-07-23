using Entities;

namespace Repository;

public interface IUserRepository
{
    Task<EUser?> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(EUser user);
}
