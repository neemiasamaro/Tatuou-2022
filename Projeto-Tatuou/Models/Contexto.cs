using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao")
        { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Estilos> Estilos { get; set; }
        public DbSet<Estudio> Estudio { get; set; }
        public DbSet<EstudioEstilo> EstudioEstilo { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }

        //protected override void OnModelCreating(DbModelBuilder mb)
        //{
        //    var usu = mb.Entity<Usuario>();
        //    usu.ToTable("usu_usuario");
        //    usu.Property(x => x.Id).HasColumnName("usu_codigo");
        //    usu.Property(x => x.Nome).HasColumnName("usu_nome");
        //    usu.Property(x => x.Email).HasColumnName("usu_email");
        //    usu.Property(x => x.Cpf).HasColumnName("usu_cpf");
        //    usu.Property(x => x.Senha).HasColumnName("usu_senha");
        //    usu.Property(x => x.PerfilId).HasColumnName("per_codigo");

        //    var per = mb.Entity<Perfil>();
        //    per.ToTable("per_perfil");
        //    per.Property(x => x.Id).HasColumnName("per_codigo");
        //    per.Property(x => x.Descricao).HasColumnName("per_descricao");

        //    var upe = mb.Entity<UsuarioPerfil>();
        //    upe.ToTable("upe_usuarioperfil");
        //    upe.Property(x => x.Id).HasColumnName("upe_codigo");
        //    upe.Property(x => x.PerfilId).HasColumnName("per_codigo");
        //    upe.Property(x => x.UsuarioId).HasColumnName("usu_codigo");


        //}
    }
}