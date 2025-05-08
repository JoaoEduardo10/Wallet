using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Data.Mapping
{
    internal class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.SenderWalletId)
                .HasColumnName("sender_wallet_id")
                .IsRequired();

            builder.Property(x => x.ReceiverWalletId)
                .HasColumnName("receiver_wallet_id")
                .IsRequired();

            builder.Property(x => x.Amout)
                .HasColumnName("amount")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.HasOne(x => x.SenderWallet)
                .WithMany()
                .HasForeignKey(x => x.SenderWalletId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(x => x.ReceiverWallet)
                .WithMany()
                .HasForeignKey(x => x.ReceiverWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
