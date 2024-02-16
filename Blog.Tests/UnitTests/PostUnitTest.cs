using Blog.Domain.BlogContext.PostAggregate;
using Blog.Domain.BlogContext.WriterAggregate.ValueObjects;
using FluentAssertions;

namespace Blog.Tests.UnitTests;

public sealed class PostUnitTest
{
    [Fact]
    public void CreatePost_WhenDataIsValid_ShouldCreatePost()
    {
        var post = Post.Create("Post name", @"<p>This is the HTML content</p>", WriterId.CreateUnique(),
            Guid.NewGuid().ToString());
        post.Title.Should().Be("Post name");
        post.Status.Should().Be(StatusPost.InProgress);
    }

    [Fact]
    public void UpdatePost_WhenDataIsValid_ShouldUpdateValues()
    {
        var post = Post.Create("Post name", @"<p>This is the HTML content</p>", WriterId.CreateUnique(),
            Guid.NewGuid().ToString());
        post.Update("New Title", "New Content", Guid.NewGuid().ToString());
        post.Title.Should().Be("New Title");
        post.Content.Should().Be("New Content");
        post.UpdateTime.Should().NotBeNull();
    }

    [Fact]
    public void PublishPost_WhenDataIsValid_ShouldUpdateStatus()
    {
        var post = Post.Create("Post name", @"<p>This is the HTML content</p>", WriterId.CreateUnique(),
            Guid.NewGuid().ToString());
        post.Publish();
        post.Status.Should().Be(StatusPost.Published);
        post.UpdateTime.Should().NotBeNull();
    }
}