using Entities;
using HotChocolate;
using Microsoft.AspNetCore.Identity;
using Repository;

namespace BusinessLogic.Auth;

public class AuthService(UserManager<EUser> userManager, ITokenService tokenService) : IAuthService
{
    public async Task<AuthPayload> LoginAsync(LoginInput loginInput)
    {
        var user = await userManager.FindByNameAsync(loginInput.Username);
        if (user is null)
            throw new GraphQLException("Invalid username or password");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, loginInput.Password);
        if (!isPasswordValid)
            throw new GraphQLException("Invalid username or password");

        var (token, expires) = await tokenService.GenerateTokenAsync(user);
        return new AuthPayload(user.UserName!, token, expires);
    }

    public async Task<AuthPayload> RegisterAsync(LoginInput registerInput)
    {
        var existing = await userManager.FindByNameAsync(registerInput.Username);

        if (existing is not null)
            throw new GraphQLException();

        var user = new EUser { UserName = registerInput.Username };

        // so the userManager handles password hashing implicitly
        var result = await userManager.CreateAsync(user, registerInput.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e =>
                ErrorBuilder.New().SetMessage(e.Description).SetCode(e.Code).Build()
            );
            throw new GraphQLException(errors);
        }

        var roleResult = await userManager.AddToRoleAsync(user, Roles.Default);

        if (!roleResult.Succeeded)
        {
            var errors = roleResult.Errors.Select(e =>
                ErrorBuilder.New().SetMessage(e.Description).SetCode(e.Code).Build()
            );
            throw new GraphQLException(errors);
        }

        var (token, expires) = await tokenService.GenerateTokenAsync(user);
        return new AuthPayload(user.UserName!, token, expires);
    }
}
