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

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"^(\(\d{2}\)\s?(\d{4,5})\s?-?\d{4}|\d{4}-\d{4}|\d{8})$", ErrorMessage = "O número de telefone deve estar no formato: (XX) XXXXX-XXXX ou (XX) XXXX-XXXX")]
        public string NumeroTelefone { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }

        public ICollection<Livro>? Livros { get; set; }  
    }
}
