using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aaaSystemsApi.Migrations
{
    /// <inheritdoc />
    public partial class EditRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_Dialogs_DialogId",
                table: "DialogMessages");

            migrationBuilder.DropIndex(
                name: "IX_DialogMessages_DialogId",
                table: "DialogMessages");

            migrationBuilder.DropColumn(
                name: "DialogId",
                table: "DialogMessages");

            migrationBuilder.CreateIndex(
                name: "IX_DialogMessages_ChatId",
                table: "DialogMessages",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_Senders_ChatId",
                table: "DialogMessages",
                column: "ChatId",
                principalTable: "Senders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_Senders_ChatId",
                table: "DialogMessages");

            migrationBuilder.DropIndex(
                name: "IX_DialogMessages_ChatId",
                table: "DialogMessages");

            migrationBuilder.AddColumn<long>(
                name: "DialogId",
                table: "DialogMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DialogMessages_DialogId",
                table: "DialogMessages",
                column: "DialogId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_Dialogs_DialogId",
                table: "DialogMessages",
                column: "DialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
