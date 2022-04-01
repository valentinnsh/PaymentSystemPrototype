using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Transfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fund_transfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    card_number = table.Column<long>(type: "bigint", nullable: false),
                    confirmed_by = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    confirmed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fund_transfers", x => x.id);
                    table.ForeignKey(
                        name: "FK_fund_transfers_users_confirmed_by",
                        column: x => x.confirmed_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fund_transfers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "role_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_fund_transfers_confirmed_by",
                table: "fund_transfers",
                column: "confirmed_by");

            migrationBuilder.CreateIndex(
                name: "IX_fund_transfers_user_id",
                table: "fund_transfers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fund_transfers");

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "role_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registered_at",
                value: new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc).AddTicks(9577));
        }
    }
}
