using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aaaSystemsApi.Migrations
{
    /// <inheritdoc />
    public partial class EditDialogMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "DialogMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "DialogMessages");
        }
    }
}
