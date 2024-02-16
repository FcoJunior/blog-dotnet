using Blog.Domain.Abstraction;

namespace Blog.Domain.BlogContext.PostAggregate.Events;

public sealed record PostPublished(Post Post) : IDomainEvent;