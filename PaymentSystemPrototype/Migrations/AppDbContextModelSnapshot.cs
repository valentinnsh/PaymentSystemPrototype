﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentSystemPrototype;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaymentSystemPrototype.Models.BalanceRecord", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<float>("Amount")
                        .HasColumnType("real")
                        .HasColumnName("amount");

                    b.HasKey("UserId");

                    b.ToTable("balances");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Amount = 100f
                        },
                        new
                        {
                            UserId = 2,
                            Amount = 100f
                        },
                        new
                        {
                            UserId = 3,
                            Amount = 100f
                        },
                        new
                        {
                            UserId = 4,
                            Amount = 100f
                        },
                        new
                        {
                            UserId = 5,
                            Amount = 100f
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.RoleRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "KycManager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "FundsManager"
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.TransferRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real")
                        .HasColumnName("amount");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("card_number");

                    b.Property<DateTime?>("ConfirmedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("confirmed_at");

                    b.Property<int?>("ConfirmedBy")
                        .HasColumnType("integer")
                        .HasColumnName("confirmed_by");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ConfirmedBy");

                    b.HasIndex("UserId");

                    b.ToTable("fund_transfers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100f,
                            CardNumber = 1234567812345678L,
                            CreatedAt = new DateTime(2022, 3, 28, 14, 29, 3, 605, DateTimeKind.Utc),
                            Status = 2,
                            UserId = 4
                        },
                        new
                        {
                            Id = 2,
                            Amount = -10f,
                            CardNumber = 8765432112345678L,
                            CreatedAt = new DateTime(2022, 3, 24, 11, 29, 3, 605, DateTimeKind.Utc),
                            Status = 2,
                            UserId = 5
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Block")
                        .HasColumnType("boolean")
                        .HasColumnName("block");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("registered_at");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Block = false,
                            Email = "admin@gmail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = "admin",
                            RegisteredAt = new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 2,
                            Block = false,
                            Email = "kyc@gmail.com",
                            FirstName = "Kyc",
                            LastName = "Kyc",
                            Password = "kyc",
                            RegisteredAt = new DateTime(2021, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 3,
                            Block = false,
                            Email = "funds@gmail.com",
                            FirstName = "Funds",
                            LastName = "Funds",
                            Password = "funds",
                            RegisteredAt = new DateTime(2020, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 4,
                            Block = false,
                            Email = "user1@gmail.com",
                            FirstName = "User1",
                            LastName = "User1",
                            Password = "user1",
                            RegisteredAt = new DateTime(2022, 3, 30, 17, 29, 3, 605, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 5,
                            Block = false,
                            Email = "user2@gmail.com",
                            FirstName = "User2",
                            LastName = "User2",
                            Password = "user2",
                            RegisteredAt = new DateTime(2022, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRoleRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 3,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            RoleId = 4,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            RoleId = 1,
                            UserId = 4
                        },
                        new
                        {
                            Id = 5,
                            RoleId = 1,
                            UserId = 5
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.VereficationRecord", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("LastChangeDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_change_date");

                    b.Property<string>("Reviewer")
                        .HasColumnType("text")
                        .HasColumnName("reviewer");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("UserId");

                    b.ToTable("verefications");

                    b.HasData(
                        new
                        {
                            UserId = 4,
                            LastChangeDate = new DateTime(2022, 3, 29, 14, 29, 3, 605, DateTimeKind.Utc),
                            Status = 2
                        },
                        new
                        {
                            UserId = 5,
                            LastChangeDate = new DateTime(2022, 3, 28, 14, 29, 3, 605, DateTimeKind.Utc),
                            Status = 2
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.BalanceRecord", b =>
                {
                    b.HasOne("PaymentSystemPrototype.Models.UserRecord", "UserRecord")
                        .WithOne("BalanceRecord")
                        .HasForeignKey("PaymentSystemPrototype.Models.BalanceRecord", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRecord");
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.TransferRecord", b =>
                {
                    b.HasOne("PaymentSystemPrototype.Models.UserRecord", "FundsUserRecord")
                        .WithMany("ManagerTransferRecords")
                        .HasForeignKey("ConfirmedBy");

                    b.HasOne("PaymentSystemPrototype.Models.UserRecord", "UserRecord")
                        .WithMany("TransferRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FundsUserRecord");

                    b.Navigation("UserRecord");
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRoleRecord", b =>
                {
                    b.HasOne("PaymentSystemPrototype.Models.RoleRecord", "RoleRecord")
                        .WithMany("UserRoleRecord")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentSystemPrototype.Models.UserRecord", "UserRecord")
                        .WithOne("UserRoleRecord")
                        .HasForeignKey("PaymentSystemPrototype.Models.UserRoleRecord", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleRecord");

                    b.Navigation("UserRecord");
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.VereficationRecord", b =>
                {
                    b.HasOne("PaymentSystemPrototype.Models.UserRecord", "UserRecord")
                        .WithOne("VereficationRecord")
                        .HasForeignKey("PaymentSystemPrototype.Models.VereficationRecord", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRecord");
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.RoleRecord", b =>
                {
                    b.Navigation("UserRoleRecord");
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRecord", b =>
                {
                    b.Navigation("BalanceRecord")
                        .IsRequired();

                    b.Navigation("ManagerTransferRecords");

                    b.Navigation("TransferRecords");

                    b.Navigation("UserRoleRecord")
                        .IsRequired();

                    b.Navigation("VereficationRecord")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
