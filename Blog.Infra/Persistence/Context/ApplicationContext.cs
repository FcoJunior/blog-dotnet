using Blog.Application.Interfaces.Data;
using Blog.Domain.Abstraction;
using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.BlogContext.WriterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.Infra.Persistence.Context;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration? getService) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Writer> Writers { get; set; } = null!;
}