using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.WalletId)
                .HasColumnName("wallet_id")
                .IsRequired();

            builder.Property(x => x.WalletId)
                .IsRequired();

            builder.HasOne(x => x.Wallet)
                .WithOne()
                .HasForeignKey<User>(x => x.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
