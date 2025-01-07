using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class CreateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "DATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2024, 12, 31, 21, 30, 6, 576, DateTimeKind.Utc).AddTicks(137),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2024, 12, 14, 13, 54, 53, 715, DateTimeKind.Utc).AddTicks(9240));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "DATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 13, 54, 53, 715, DateTimeKind.Utc).AddTicks(9240),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2024, 12, 31, 21, 30, 6, 576, DateTimeKind.Utc).AddTicks(137));
        }
    }
}
