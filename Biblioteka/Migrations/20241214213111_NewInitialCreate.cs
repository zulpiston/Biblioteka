using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tytul",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RokWydania",
                table: "Books",
                newName: "YearPublished");

            migrationBuilder.RenameColumn(
                name: "CzyDostepna",
                table: "Books",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Books",
                newName: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearPublished",
                table: "Books",
                newName: "RokWydania");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Tytul");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Books",
                newName: "CzyDostepna");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "Autor");
        }
    }
}
