using Entities;

namespace BusinessLogic.Auth;

public interface ITokenService
{
    Task<(string Token, DateTime Expires)> GenerateTokenAsync(EUser user);
}
