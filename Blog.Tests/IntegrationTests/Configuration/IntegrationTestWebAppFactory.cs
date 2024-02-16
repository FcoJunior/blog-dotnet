using Blog.Infra.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;

namespace Blog.Tests.IntegrationTests.Configuration;

public sealed class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private Respawner _respawn = default!;
    private NpgsqlConnection _dbConnection = default!;
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("blog_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationContext>));
            
            if (descriptor is not null)
                services.Remove(descriptor);
            
            services.AddDbContext<ApplicationContext>(option =>
            {
                option
                    .UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await InitializeMigrationAsync();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await InitializeRespawnAsync();
    }
    
    private async Task InitializeMigrationAsync()
    {
        using var scope = this.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await dbContext.Database.MigrateAsync();
    }
    
    private async Task InitializeRespawnAsync()
    {
        await _dbConnection.OpenAsync();
        _respawn = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new [] { "public" },
            TablesToIgnore = new [] { new Table("__EFMigrationsHistory") }
        });
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
    
    public async Task ResetDatabase()
    {
        await _respawn.ResetAsync(_dbConnection);
    }
}