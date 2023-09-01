using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eintraege",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Stunden = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Abrechenbar = table.Column<bool>(type: "bit", nullable: false),
                    ExterneId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eintraege", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projekte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ExterneId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pbis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pbis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pbis_Projekte_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eintraege_ExterneId",
                table: "Eintraege",
                column: "ExterneId",
                unique: true,
                filter: "[ExterneId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pbis_ProjektId",
                table: "Pbis",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekte_ExterneId",
                table: "Projekte",
                column: "ExterneId",
                unique: true,
                filter: "[ExterneId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eintraege");

            migrationBuilder.DropTable(
                name: "Pbis");

            migrationBuilder.DropTable(
                name: "Projekte");
        }
    }
}
