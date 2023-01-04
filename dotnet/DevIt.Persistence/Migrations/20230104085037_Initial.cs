﻿using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Projekte",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjektId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjektId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Pbis_Projekte_ProjektId1",
                        column: x => x.ProjektId1,
                        principalTable: "Projekte",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pbis_ProjektId",
                table: "Pbis",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Pbis_ProjektId1",
                table: "Pbis",
                column: "ProjektId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pbis");

            migrationBuilder.DropTable(
                name: "Projekte");
        }
    }
}
