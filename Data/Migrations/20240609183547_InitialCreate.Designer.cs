﻿// <auto-generated />
using System;
using BankingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingSystem.Data.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20240609183547_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingSystem.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<int>("AccountType").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BankingSystem.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwiftCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("BankingSystem.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("BankingSystem.Entities.Card", b =>
                {
                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CardNumber");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("BankingSystem.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BankingSystem.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RepaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("BankingSystem.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LoanId")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id", "LoanId");

                    b.HasIndex("LoanId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BankingSystem.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingSystem.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("HashedPassword")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankingSystem.Entities.CheckingsAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Entities.Account");

                    b.Property<double>("OverdraftLimit")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("BankingSystem.Entities.SavingsAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Entities.Account");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BankingSystem.Entities.Account", b =>
                {
                    b.HasOne("BankingSystem.Entities.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Entities.Branch", b =>
                {
                    b.HasOne("BankingSystem.Entities.Bank", "Bank")
                        .WithMany("Branches")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("BankingSystem.Entities.Card", b =>
                {
                    b.HasOne("BankingSystem.Entities.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BankingSystem.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Entities.Employee", b =>
                {
                    b.HasOne("BankingSystem.Entities.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("BankingSystem.Entities.Loan", b =>
                {
                    b.HasOne("BankingSystem.Entities.Branch", "Branch")
                        .WithMany("Loans")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingSystem.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Entities.Payment", b =>
                {
                    b.HasOne("BankingSystem.Entities.Loan", "Loan")
                        .WithMany("Payments")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingSystem.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Entities.Transaction", b =>
                {
                    b.HasOne("BankingSystem.Entities.Card", "Card")
                        .WithMany("Transactions")
                        .HasForeignKey("CardNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("BankingSystem.Entities.User", b =>
                {
                    b.HasOne("BankingSystem.Entities.Employee", "Employee")
                        .WithMany("Users")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BankingSystem.Entities.Account", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("BankingSystem.Entities.Bank", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("BankingSystem.Entities.Branch", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Loans");
                });

            modelBuilder.Entity("BankingSystem.Entities.Card", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BankingSystem.Entities.Employee", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BankingSystem.Entities.Loan", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("BankingSystem.Entities.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Cards");

                    b.Navigation("Loans");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
