using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.BlogContext.PostAggregate;
using Blog.Domain.BlogContext.WriterAggregate;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Interfaces.Data;

public interface IApplicationContext
{
    DbSet<Account> Accounts { get; set; }
    DbSet<Writer> Writers { get; set; }
    DbSet<Post> Posts { get; set; }
}