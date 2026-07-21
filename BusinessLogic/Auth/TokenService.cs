using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Auth;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = "MovieAPI";
    public string Audience { get; set; } = "MovieAPI-Client";
    public int ExpiryMinutes { get; set; }
}

public class TokenService(
    IOptions<JwtSettings> options,
    UserManager<EUser> userManager,
    RoleManager<IdentityRole<int>> roleManager
) : ITokenService
{
    private readonly JwtSettings _settings = options.Value;

    public async Task<(string Token, DateTime Expires)> GenerateTokenAsync(EUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
        };
        var roles = await userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
            var role = await roleManager.FindByNameAsync(roleName);
            if (role is not null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims.Where(claim => claim.Type == "permission"));
            }
        }

        var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _settings.Issuer,
            _settings.Audience,
            claims,
            expires: expires,
            signingCredentials: credentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
