using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Update8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastModifiedTime", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 1, 7, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 1, 1, 7, 0, 0, 0, DateTimeKind.Local), "$2a$11$1d46SJBejzJx78nxb6Fthu1Vo/dpR9DxEMQ0PSDC.QJJ5LfxUqKyS", new DateTime(2025, 1, 1, 7, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastModifiedTime", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9633), "$2a$11$SWzfYhnwGx.8jgmxip/vtedd7cJvKf07YFOsE3GbsfvxksvEh.UTG", new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9802) });
        }
    }
}
