﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Personal_Financial_WebApi.Data;

#nullable disable

namespace Personal_Financial_WebApi.Migrations
{
    [DbContext(typeof(TtDbContext))]
    partial class TtDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Donation", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SentTo")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<short>("Amount")
                        .HasColumnType("smallint");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserIdentifier", "SentTo", "Date");

                    b.HasIndex("UserIdentifier", "Date", "SentTo");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Expense", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Category")
                        .HasColumnType("varchar(10)");

                    b.Property<byte>("Month")
                        .HasColumnType("tinyint");

                    b.Property<short>("Year")
                        .HasColumnType("smallint");

                    b.Property<short>("Limit")
                        .HasColumnType("smallint");

                    b.Property<short>("Spend")
                        .HasColumnType("smallint");

                    b.HasKey("UserIdentifier", "Category", "Month", "Year");

                    b.HasIndex("UserIdentifier", "Category", "Month", "Year");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Loan", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SecondStakeHolder")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<short>("Amount")
                        .HasColumnType("smallint");

                    b.Property<byte>("Done")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Owned")
                        .HasColumnType("tinyint");

                    b.HasKey("UserIdentifier", "SecondStakeHolder", "Date");

                    b.HasIndex("UserIdentifier", "SecondStakeHolder", "Date");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Save4Good", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Item")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<short>("Amount")
                        .HasColumnType("smallint");

                    b.HasKey("UserIdentifier", "Item", "Date");

                    b.HasIndex("UserIdentifier", "Item", "Date");

                    b.ToTable("Save4Goods");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.User", b =>
                {
                    b.Property<string>("Identifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("TotalBalance")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalDonated")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalSavingForGood")
                        .HasColumnType("money");

                    b.HasKey("Identifier");

                    b.HasIndex("Identifier");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.WishItem", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Item")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("Amount")
                        .HasColumnType("smallmoney");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserIdentifier", "Item", "Date");

                    b.HasIndex("UserIdentifier", "Item", "Date");

                    b.ToTable("WishItems");
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Donation", b =>
                {
                    b.HasOne("Personal_Financial_WebApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Expense", b =>
                {
                    b.HasOne("Personal_Financial_WebApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Loan", b =>
                {
                    b.HasOne("Personal_Financial_WebApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.Save4Good", b =>
                {
                    b.HasOne("Personal_Financial_WebApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Financial_WebApi.Data.Entities.WishItem", b =>
                {
                    b.HasOne("Personal_Financial_WebApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}