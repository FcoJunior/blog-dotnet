using Blog.Domain.Abstraction;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Blog.Domain.BlogContext.WriterAggregate.ValueObjects;

namespace Blog.Domain.BlogContext.WriterAggregate;

public sealed class Writer : AggregateRoot<WriterId>
{
    private Writer() { }
    
    public new WriterId Id { get; private set; } = null!;
    public string Name { get; private set; }
    public string CoverLetter { get; private set; }
    public string Photo { get; private set; }
    public AccountId AccountId { get; private set; }

    public static Writer Create(string name, string coverLetter, string photo, AccountId accountId)
    {
        return new Writer
        {
            Id = WriterId.CreateUnique(),
            Name = name,
            CoverLetter = coverLetter,
            Photo = photo,
            AccountId = accountId
        };
    }
}