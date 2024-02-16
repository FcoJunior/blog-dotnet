using Blog.Application.Interfaces.Auth;
using Blog.Application.Interfaces.Data;
using Blog.Domain.AuthenticationContext.AccountAggregate.Repository;
using Blog.Domain.BlogContext.WriterAggregate.Repository;
using Blog.Infra.Auth;
using Blog.Infra.Persistence.Context;
using Blog.Infra.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Ports and Adapters
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtProvider, JwtProvider>();
        
        // Persistence
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BlogDB")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IWriterRepository, WriterRepository>();
        
        return services;
    }    
}