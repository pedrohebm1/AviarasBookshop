using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviarasBookshop.Migrations
{
    /// <inheritdoc />
    public partial class update_pedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Pedidos_PedidoId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Livros_LivroId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_LivroId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Livros_PedidoId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Livros");

            migrationBuilder.CreateTable(
                name: "PedidoLivro",
                columns: table => new
                {
                    LivrosId = table.Column<int>(type: "int", nullable: false),
                    PedidosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoLivro", x => new { x.LivrosId, x.PedidosId });
                    table.ForeignKey(
                        name: "FK_PedidoLivro_Livros_LivrosId",
                        column: x => x.LivrosId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoLivro_Pedidos_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoLivro_PedidosId",
                table: "PedidoLivro",
                column: "PedidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoLivro");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_LivroId",
                table: "Pedidos",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_PedidoId",
                table: "Livros",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Pedidos_PedidoId",
                table: "Livros",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Livros_LivroId",
                table: "Pedidos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
