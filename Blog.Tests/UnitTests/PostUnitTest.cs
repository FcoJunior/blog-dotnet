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
    }
}