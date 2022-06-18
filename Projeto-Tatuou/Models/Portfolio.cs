using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public virtual Estudio Estudio { get; set; }
        [MaxLength(100)]
        public string Imagem { get; set; }

        //[Required]
        //[MaxLength(255)]
        //public string Descricao { get; set; }
    }
}