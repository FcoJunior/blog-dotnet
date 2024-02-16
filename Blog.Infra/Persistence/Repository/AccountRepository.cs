using Blog.Application.Interfaces.Data;
using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.AuthenticationContext.AccountAggregate.Repository;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Blog.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Persistence.Repository;

public sealed class AccountRepository : IAccountRepository
{
    private readonly IApplicationContext _context;

    public AccountRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountByEmail(Email email, CancellationToken cancellationToken)
        => await _context.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
}