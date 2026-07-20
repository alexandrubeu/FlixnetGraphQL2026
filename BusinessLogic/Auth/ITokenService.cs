using Entities;

namespace BusinessLogic.Auth;

public interface ITokenService
{
    (string Token, DateTime Expires) GenerateToken(EUser user);
}
