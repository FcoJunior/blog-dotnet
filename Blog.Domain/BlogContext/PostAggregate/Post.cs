using Blog.Domain.Abstraction;
using Blog.Domain.BlogContext.PostAggregate.Events;
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
    public StatusPost Status { get; private set; }
    public WriterId WriterId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UpdateTime { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    public static Post Create(string title, string content, WriterId writerId, string thumbnail)
    {
        return new Post
        {
            Id = PostId.CreateUnique(),
            Title = title,
            Content = content,
            WriterId = writerId,
            Thumbnail = thumbnail,
            Status = StatusPost.InProgress,
            CreationDate = DateTime.Now
        };
    }

    public void Update(string title, string content, string thumbnail)
    {
        this.Title = title;
        this.Content = content;
        this.Thumbnail = thumbnail;
        this.UpdateTime = DateTime.Now;
    }

    public void Publish()
    {
        this.Status = StatusPost.Published;
        this.UpdateTime = DateTime.Now;
        this.Raise(new PostPublished(this));
    }
}