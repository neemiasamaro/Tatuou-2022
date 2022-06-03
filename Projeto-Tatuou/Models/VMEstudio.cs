using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CadastroEstudio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nome do Estúdio")]
        public string NomeEstudio { get; set; }

        [Required]
        [MaxLength(9)]
        public string Cep { get; set; }

        [MaxLength(200)]
        [Display(Name="Rua")]
        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Whatsapp { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Twitter { get; set; }

        public string Linkedin { get; set; }

        [Required]
        [StringLength(1000)]
        public string Bio { get; set; }

        [Required]
        public string Cnpj { get; set; }

        public string Foto { get; set; }

        public string Imagens { get; set; }

        public ICollection<Portfolio> Portfolio { get; set; }
    }
}