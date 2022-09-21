﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tiberna.MyFinancePal.AssetsService.API.Models;

#nullable disable

namespace AssetsService.API.Migrations
{
    [DbContext(typeof(AssetsDbContext))]
    [Migration("20220921193347_AddedInvestments")]
    partial class AddedInvestments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("account_currency_id");

                    b.Property<decimal>("ActualBalance")
                        .HasColumnType("numeric")
                        .HasColumnName("actual_balance");

                    b.Property<string>("BankAccountId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("bank_account_id");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<decimal>("InitialBalance")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("initial_balance");

                    b.Property<DateTime>("InitialBalanceDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initial_balance_date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.HasIndex("AccountCurrencyId")
                        .HasDatabaseName("ix_accounts_account_currency_id");

                    b.HasIndex("BankAccountId")
                        .IsUnique()
                        .HasDatabaseName("ix_accounts_bank_account_id")
                        .HasFilter("bank_account_id IS NOT NULL");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_accounts_name");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_currencies");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_currencies_code");

                    b.ToTable("currencies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "EUR",
                            Name = "Euro"
                        },
                        new
                        {
                            Id = 2,
                            Code = "USD",
                            Name = "Dollar"
                        },
                        new
                        {
                            Id = 3,
                            Code = "GBP",
                            Name = "British Pound"
                        },
                        new
                        {
                            Id = 4,
                            Code = "BRL",
                            Name = "Brazilian Real"
                        });
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.ExchangeRate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("ExchangeDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("exchange_date");

                    b.Property<decimal>("Rate")
                        .HasPrecision(12, 6)
                        .HasColumnType("numeric(12,6)")
                        .HasColumnName("rate");

                    b.Property<int>("SourceCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("source_currency_id");

                    b.Property<int?>("SourceId")
                        .HasColumnType("integer")
                        .HasColumnName("source_id");

                    b.Property<int>("TargetCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("target_currency_id");

                    b.Property<int?>("TargetId")
                        .HasColumnType("integer")
                        .HasColumnName("target_id");

                    b.HasKey("Id")
                        .HasName("pk_exchange_rates");

                    b.HasIndex("SourceId")
                        .HasDatabaseName("ix_exchange_rates_source_id");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_exchange_rates_target_id");

                    b.HasIndex("SourceCurrencyId", "TargetCurrencyId", "ExchangeDate")
                        .IsUnique()
                        .HasDatabaseName("ix_exchange_rates_source_currency_id_target_currency_id_exchan");

                    b.ToTable("exchange_rates", (string)null);
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("account_currency_id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<decimal>("AvailableBalance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("available_balance");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<decimal>("InitialBalance")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("initial_balance");

                    b.Property<DateTime>("InitialBalanceDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initial_balance_date");

                    b.Property<decimal>("InvestedBalance")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("invested_balance");

                    b.Property<int>("InvestmentTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("investment_type_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_investment_accounts");

                    b.HasIndex("AccountCurrencyId")
                        .HasDatabaseName("ix_investment_accounts_account_currency_id");

                    b.HasIndex("InvestmentTypeId")
                        .HasDatabaseName("ix_investment_accounts_investment_type_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_investment_accounts_name");

                    b.ToTable("investment_accounts", (string)null);
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_investment_account_types");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_investment_account_types_name");

                    b.ToTable("investment_account_types", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "STOCKS",
                            Name = "Stocks"
                        },
                        new
                        {
                            Id = 2,
                            Code = "CRYPTO",
                            Name = "Cryptocurrency"
                        },
                        new
                        {
                            Id = 3,
                            Code = "PPR",
                            Name = "Retirement Plans"
                        },
                        new
                        {
                            Id = 4,
                            Code = "SAVE",
                            Name = "Savings"
                        },
                        new
                        {
                            Id = 5,
                            Code = "REAL",
                            Name = "Real Estate"
                        },
                        new
                        {
                            Id = 6,
                            Code = "COLLECT",
                            Name = "Collections"
                        });
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("amount");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<int>("InvestmentAccountId")
                        .HasColumnType("integer")
                        .HasColumnName("investment_account_id");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("integer")
                        .HasColumnName("transaction_id");

                    b.Property<long?>("TransactionId1")
                        .HasColumnType("bigint")
                        .HasColumnName("transaction_id1");

                    b.HasKey("Id")
                        .HasName("pk_investment_transactions");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_investment_transactions_currency_id");

                    b.HasIndex("InvestmentAccountId")
                        .HasDatabaseName("ix_investment_transactions_investment_account_id");

                    b.HasIndex("TransactionId1")
                        .HasDatabaseName("ix_investment_transactions_transaction_id1");

                    b.ToTable("investment_transactions", (string)null);
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("amount");

                    b.Property<string>("BankTransactionId")
                        .HasColumnType("text")
                        .HasColumnName("bank_transaction_id");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsIncome")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_income");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_transactions_account_id");

                    b.HasIndex("BankTransactionId")
                        .IsUnique()
                        .HasDatabaseName("ix_transactions_bank_transaction_id")
                        .HasFilter("bank_transaction_id IS NOT NULL");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_transactions_currency_id");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Account", b =>
                {
                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "AccountCurrency")
                        .WithMany()
                        .HasForeignKey("AccountCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_accounts_currencies_account_currency_id");

                    b.Navigation("AccountCurrency");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.ExchangeRate", b =>
                {
                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .HasConstraintName("fk_exchange_rates_currencies_source_id");

                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .HasConstraintName("fk_exchange_rates_currencies_target_id");

                    b.Navigation("Source");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccount", b =>
                {
                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "AccountCurrency")
                        .WithMany()
                        .HasForeignKey("AccountCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_investment_accounts_currencies_account_currency_id");

                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccountType", "InvestmentType")
                        .WithMany()
                        .HasForeignKey("InvestmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_investment_accounts_investment_account_types_investment_typ");

                    b.Navigation("AccountCurrency");

                    b.Navigation("InvestmentType");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentTransaction", b =>
                {
                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_investment_transactions_currencies_currency_id");

                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccount", "InvestmentAccount")
                        .WithMany("InvestmentTransactions")
                        .HasForeignKey("InvestmentAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_investment_transactions_investment_accounts_investment_acco");

                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId1")
                        .HasConstraintName("fk_investment_transactions_transactions_transaction_id1");

                    b.Navigation("Currency");

                    b.Navigation("InvestmentAccount");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Transaction", b =>
                {
                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_accounts_account_id");

                    b.HasOne("Tiberna.MyFinancePal.AssetsService.API.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_currencies_currency_id");

                    b.Navigation("Account");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Tiberna.MyFinancePal.AssetsService.API.Models.InvestmentAccount", b =>
                {
                    b.Navigation("InvestmentTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
