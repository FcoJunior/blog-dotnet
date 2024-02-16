using Blog.Tests.IntegrationTests.Abstractions;
using Blog.Tests.IntegrationTests.Configuration;

namespace Blog.Tests.IntegrationTests.Tests.Authentication;

[Collection("WebApplicationCollectionFixture")]
public sealed class SignUpIntegrationTest : BaseIntegrationTest
{
    public SignUpIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
}