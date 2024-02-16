using Blog.Application.Authentication.Commands.SignIn;
using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Tests.IntegrationTests.Abstractions;
using Blog.Tests.IntegrationTests.Configuration;
using FluentAssertions;

namespace Blog.Tests.IntegrationTests.Tests.Authentication;

[Collection("WebApplicationCollectionFixture")]
public sealed class SignInIntegrationTest : BaseIntegrationTest
{
    public SignInIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Signin_WheDataIsValid_ShouldReturnToken()
    {
        try
        {
            var account = Account.Create("account@mail.com", "password");
            await Context.Accounts.AddAsync(account);
            await Context.SaveChangesAsync();

            var command = new SignInCommand("account@mail.com", "password");
            var result = await Sender.Send(command);

            result.IsError.Should().BeFalse();
            result.Value.AccessToken.Should().NotBeNull();
        }
        finally
        {
            await ResetDatabase();
        }
    }
}