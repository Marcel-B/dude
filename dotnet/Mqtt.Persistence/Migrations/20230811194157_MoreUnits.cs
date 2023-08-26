using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mqtt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoreUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Created", "Name", "ShortName", "Updated" },
                values: new object[,]
                {
                    { new Guid("266e2ee3-869a-473e-8c26-ed333deb9301"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "PRESS", "hPa", null },
                    { new Guid("37cd8490-4d8d-41f2-a188-6c5e74c6e223"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "CO2", "%", null },
                    { new Guid("8b9460ea-6c8e-4dd4-bbe0-a51537f9ce5a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ETHANOL", "%", null },
                    { new Guid("a727103f-5342-428b-9d9a-b3a0958b1640"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "TVOC", "%", null },
                    { new Guid("dac4dba1-0d62-4ba7-90a7-36666cb0caca"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "H2", "%", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("266e2ee3-869a-473e-8c26-ed333deb9301"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("37cd8490-4d8d-41f2-a188-6c5e74c6e223"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8b9460ea-6c8e-4dd4-bbe0-a51537f9ce5a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a727103f-5342-428b-9d9a-b3a0958b1640"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("dac4dba1-0d62-4ba7-90a7-36666cb0caca"));
        }
    }
}
