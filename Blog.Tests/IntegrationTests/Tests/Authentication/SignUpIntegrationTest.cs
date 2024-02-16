using Blog.Application.Authentication.Commands.SignUp;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Blog.Tests.IntegrationTests.Abstractions;
using Blog.Tests.IntegrationTests.Configuration;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.IntegrationTests.Tests.Authentication;

[Collection("WebApplicationCollectionFixture")]
public sealed class SignUpIntegrationTest : BaseIntegrationTest
{
    public SignUpIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task SignUp_WheDataIsValid_ShouldCreateAccountAndWriter()
    {
        try
        {
            var command = new SignUpCommand(
                "account@mail.com", 
                "password", 
                "Writer Name", 
                "This is a cover letter",
                Guid.NewGuid().ToString());
            var result = await Sender.Send(command);

            result.IsError.Should().BeFalse();
            var account = await Context.Accounts.Where(x => x.Email == Email.Create("account@mail.com")).SingleAsync();
            account.Should().NotBeNull();
            var writer = await Context.Writers.Where(x => x.AccountId == account.Id).SingleAsync();
            writer.Should().NotBeNull();
        }
        finally
        {
            await ResetDatabase();
        }
    }
}