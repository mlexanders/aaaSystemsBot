using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aaaSystemsApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dialogs",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsNeedAnswer = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogs", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "DialogMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_DialogMessages_Dialogs_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Dialogs",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    PK = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Senders_Dialogs_PK",
                        column: x => x.PK,
                        principalTable: "Dialogs",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialogMessages_ChatId",
                table: "DialogMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Senders_PK",
                table: "Senders",
                column: "PK",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogMessages");

            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.DropTable(
                name: "Dialogs");
        }
    }
}
