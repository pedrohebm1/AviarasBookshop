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

            
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Autores)
                .WithMany(a => a.Livros)
                .UsingEntity(j => j.ToTable("LivroAutor"));

            modelBuilder.Entity<Cliente>()
                .HasMany(l => l.Livros)
                .WithMany(c => c.Clientes)
                .UsingEntity(ntable => ntable.ToTable("ClienteLivro"));

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Livros)
                .WithMany(l => l.Pedidos)
                .UsingEntity(j => j.ToTable("PedidoLivro"));

            modelBuilder.Entity<Pedido>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(r => r.ClienteId);
        }
    }
}
