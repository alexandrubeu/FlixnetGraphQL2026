using BusinessLogic.Auth;

namespace WebApi.GraphQL.Mutations;

[MutationType]
public static class AuthMutation
{
    public static Task<AuthPayload> Login(IAuthService authService, LoginInput input)
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

    public static Task<AuthPayload> Register(IAuthService authService, LoginInput input)
    {
        return authService.RegisterAsync(input);
    }
}
