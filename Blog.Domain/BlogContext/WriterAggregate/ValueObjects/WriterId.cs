using Blog.Domain.Abstraction;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

namespace Blog.Domain.BlogContext.WriterAggregate.ValueObjects;

public record WriterId : ValueObject
{
    public Guid Value { get; init; }
    
    private WriterId()
    {
        Value = Guid.NewGuid();
    }
    
    private WriterId(Guid value)
    {
        Value = value;
    }

    public static WriterId Create(Guid value) => new(value); 
    
    public static WriterId CreateUnique() => new(); 

    public override string ToString()
    {
        return Value.ToString();
    }
};