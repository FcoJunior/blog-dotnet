using Blog.Domain.AuthenticationContext.AccountAggregate;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Interfaces.Data;

public interface IApplicationContext
{
    DbSet<Account> Accounts { get; set; }
}