using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class eventauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "EventItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_AuthorId",
                table: "EventItems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventItems_Users_AuthorId",
                table: "EventItems",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventItems_Users_AuthorId",
                table: "EventItems");

            migrationBuilder.DropIndex(
                name: "IX_EventItems_AuthorId",
                table: "EventItems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "EventItems");
        }
    }
}
