using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteLogin.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("creditos")]
        public int Creditos { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("usuario")]
        public string? UsuarioLogin { get; set; }

        [Column("senha")]
        public string? Senha { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
