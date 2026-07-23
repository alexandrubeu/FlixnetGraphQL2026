using BusinessLogic.Auth;

namespace WebApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutations))]
public class AuthMutation
{
    public Task<AuthPayload> Login(IAuthService authService, LoginInput input)
    {
        return authService.LoginAsync(input);
    }

    // i will try this later:
    // mutation {
    // login(input: { username: "gabit", password: "pass" }) {
    //     username
    //     token
    //     expiresAt
    // }
    // }

    public Task<AuthPayload> Register(IAuthService authService, LoginInput input)
    {
        return authService.RegisterAsync(input);
    }
}
