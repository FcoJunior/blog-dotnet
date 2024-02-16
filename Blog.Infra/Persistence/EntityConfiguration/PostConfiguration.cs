using Blog.Domain.BlogContext.PostAggregate;
using Blog.Domain.BlogContext.PostAggregate.ValueObjects;
using Blog.Domain.BlogContext.WriterAggregate;
using Blog.Domain.BlogContext.WriterAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Persistence.EntityConfiguration;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("post", "public");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                postId => postId.Value, 
                value => PostId.Create(value));
        
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Content)
            .HasColumnName("content")
            .IsRequired();
        
        builder.Property(x => x.Thumbnail)
            .HasColumnName("thumbnail")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(x => x.CreationDate)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdateTime)
            .HasColumnName("updated_at")
            .IsRequired(false);
        
        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);
        
        builder.Property(x => x.WriterId)
            .HasColumnName("writer_id")
            .HasConversion(
                writerId => writerId.Value, 
                value => WriterId.Create(value))
            .IsRequired();
        
        builder.HasOne<Writer>()
            .WithMany()
            .HasForeignKey(x => x.WriterId);
    }
}