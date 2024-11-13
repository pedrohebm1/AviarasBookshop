using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviarasBookshop.Migrations
{
    /// <inheritdoc />
    public partial class AddAutorIdToLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteLivro_Livro_LivrosId",
                table: "ClienteLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLivro_Livro_LivrosId",
                table: "PedidoLivro");

            migrationBuilder.DropTable(
                name: "LivroAutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livro",
                table: "Livro");

            migrationBuilder.RenameTable(
                name: "Livro",
                newName: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livros",
                table: "Livros",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorId",
                table: "Livros",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteLivro_Livros_LivrosId",
                table: "ClienteLivro",
                column: "LivrosId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros",
                column: "AutorId",
                principalTable: "Autores",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteLivro_Livros_LivrosId",
                table: "ClienteLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoLivro_Livros_LivrosId",
                table: "PedidoLivro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livros",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_AutorId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Livros");

            migrationBuilder.RenameTable(
                name: "Livros",
                newName: "Livro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livro",
                table: "Livro",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LivroAutor",
                columns: table => new
                {
                    AutoresId = table.Column<int>(type: "int", nullable: false),
                    LivrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroAutor", x => new { x.AutoresId, x.LivrosId });
                    table.ForeignKey(
                        name: "FK_LivroAutor_Autores_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroAutor_Livro_LivrosId",
                        column: x => x.LivrosId,
                        principalTable: "Livro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_LivrosId",
                table: "LivroAutor",
                column: "LivrosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteLivro_Livro_LivrosId",
                table: "ClienteLivro",
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
    }
}
