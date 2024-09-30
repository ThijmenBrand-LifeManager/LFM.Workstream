using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.WorkStream.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkStreams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 9, 29, 20, 13, 59, 20, DateTimeKind.Unspecified).AddTicks(7150), new TimeSpan(0, 2, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStreams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkStreams");
        }
    }
}
