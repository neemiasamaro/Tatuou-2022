using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Estilos
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public Boolean Ativos { get; set; }

        public virtual ICollection<EstudioEstilo> EstudioEstilo { get; set; }
        [MaxLength(100)]
        public string Foto { get; set; }

    }
}