using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Eintraege : Migration
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
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stunden = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Abrechenbar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eintraege", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eintraege");
        }
    }
}
