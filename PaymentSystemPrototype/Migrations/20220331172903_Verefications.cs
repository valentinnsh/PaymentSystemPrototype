using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Verefications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "verefications",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    last_change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reviewer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verefications", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_verefications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc).AddTicks(9577));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "verefications");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 16, 51, 44, 495, DateTimeKind.Utc).AddTicks(922));
        }
    }
}
