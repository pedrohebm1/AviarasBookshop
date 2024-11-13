using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviarasBookshop.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteLivro_Livros_LivrosId",
                table: "ClienteLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livros_LivrosId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLivro_Livros_LivrosId",
                table: "PedidoLivro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livros",
                table: "Livros");

            migrationBuilder.RenameTable(
                name: "Livros",
                newName: "Livro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livro",
                table: "Livro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteLivro_Livro_LivrosId",
                table: "ClienteLivro",
                column: "LivrosId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livro_LivrosId",
                table: "LivroAutor",
                column: "LivrosId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoLivro_Livro_LivrosId",
                table: "PedidoLivro",
                column: "LivrosId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteLivro_Livro_LivrosId",
                table: "ClienteLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livro_LivrosId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLivro_Livro_LivrosId",
                table: "PedidoLivro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livro",
                table: "Livro");

            migrationBuilder.RenameTable(
                name: "Livro",
                newName: "Livros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livros",
                table: "Livros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteLivro_Livros_LivrosId",
                table: "ClienteLivro",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livros_LivrosId",
                table: "LivroAutor",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoLivro_Livros_LivrosId",
                table: "PedidoLivro",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
