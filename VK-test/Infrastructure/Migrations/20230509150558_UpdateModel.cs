using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_Login",
                table: "User",
                column: "Login");

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Admin", "Aдминистратор" },
                    { 2, "User", "Пользователь" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Active", "Активный пользователей" },
                    { 2, "Blocked", "Заблокированый пользователь" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_Login",
                table: "User");

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
