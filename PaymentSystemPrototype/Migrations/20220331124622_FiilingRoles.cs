using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class FiilingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 2, "Admin" },
                    { 3, "KYC" },
                    { 4, "Funds Manager" }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 12, 46, 21, 825, DateTimeKind.Utc).AddTicks(6203));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 30, 11, 32, 24, 820, DateTimeKind.Utc).AddTicks(3795));
        }
    }
}
