using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scraper.Data.Migrations
{
    public partial class SnapshotId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SnapshotId",
                table: "Stocks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SnapshotId",
                table: "Stocks");
        }
    }
}
