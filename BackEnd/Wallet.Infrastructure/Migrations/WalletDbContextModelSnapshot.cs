﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wallet.Infrastructure.Data.Context;

#nullable disable

namespace Wallet.Infrastructure.Migrations
{
    [DbContext(typeof(WalletDbContext))]
    partial class WalletDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Wallet.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Amout")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("ReceiverWalletId")
                        .HasColumnType("uuid")
                        .HasColumnName("receiver_wallet_id");

                    b.Property<Guid>("SenderWalletId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_wallet_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverWalletId");

                    b.HasIndex("SenderWalletId");

                    b.HasIndex("WalletId");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("wallets", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Wallet.Domain.Entities.Wallet", "ReceiverWallet")
                        .WithMany()
                        .HasForeignKey("ReceiverWalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wallet.Domain.Entities.Wallet", "SenderWallet")
                        .WithMany()
                        .HasForeignKey("SenderWalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wallet.Domain.Entities.Wallet", null)
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ReceiverWallet");

                    b.Navigation("SenderWallet");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("Wallet.Domain.Entities.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("Wallet.Domain.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User", b =>
                {
                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
