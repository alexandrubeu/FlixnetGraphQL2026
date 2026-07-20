namespace BusinessLogic.Auth;

public interface IAuthService
{
    Task<AuthPayload> LoginAsync(LoginInput loginInput);
}
