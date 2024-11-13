namespace AviarasBookshop.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }

        public int AutorId { get; set; }

        public Autor? Autor { get; set; }

        // Many-to-many relationship with Autor

        public ICollection<Cliente>? Clientes { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
