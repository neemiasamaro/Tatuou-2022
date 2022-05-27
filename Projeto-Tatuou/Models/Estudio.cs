using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Estudio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nome do Estúdio")]
        public string nomeEstudio { get; set; }

        public virtual ICollection<EstudioEstilo> EstudioEstilos { get; set; }

        [Required]
        [MaxLength(9)]
        public string Cep { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        [MaxLength(18)]
        public string Cnpj { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Whatsapp { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Linkedin { get; set; }

        public string Twitter { get; set; }

        public string Bio { get; set; }

        public Boolean Disponivel { get; set; }

        [MaxLength(100)]
        public string Foto { get; set; }
        //public ICollection<Portfolio> Portfolio { get; set; }

    }
}