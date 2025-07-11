using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteLogin.Models
{
    [Table("arquivos")]
    public class Arquivo
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Required]
        [Column("nome_original")]
        public string NomeOriginal { get; set; }

        [Required]
        [Column("caminho")]
        public string Caminho { get; set; }

        [Column("data_envio")]
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;

        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
