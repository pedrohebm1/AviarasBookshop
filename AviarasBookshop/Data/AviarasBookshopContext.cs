using AviarasBookshop.Models;
using Microsoft.EntityFrameworkCore;

namespace AviarasBookshop.Data
{
    public class AviarasBookshopContext : DbContext
    {
        public AviarasBookshopContext(DbContextOptions<AviarasBookshopContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para relacionamento N:M entre Autor e Livro
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(j => j.AutorId);

            // Outras configurações para relacionamento N:M entre Cliente e Livro, Pedido e Livro
            modelBuilder.Entity<Cliente>()
                .HasMany(l => l.Livros)
                .WithMany(c => c.Clientes)
                .UsingEntity(ntable => ntable.ToTable("ClienteLivro"));

            // modelBuilder.Entity<Pedido>()
            //.HasMany(l => l.Livros)
            //.WithMany(p => p.Pedidos)
            //.UsingEntity(ntable => ntable.ToTable("PedidoLivro"));

            modelBuilder.Entity<Pedido>()
                .HasOne(r => r.Livro)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(r => r.LivroId);

            modelBuilder.Entity<Pedido>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(r => r.ClienteId);
        }
    }
}
