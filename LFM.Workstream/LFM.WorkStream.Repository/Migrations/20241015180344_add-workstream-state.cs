using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.WorkStream.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addworkstreamstate : Migration
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
                    State = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 10, 15, 20, 3, 44, 5, DateTimeKind.Unspecified).AddTicks(3930), new TimeSpan(0, 2, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStreams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 10, 15, 20, 3, 44, 5, DateTimeKind.Unspecified).AddTicks(4570), new TimeSpan(0, 2, 0, 0, 0))),
                    WorkStreamId = table.Column<string>(type: "text", nullable: false),
                    WorkStreamId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_WorkStreams_WorkStreamId1",
                        column: x => x.WorkStreamId1,
                        principalTable: "WorkStreams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkStreamId1",
                table: "Projects",
                column: "WorkStreamId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "WorkStreams");
        }
    }
}
