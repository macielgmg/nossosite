using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteLogin.Models
{
    public class Arquivo
    {
        public int Id { get; set; }

        [Required]
        public string NomeOriginal { get; set; }

        [Required]
        public string Caminho { get; set; }

        public DateTime DataEnvio { get; set; } = DateTime.Now;

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
