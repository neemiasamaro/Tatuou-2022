using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        public string Senha { get; set; }

        public int PerfilId { get; set; }

        public virtual Perfil Perfil { get; set; }

        public string Hash { get; set; }

        [MaxLength(14)]
        public string Cpf { get; set; }

        public Boolean Status { get; set;}
    }
}