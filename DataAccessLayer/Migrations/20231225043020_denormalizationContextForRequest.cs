using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class denormalizationContextForRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordDate",
                table: "UserRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 25, 4, 30, 20, 645, DateTimeKind.Utc).AddTicks(2739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 25, 3, 42, 34, 65, DateTimeKind.Utc).AddTicks(9596));

            migrationBuilder.AddColumn<bool>(
                name: "UpdateType",
                table: "UserRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateType",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordDate",
                table: "UserRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 25, 3, 42, 34, 65, DateTimeKind.Utc).AddTicks(9596),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 25, 4, 30, 20, 645, DateTimeKind.Utc).AddTicks(2739));
        }
    }
}
