using Blog.Domain.Abstraction;

namespace Blog.Domain.BlogContext.PostAggregate.ValueObjects;

public sealed record PostId : ValueObject
{
    public Guid Value { get; init; }
    
    private PostId()
    {
        Value = Guid.NewGuid();
    }
    
    private PostId(Guid value)
    {
        Value = value;
    }

    public static PostId Create(Guid value) => new(value); 
    
    public static PostId CreateUnique() => new(); 

    public override string ToString()
    {
        return Value.ToString();
    }
};