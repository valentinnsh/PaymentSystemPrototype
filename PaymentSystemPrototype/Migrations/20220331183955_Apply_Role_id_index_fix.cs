using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Apply_Role_id_index_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 18, 39, 55, 560, DateTimeKind.Utc).AddTicks(799));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 18, 31, 23, 159, DateTimeKind.Utc).AddTicks(8666));
        }
    }
}
