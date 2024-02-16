using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

namespace Blog.Domain.AuthenticationContext.AccountAggregate.Repository;

public interface IAccountRepository
{
    public Task<Account?> GetAccountByEmail(Email email, CancellationToken cancellationToken);
}