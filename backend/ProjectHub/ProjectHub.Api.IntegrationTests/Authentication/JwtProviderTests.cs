using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;

using ProjectHub.Domain.Entities;
using ProjectHub.Domain.Enums;
using ProjectHub.Domain.ValueObjects;
using ProjectHub.Persistence.Authentication;

namespace ProjectHub.Api.IntegrationTests.Authentication;

public sealed class JwtProviderTests
{
    private static JwtProvider CreateProvider()
    {
        var options = Options.Create(
            new JwtOptions
            {
                Issuer = "ProjectHub",
                Audience = "ProjectHub.Client",
                SecretKey = "THIS_IS_A_DEVELOPMENT_SECRET_KEY_AT_LEAST_32_CHARS",
                AccessTokenExpirationInMinutes = 15
            });

        return new JwtProvider(options);
    }

    private static User CreateUser()
    {
        return User.Create(
            fullName: "Nguyen Van A",
            email: Email.Create("user@example.com"),
            avatarUrl: null);
    }

    [Fact]
    public void GenerateAccessToken_Should_ReturnToken()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public void GenerateAccessToken_Should_ReturnValidJwt()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        Assert.NotNull(jwt);
    }

    [Fact]
    public void GenerateAccessToken_Should_ContainSubjectClaim()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        var subject = jwt.Claims.First(
            x => x.Type == JwtRegisteredClaimNames.Sub);

        Assert.Equal(user.Id.ToString(), subject.Value);
    }

    [Fact]
    public void GenerateAccessToken_Should_ContainEmailClaim()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        var email = jwt.Claims.First(
            x => x.Type == JwtRegisteredClaimNames.Email);

        Assert.Equal(user.Email.Value, email.Value);
    }

    [Fact]
    public void GenerateAccessToken_Should_ContainJti()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        var jti = jwt.Claims.First(
            x => x.Type == JwtRegisteredClaimNames.Jti);

        Assert.False(string.IsNullOrWhiteSpace(jti.Value));
    }

    [Fact]
    public void GenerateAccessToken_Should_SetIssuer()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        Assert.Equal("ProjectHub", jwt.Issuer);
    }

    [Fact]
    public void GenerateAccessToken_Should_SetAudience()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var token = provider.GenerateAccessToken(user);

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        Assert.Single(jwt.Audiences);

        Assert.Equal(
            "ProjectHub.Client",
            jwt.Audiences.First());
    }

    [Fact]
    public void GenerateAccessToken_Should_SetExpiration()
    {
        var provider = CreateProvider();
        var user = CreateUser();

        var before = DateTime.UtcNow;

        var token = provider.GenerateAccessToken(user);

        var after = DateTime.UtcNow;

        var jwt = new JwtSecurityTokenHandler()
            .ReadJwtToken(token);

        Assert.True(jwt.ValidTo >= before.AddMinutes(14));

        Assert.True(jwt.ValidTo <= after.AddMinutes(16));
    }
}