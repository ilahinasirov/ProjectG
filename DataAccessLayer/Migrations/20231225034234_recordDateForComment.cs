using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class recordDateForComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecordDate",
                table: "UserRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 25, 3, 42, 34, 65, DateTimeKind.Utc).AddTicks(9596));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordDate",
                table: "UserRequests");
        }
    }
}
