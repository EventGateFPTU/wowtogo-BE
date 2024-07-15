using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveUsedAtToCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedAt",
                schema: "wowtogo",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UsedInFormat",
                schema: "wowtogo",
                table: "Tickets");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UsedAt",
                schema: "wowtogo",
                table: "Checkins",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "UsedInFormat",
                schema: "wowtogo",
                table: "Checkins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedAt",
                schema: "wowtogo",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "UsedInFormat",
                schema: "wowtogo",
                table: "Checkins");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UsedAt",
                schema: "wowtogo",
                table: "Tickets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsedInFormat",
                schema: "wowtogo",
                table: "Tickets",
                type: "integer",
                nullable: true);
        }
    }
}
