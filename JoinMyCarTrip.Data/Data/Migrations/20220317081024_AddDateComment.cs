using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinMyCarTrip.Data.Data.Migrations
{
    public partial class AddDateComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "04b26bcc-bd59-42b4-94cb-0eca55385f13", "a6b36e91-466e-47dc-b5e4-3a927cbb4a03" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "afc79b1f-d045-4614-a887-1017778918ca", "243be7ec-b751-45c4-a2a2-4eb03b42e690" });
        }
    }
}
