using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Model = Infrastructure.Models.UsersState;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { nameof(Model.Id), nameof(Model.Code), nameof(Model.Description) },
                columnTypes: new[] { "integer", "text", "text" },
                values: new object[,]
                {
                    { 1, UserGroupCode.Admin.ToString(), "Aдминистратор" },
                    { 2, UserGroupCode.User.ToString(), "Пользователь" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Group",
                new[] {
                    nameof(Model.Id),
                },
                new object[,] {
                    { 1 },
                    { 2 },
                }
            );
        }
    }
}
