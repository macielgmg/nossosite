using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteLogin.Models
{
    [Table("arquivos")] // Nome da tabela no banco de dados (minúsculo)
    public class Arquivo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nomeoriginal")]
        public string NomeOriginal { get; set; }

        [Required]
        [Column("contenttype")]
        public string ContentType { get; set; }

        [Required]
        [Column("dados")]
        public byte[] Dados { get; set; }

        [Column("dataenvio")]
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;

        [Column("usuarioid")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
