using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Infrastructure.Data.Mapping
{
    public class WalletMap : IEntityTypeConfiguration<Wallet.Domain.Entities.Wallet>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Wallet> builder)
        {
            builder.ToTable("wallets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasColumnName("balance")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<Domain.Entities.Wallet>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(x => x.Transactions)
                .WithOne() 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
