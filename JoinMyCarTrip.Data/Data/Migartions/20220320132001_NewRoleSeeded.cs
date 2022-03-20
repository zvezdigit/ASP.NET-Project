using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinMyCarTrip.Data.Data.Migartions
{
    public partial class NewRoleSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "687cd1fa-ba03-4f05-b341-3a0bb817b16e", "1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "18753ecc-eb37-4319-82c8-6f5200bb5982", "9688c2db-dfba-44f2-9a68-1142e6e97c3f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "687cd1fa-ba03-4f05-b341-3a0bb817b16e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "967f97c1-e4c9-44f3-8783-cde70b5db172", "4d7900f1-b934-4991-a215-654c6ad14ca4" });
        }
    }
}
