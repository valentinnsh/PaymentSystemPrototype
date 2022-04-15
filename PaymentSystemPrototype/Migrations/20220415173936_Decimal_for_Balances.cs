using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Decimal_for_Balances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "fund_transfers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "balances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 1,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 2,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 3,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 4,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 5,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 1,
                column: "amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 2,
                column: "amount",
                value: -10m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "amount",
                table: "fund_transfers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "amount",
                table: "balances",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 1,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 2,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 3,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 4,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 5,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 1,
                column: "amount",
                value: 100f);

            migrationBuilder.UpdateData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 2,
                column: "amount",
                value: -10f);
        }
    }
}
