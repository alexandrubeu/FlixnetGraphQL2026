namespace BusinessLogic.Auth;

// Records compare by value, immutable,
public record LoginInput(string Username, string Password);

public record AuthPayload(string Username, string Token, DateTime ExpiresAt);
