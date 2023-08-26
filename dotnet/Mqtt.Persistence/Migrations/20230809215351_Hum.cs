using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mqtt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Hum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Created", "Name", "ShortName", "Updated" },
                values: new object[] { new Guid("7d85889d-b1f2-487b-aaff-8f3e8f6af445"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "HUM", "%", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("7d85889d-b1f2-487b-aaff-8f3e8f6af445"));
        }
    }
}
