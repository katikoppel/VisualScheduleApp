using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualScheduleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class scheduleitemupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "ScheduleItems",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "ScheduleItems");
        }
    }
}
