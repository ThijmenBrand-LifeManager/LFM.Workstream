using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.WorkStream.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "WorkStreams",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 10, 4, 17, 14, 8, 25, DateTimeKind.Unspecified).AddTicks(6450), new TimeSpan(0, 1, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 9, 29, 20, 13, 59, 20, DateTimeKind.Unspecified).AddTicks(7150), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkStreamId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 10, 4, 17, 14, 8, 25, DateTimeKind.Unspecified).AddTicks(7030), new TimeSpan(0, 1, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "WorkStreams",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 9, 29, 20, 13, 59, 20, DateTimeKind.Unspecified).AddTicks(7150), new TimeSpan(0, 2, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 10, 4, 17, 14, 8, 25, DateTimeKind.Unspecified).AddTicks(6450), new TimeSpan(0, 1, 0, 0, 0)));
        }
    }
}
