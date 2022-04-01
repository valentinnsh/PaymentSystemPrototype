using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class More_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "email", "first_name", "last_name", "password" },
                values: new object[] { "Admin@gmail.com", "Admin", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "block", "email", "first_name", "last_name", "password", "registered_at" },
                values: new object[,]
                {
                    { 2, false, "Kyc@gmail.com", "Kyc", "Kyc", "kyc", new DateTime(2021, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc) },
                    { 3, false, "Funds@gmail.com", "Funds", "Funds", "funds", new DateTime(2020, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc) },
                    { 4, false, "User1@gmail.com", "User1", "User1", "user1", new DateTime(2022, 3, 30, 17, 29, 3, 605, DateTimeKind.Utc) },
                    { 5, false, "User2@gmail.com", "User2", "User2", "user2", new DateTime(2022, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "balances",
                columns: new[] { "user_id", "amount" },
                values: new object[,]
                {
                    { 2, 100 },
                    { 3, 100 },
                    { 4, 100 },
                    { 5, 100 }
                });

            migrationBuilder.InsertData(
                table: "fund_transfers",
                columns: new[] { "id", "amount", "card_number", "confirmed_at", "confirmed_by", "created_at", "status", "user_id" },
                values: new object[,]
                {
                    { 1, 100, 1234567812345678L, null, null, new DateTime(2022, 3, 28, 14, 29, 3, 605, DateTimeKind.Utc), 2, 4 },
                    { 2, -10, 8765432112345678L, null, null, new DateTime(2022, 3, 24, 11, 29, 3, 605, DateTimeKind.Utc), 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "role_id", "user_id" },
                values: new object[,]
                {
                    { 2, 3, 2 },
                    { 3, 4, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "verefications",
                columns: new[] { "user_id", "last_change_date", "reviewer", "status" },
                values: new object[,]
                {
                    { 4, new DateTime(2022, 3, 29, 14, 29, 3, 605, DateTimeKind.Utc), null, 2 },
                    { 5, new DateTime(2022, 3, 28, 14, 29, 3, 605, DateTimeKind.Utc), null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "balances",
                keyColumn: "user_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "fund_transfers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "verefications",
                keyColumn: "user_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "verefications",
                keyColumn: "user_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "email", "first_name", "last_name", "password" },
                values: new object[] { "Igor@gmail.com", "Igor", "Igorev", "password" });
        }
    }
}
