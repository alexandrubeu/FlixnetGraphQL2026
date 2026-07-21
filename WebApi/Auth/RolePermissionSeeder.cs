using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Auth;

public static class RolePermissionSeeder
{
    public static async Task SeedRolePermissionsAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        foreach (var (roleName, permissions) in RolePermissions.RolePermissionsMap)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role is null)
            {
                role = new IdentityRole<int> { Name = roleName };
                await roleManager.CreateAsync(role);
            }

            var dbClaims = await roleManager.GetClaimsAsync(role);
            var dbPermissions = dbClaims
                .Where(claim => claim.Type == "permission")
                .Select(claim => claim.Value)
                .ToHashSet(); // (O(1) lookups)

            // add missing
            foreach (var permission in permissions.Where(perm => !dbPermissions.Contains(perm)))
                await roleManager.AddClaimAsync(role, new Claim("permission", permission));

            // remove unneeded
            foreach (
                var staleClaim in dbClaims.Where(claim =>
                    claim.Type == "permission" && !permissions.Contains(claim.Value)
                )
            )
                await roleManager.RemoveClaimAsync(role, staleClaim);
        }
    }
}
