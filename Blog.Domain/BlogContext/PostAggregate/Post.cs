using Blog.Domain.Abstraction;
using Blog.Domain.BlogContext.PostAggregate.ValueObjects;
using Blog.Domain.BlogContext.WriterAggregate.ValueObjects;

namespace Blog.Domain.BlogContext.PostAggregate;

public sealed class Post : AggregateRoot<PostId>
{
    private Post() { }
    
    public new PostId Id { get; private set; } = null!;
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Thumbnail { get; private set; }
    public WriterId WriterId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateTime { get; private set; }
    
    public static Post Create(string title, string content, WriterId writerId, string thumbnail)
    {
        return new Post
        {
            Id = PostId.CreateUnique(),
            Title = title,
            Content = content,
            WriterId = writerId,
            Thumbnail = thumbnail,
            CreationDate = DateTime.Now
        };
    }
}