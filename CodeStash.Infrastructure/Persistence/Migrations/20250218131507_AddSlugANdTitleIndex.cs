using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeStash.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugANdTitleIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Snippets_Slug",
                table: "Snippets",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Snippets_Title",
                table: "Snippets",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Snippets_Slug",
                table: "Snippets");

            migrationBuilder.DropIndex(
                name: "IX_Snippets_Title",
                table: "Snippets");
        }
    }
}
