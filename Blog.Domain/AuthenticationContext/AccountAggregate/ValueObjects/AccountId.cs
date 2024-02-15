using Blog.Domain.Abstraction;

namespace Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

public sealed record AccountId : ValueObject
{
    public Guid Value { get; init; }

    private AccountId() => Value = Guid.NewGuid();
    
    private AccountId(Guid value) => Value = value;

    public static AccountId Create(Guid value) => new(value);
    
    public static AccountId CreateUnique() => new();
    
    public override string ToString()
    {
        return Value.ToString();
    }
}