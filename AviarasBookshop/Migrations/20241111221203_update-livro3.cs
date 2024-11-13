using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviarasBookshop.Migrations
{
    /// <inheritdoc />
    public partial class updatelivro3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorLivro_Autores_AutoresId",
                table: "AutorLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_AutorLivro_Livros_LivrosId",
                table: "AutorLivro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutorLivro",
                table: "AutorLivro");

            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Livros");

            migrationBuilder.RenameTable(
                name: "AutorLivro",
                newName: "LivroAutor");

            migrationBuilder.RenameIndex(
                name: "IX_AutorLivro_LivrosId",
                table: "LivroAutor",
                newName: "IX_LivroAutor_LivrosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor",
                columns: new[] { "AutoresId", "LivrosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Autores_AutoresId",
                table: "LivroAutor",
                column: "AutoresId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livros_LivrosId",
                table: "LivroAutor",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Autores_AutoresId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livros_LivrosId",
                table: "LivroAutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor");

            migrationBuilder.RenameTable(
                name: "LivroAutor",
                newName: "AutorLivro");

            migrationBuilder.RenameIndex(
                name: "IX_LivroAutor_LivrosId",
                table: "AutorLivro",
                newName: "IX_AutorLivro_LivrosId");

            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutorLivro",
                table: "AutorLivro",
                columns: new[] { "AutoresId", "LivrosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AutorLivro_Autores_AutoresId",
                table: "AutorLivro",
                column: "AutoresId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AutorLivro_Livros_LivrosId",
                table: "AutorLivro",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
