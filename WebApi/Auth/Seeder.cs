using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Auth;

public static class Seeder
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

    public static async Task SeedAdminUserAsync(IServiceProvider services, string adminUserName, string adminPassword)
    {
        var userManager = services.GetRequiredService<UserManager<EUser>>();

        var admin = await userManager.FindByNameAsync("admin");
        if (admin is null)
        {
            admin = new EUser() { UserName = adminUserName };
            var createResult = await userManager.CreateAsync(admin, adminPassword);
            if (!createResult.Succeeded)
                throw new InvalidOperationException(
                    "Failed to seed admin user: " + string.Join(" ", createResult.Errors.Select(e => e.Description)));
        }

        if (!await userManager.IsInRoleAsync(admin, Roles.Admin))
            await userManager.AddToRoleAsync(admin, Roles.Admin);
    }
}
