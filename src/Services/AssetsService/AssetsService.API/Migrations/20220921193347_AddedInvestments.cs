using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AssetsService.API.Migrations
{
    public partial class AddedInvestments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exchange_rates_currencies_source_id",
                table: "exchange_rates");

            migrationBuilder.DropForeignKey(
                name: "fk_exchange_rates_currencies_target_id",
                table: "exchange_rates");

            migrationBuilder.AddColumn<bool>(
                name: "is_income",
                table: "transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "target_id",
                table: "exchange_rates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "source_id",
                table: "exchange_rates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "account_currency_id",
                table: "accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "actual_balance",
                table: "accounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "investment_account_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_investment_account_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "investment_accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    investment_type_id = table.Column<int>(type: "integer", nullable: false),
                    account_currency_id = table.Column<int>(type: "integer", nullable: false),
                    initial_balance_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    initial_balance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    invested_balance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    available_balance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_investment_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_investment_accounts_currencies_account_currency_id",
                        column: x => x.account_currency_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_investment_accounts_investment_account_types_investment_typ",
                        column: x => x.investment_type_id,
                        principalTable: "investment_account_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "investment_transactions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    currency_id = table.Column<int>(type: "integer", nullable: false),
                    investment_account_id = table.Column<int>(type: "integer", nullable: false),
                    transaction_id1 = table.Column<long>(type: "bigint", nullable: true),
                    transaction_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_investment_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_investment_transactions_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_investment_transactions_investment_accounts_investment_acco",
                        column: x => x.investment_account_id,
                        principalTable: "investment_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_investment_transactions_transactions_transaction_id1",
                        column: x => x.transaction_id1,
                        principalTable: "transactions",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "investment_account_types",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, "STOCKS", "Stocks" },
                    { 2, "CRYPTO", "Cryptocurrency" },
                    { 3, "PPR", "Retirement Plans" },
                    { 4, "SAVE", "Savings" },
                    { 5, "REAL", "Real Estate" },
                    { 6, "COLLECT", "Collections" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_accounts_account_currency_id",
                table: "accounts",
                column: "account_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_investment_account_types_name",
                table: "investment_account_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_investment_accounts_account_currency_id",
                table: "investment_accounts",
                column: "account_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_investment_accounts_investment_type_id",
                table: "investment_accounts",
                column: "investment_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_investment_accounts_name",
                table: "investment_accounts",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_investment_transactions_currency_id",
                table: "investment_transactions",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_investment_transactions_investment_account_id",
                table: "investment_transactions",
                column: "investment_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_investment_transactions_transaction_id1",
                table: "investment_transactions",
                column: "transaction_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_currencies_account_currency_id",
                table: "accounts",
                column: "account_currency_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_exchange_rates_currencies_source_id",
                table: "exchange_rates",
                column: "source_id",
                principalTable: "currencies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exchange_rates_currencies_target_id",
                table: "exchange_rates",
                column: "target_id",
                principalTable: "currencies",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_currencies_account_currency_id",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_exchange_rates_currencies_source_id",
                table: "exchange_rates");

            migrationBuilder.DropForeignKey(
                name: "fk_exchange_rates_currencies_target_id",
                table: "exchange_rates");

            migrationBuilder.DropTable(
                name: "investment_transactions");

            migrationBuilder.DropTable(
                name: "investment_accounts");

            migrationBuilder.DropTable(
                name: "investment_account_types");

            migrationBuilder.DropIndex(
                name: "ix_accounts_account_currency_id",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "is_income",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "account_currency_id",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "actual_balance",
                table: "accounts");

            migrationBuilder.AlterColumn<int>(
                name: "target_id",
                table: "exchange_rates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "source_id",
                table: "exchange_rates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_exchange_rates_currencies_source_id",
                table: "exchange_rates",
                column: "source_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_exchange_rates_currencies_target_id",
                table: "exchange_rates",
                column: "target_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
