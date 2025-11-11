using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CenterId", "CreatedAt", "CreatedBy", "Email", "FullName", "IsActive", "IsDeleted", "LastModifiedTime", "PasswordHash", "PhoneNumber", "RecordNumber", "RoleId", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, null, new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9800), 0, "admin@gmail.com", "System Administrator", true, false, new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9633), "$2a$11$SWzfYhnwGx.8jgmxip/vtedd7cJvKf07YFOsE3GbsfvxksvEh.UTG", null, 1, 1, new DateTime(2025, 11, 11, 2, 17, 54, 468, DateTimeKind.Utc).AddTicks(9802), 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
