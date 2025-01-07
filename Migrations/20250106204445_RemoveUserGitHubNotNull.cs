using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserGitHubNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GitHub",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "DATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 20, 44, 45, 465, DateTimeKind.Utc).AddTicks(4132),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2025, 1, 6, 20, 41, 6, 701, DateTimeKind.Utc).AddTicks(3245));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GitHub",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "DATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2025, 1, 6, 20, 41, 6, 701, DateTimeKind.Utc).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2025, 1, 6, 20, 44, 45, 465, DateTimeKind.Utc).AddTicks(4132));
        }
    }
}
