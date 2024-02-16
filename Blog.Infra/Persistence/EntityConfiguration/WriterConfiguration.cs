using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Blog.Domain.BlogContext.WriterAggregate;
using Blog.Domain.BlogContext.WriterAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Persistence.EntityConfiguration;

internal sealed class WriterConfiguration : IEntityTypeConfiguration<Writer>
{
    public void Configure(EntityTypeBuilder<Writer> builder)
    {
        builder.ToTable("writer", "public");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                writerId => writerId.Value, 
                value => WriterId.Create(value));

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id")
            .HasConversion(
                accountId => accountId.Value, 
                value => AccountId.Create(value));

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.CoverLetter)
            .HasColumnName("cover_letter")
            .HasMaxLength(400)
            .IsRequired();
        
        builder.Property(x => x.Photo)
            .HasColumnName("photo")
            .HasMaxLength(36)
            .IsRequired();
        
        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Writer>(x => x.AccountId);
    }
}