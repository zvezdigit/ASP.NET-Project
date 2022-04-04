using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinMyCarTrip.Data.Data.Migrations
{
    public partial class NewRoleSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "73a944f1-a19a-4a81-b9c9-f540b5a3eff8", "1", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a3b9ced7-4d48-4a85-9897-a6ecd74d018e", "e7bf53e7-d23b-4c6d-aeb3-27b51fd747b4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a944f1-a19a-4a81-b9c9-f540b5a3eff8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6f22d933-5e97-4f3d-915f-064f1b83611d", "81cd2a05-b6ed-4471-8525-47c7dd405662" });
        }
    }
}
