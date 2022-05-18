using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EstudioEstilo
    {
        public int Id { get; set; }
        public int EstudioId { get; set; }
        public int EstilosId { get; set; }
        public virtual Estudio Estudio { get; set; }
        public virtual Estilos Estilo { get; set; }

    }
}