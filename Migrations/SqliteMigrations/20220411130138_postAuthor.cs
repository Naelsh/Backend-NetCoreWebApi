using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class postAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "PostItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostItems_AuthorId",
                table: "PostItems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostItems_Users_AuthorId",
                table: "PostItems",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostItems_Users_AuthorId",
                table: "PostItems");

            migrationBuilder.DropIndex(
                name: "IX_PostItems_AuthorId",
                table: "PostItems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "PostItems");
        }
    }
}
