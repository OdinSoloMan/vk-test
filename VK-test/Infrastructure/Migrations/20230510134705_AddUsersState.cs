using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Model = Infrastructure.Models.UsersState;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { nameof(Model.Id), nameof(Model.Code), nameof(Model.Description) },
                columnTypes: new[] { "integer", "text", "text" },
                values: new object[,]
                {
                    { 1, UserStateCode.Active.ToString(), "Активный пользователей" },
                    { 2, UserStateCode.Blocked.ToString(), "Заблокированый пользователь" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "State",
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
