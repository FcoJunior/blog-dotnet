using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Persistence.EntityConfiguration;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("account", "public");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                accountId => accountId.Value, 
                value => AccountId.Create(value));

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .HasMaxLength(250)
            .IsRequired();

        builder.OwnsOne(p => p.Password, pwsBuilder =>
        {
            pwsBuilder.Property(x => x.Salt).HasColumnName("salt").IsRequired();
            pwsBuilder.Property(x => x.Hash).HasColumnName("hash").IsRequired();
        });

        builder.HasIndex(x => x.Email).IsUnique();
    }
}