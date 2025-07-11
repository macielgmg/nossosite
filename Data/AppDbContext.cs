using Microsoft.EntityFrameworkCore;
using MeuSiteLogin.Models;

namespace MeuSiteLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Arquivo> Arquivos { get; set; }


        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.ToTable("arquivos");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NomeOriginal).HasColumnName("nomeoriginal");
                entity.Property(e => e.ContentType).HasColumnName("contenttype");
                entity.Property(e => e.Dados).HasColumnName("dados");
                entity.Property(e => e.DataEnvio).HasColumnName("dataenvio");
                entity.Property(e => e.UsuarioId).HasColumnName("usuarioid");
            });

            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().Property(u => u.UsuarioLogin).HasColumnName("usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Senha).HasColumnName("senha");
        }

    }

}
