namespace AviarasBookshop.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public decimal PrecoTotal { get; set; }

        public string? LivrosLista { get; set; }

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public int LivroId { get; set; }

        public Livro? Livro { get; set; }

        public ICollection<Livro>? Livros { get; set; }

        
    }
}
