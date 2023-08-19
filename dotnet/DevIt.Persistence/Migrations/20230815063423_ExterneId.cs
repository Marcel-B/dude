using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExterneId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projekte",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExterneId",
                table: "Projekte",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pbis",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Eintraege",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExterneId",
                table: "Eintraege",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projekte_ExterneId",
                table: "Projekte",
                column: "ExterneId",
                unique: true,
                filter: "[ExterneId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eintraege_ExterneId",
                table: "Eintraege",
                column: "ExterneId",
                unique: true,
                filter: "[ExterneId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projekte_ExterneId",
                table: "Projekte");

            migrationBuilder.DropIndex(
                name: "IX_Eintraege_ExterneId",
                table: "Eintraege");

            migrationBuilder.DropColumn(
                name: "ExterneId",
                table: "Projekte");

            migrationBuilder.DropColumn(
                name: "ExterneId",
                table: "Eintraege");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projekte",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pbis",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Eintraege",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);
        }
    }
}
