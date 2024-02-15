using Blog.Domain.AuthenticationContext.AccountAggregate;
using FluentAssertions;

namespace Blog.Tests.UnitTests;

public sealed class AccountUnitTest
{
    [Fact]
    public void CreateAccount_WhenDataIsValid_ShouldCreateAccount()
    {
        var account = Account.Create("account@mail.com", "password");
        account.Password.IsMatchedPassword("password").Should().BeTrue();
        account.Email.Value.Should().Be("account@mail.com");
    }
}