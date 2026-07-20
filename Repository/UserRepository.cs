using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public Task<EUser?> GetUserByUsernameAsync(string username)
    {
        return dbContext
            .Users.Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task CreateUserAsync(EUser user)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }
}
