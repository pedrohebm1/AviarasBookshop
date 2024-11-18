namespace AviarasBookshop.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public ICollection<Autor>? Autores { get; set; }

        public ICollection<Livro>? Livros { get; set; }
    }
}
