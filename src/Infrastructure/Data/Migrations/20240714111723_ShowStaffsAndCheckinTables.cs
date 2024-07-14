using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShowStaffsAndCheckinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShowId",
                schema: "wowtogo",
                table: "Staffs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checkins",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShowId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkins_Shows_ShowId",
                        column: x => x.ShowId,
                        principalSchema: "wowtogo",
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkins_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "wowtogo",
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowStaffs",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShowId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowStaffs_Shows_ShowId",
                        column: x => x.ShowId,
                        principalSchema: "wowtogo",
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowStaffs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "wowtogo",
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_ShowId",
                schema: "wowtogo",
                table: "Staffs",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_ShowId",
                schema: "wowtogo",
                table: "Checkins",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_TicketId",
                schema: "wowtogo",
                table: "Checkins",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowStaffs_ShowId",
                schema: "wowtogo",
                table: "ShowStaffs",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowStaffs_StaffId",
                schema: "wowtogo",
                table: "ShowStaffs",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Shows_ShowId",
                schema: "wowtogo",
                table: "Staffs",
                column: "ShowId",
                principalSchema: "wowtogo",
                principalTable: "Shows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Shows_ShowId",
                schema: "wowtogo",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "Checkins",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "ShowStaffs",
                schema: "wowtogo");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_ShowId",
                schema: "wowtogo",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ShowId",
                schema: "wowtogo",
                table: "Staffs");
        }
    }
}
