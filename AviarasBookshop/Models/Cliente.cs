using System.ComponentModel.DataAnnotations;

namespace AviarasBookshop.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")] 
        public string Email { get; set; }

        public string NumeroTelefone { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }

        public ICollection<Livro>? Livros { get; set; }  
    }
}
