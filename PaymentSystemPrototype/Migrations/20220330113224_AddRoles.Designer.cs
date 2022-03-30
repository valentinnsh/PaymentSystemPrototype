﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentSystemPrototype;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220330113224_AddRoles")]
    partial class AddRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.HasKey("UserId");

                    b.ToTable("balances");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Amount = 100
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
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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
                            Email = "Igor@gmail.com",
                            FirstName = "Igor",
                            LastName = "Igorev",
                            Password = "password",
                            RegisteredAt = new DateTime(2022, 3, 30, 11, 32, 24, 820, DateTimeKind.Utc).AddTicks(3795)
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

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
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

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRoleRecord", b =>
                {
                    b.HasOne("PaymentSystemPrototype.Models.RoleRecord", "RoleRecord")
                        .WithOne("UserRoleRecord")
                        .HasForeignKey("PaymentSystemPrototype.Models.UserRoleRecord", "RoleId")
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

            modelBuilder.Entity("PaymentSystemPrototype.Models.RoleRecord", b =>
                {
                    b.Navigation("UserRoleRecord")
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentSystemPrototype.Models.UserRecord", b =>
                {
                    b.Navigation("BalanceRecord")
                        .IsRequired();

                    b.Navigation("UserRoleRecord")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
