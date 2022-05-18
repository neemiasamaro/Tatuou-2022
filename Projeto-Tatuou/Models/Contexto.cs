using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString:"StringConexao")
        { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Estilos> Estilos { get; set; }
        public DbSet<Estudio> Estudio { get; set; }
        public DbSet<EstudioEstilo> EstudioEstilo { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; } 
    }
}