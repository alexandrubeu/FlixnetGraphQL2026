using Entities;
using Microsoft.AspNetCore.Identity;
using Repository;

namespace BusinessLogic.Auth;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher<EUser> passwordHasher,
    ITokenService tokenService
) : IAuthService
{
    public async Task<AuthPayload> LoginAsync(LoginInput loginInput)
    {
        var user = await userRepository.GetUserByUsernameAsync(loginInput.Username);
        if (user is null)
            throw new Exception();
        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            loginInput.Password
        );
        if (result == PasswordVerificationResult.Failed)
            throw new Exception();
        var (token, expires) = tokenService.GenerateToken(user);
        return new AuthPayload(user.Username, token, expires);
    }

    public async Task<AuthPayload> RegisterAsync(LoginInput registerInput)
    {

        var existing = await userRepository.GetUserByUsernameAsync(registerInput.Username);

        if (existing is not null)
            throw new Exception();

        var user = new EUser { Username = registerInput.Username };
        user.PasswordHash = passwordHasher.HashPassword(user, registerInput.Password);

        await userRepository.CreateUserAsync(user);

        var (token, expires) = tokenService.GenerateToken(user);
        
        return new AuthPayload(user.Username, token, expires);
    }
}
