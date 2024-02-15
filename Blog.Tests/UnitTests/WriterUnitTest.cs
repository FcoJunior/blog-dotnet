using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Blog.Domain.BlogContext.WriterAggregate;
using FluentAssertions;

namespace Blog.Tests.UnitTests;

public sealed class WriterUnitTest
{
    [Fact]
    public void CreateWriter_WhenDataIsValid_ShouldCreateWriter()
    {
        var accountId = AccountId.CreateUnique(); 
        var writer = Writer.Create("Writer Name", "It is a cover letter example...", "photo_link",
            accountId);

        writer.AccountId.Equals(accountId).Should().BeTrue();
        writer.Name.Should().Be("Writer Name");
    }
}