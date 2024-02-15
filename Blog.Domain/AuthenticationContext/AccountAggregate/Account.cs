using Blog.Domain.Abstraction;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

namespace Blog.Domain.AuthenticationContext.AccountAggregate;

public sealed class Account : AggregateRoot<AccountId>
{
    private Account() { }

    public new AccountId Id { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;

    public static Account Create(string email, string password)
    {
        return new Account
        {
            Id = AccountId.CreateUnique(),
            Email = Email.Create(email),
            Password = Password.Create(password)
        };
    }
}