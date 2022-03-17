using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinMyCarTrip.Data.Data.Migrations
{
    public partial class PetCollectionToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "26e0136f-29bd-497d-b2ff-9df11019188e", "cfcf2ab4-f796-4fa9-a84b-9cf028db4d96" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "04b26bcc-bd59-42b4-94cb-0eca55385f13", "a6b36e91-466e-47dc-b5e4-3a927cbb4a03" });
        }
    }
}
