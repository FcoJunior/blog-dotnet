namespace Blog.Domain.Abstraction;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject
{
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
}