using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinMyCarTrip.Data.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8c2b9c6-17b9-4cc3-b3d6-d2006471dc82", "1", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "670b3e98-faec-4026-994a-cd3a4a231bf4", 0, "afc79b1f-d045-4614-a887-1017778918ca", "pesho@abv.com", false, "Super Pesho", false, null, null, "PESHO@ABV.BG", "AQAAAAEAACcQAAAAEKSVbyMRRKapKw7uTWWqVsNkuegEau2em6hA5EWnPzoTn6uFGWWhktxPAr08m6k3xw==", null, false, "243be7ec-b751-45c4-a2a2-4eb03b42e690", false, "pesho@abv.bg" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c8c2b9c6-17b9-4cc3-b3d6-d2006471dc82", "670b3e98-faec-4026-994a-cd3a4a231bf4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8c2b9c6-17b9-4cc3-b3d6-d2006471dc82", "670b3e98-faec-4026-994a-cd3a4a231bf4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8c2b9c6-17b9-4cc3-b3d6-d2006471dc82");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "670b3e98-faec-4026-994a-cd3a4a231bf4");
        }
    }
}
