using BusinessLogic.Auth;
using Entities;
using HotChocolate;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task LoginAsync_ThrowsWhenUserNotFound()
    {
        var userManager = new Mock<UserManager<EUser>>(
            Mock.Of<IUserStore<EUser>>(), null, null, null, null, null, null, null, null); // only store is needed

        var tokenService = new Mock<ITokenService>();
        var authService = new AuthService(userManager.Object, tokenService.Object);

        await Assert.ThrowsAsync<GraphQLException>(() =>
            authService.LoginAsync(new LoginInput("nonexistent", "password"))
        );
        // use 'dotnet test Tests'
    }

    [Fact]
    public async Task registerAsync_ThrowsWhenUserExists()
    {
        var userManager = new Mock<UserManager<EUser>>(
            Mock.Of<IUserStore<EUser>>(), null, null, null, null, null, null, null, null);
        var tokenService = new Mock<ITokenService>();
        var authService = new AuthService(userManager.Object, tokenService.Object);

        userManager.Setup(umProxy => umProxy.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new EUser());

        await Assert.ThrowsAsync<GraphQLException>(() =>
            authService.RegisterAsync(new LoginInput("username", "password")));
    }
}
