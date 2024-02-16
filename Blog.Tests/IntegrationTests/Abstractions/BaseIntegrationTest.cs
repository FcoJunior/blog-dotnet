using Blog.Infra.Persistence.Context;
using Blog.Tests.IntegrationTests.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Tests.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;
    protected readonly ISender Sender;
    protected readonly ApplicationContext Context;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        Context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    }
    
    protected Task ResetDatabase() => _factory.ResetDatabase();
}