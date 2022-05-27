namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new
            MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(WebApplication1.Models.Contexto context)
        {
            context.Usuario.AddOrUpdate(
              p => p.Email,
              new Models.Usuario { Id = 1, Nome = "Administrador", Email = "tatuouadmin@gmail.com", Senha = "V8SLtjr9Lz6lYIiywHSnaYNNwbbZ3l9lKJ8sPAomC8YyUeiO+0G4SWvLWhjHw9Z0cNwkeNT/wRuSmRs0ke874A==", Status = true, PerfilId = 1 },
              new Models.Usuario { Id = 2, Nome = "Neemias", Email = "nemisilva10@gmail.com", Senha = "V8SLtjr9Lz6lYIiywHSnaYNNwbbZ3l9lKJ8sPAomC8YyUeiO+0G4SWvLWhjHw9Z0cNwkeNT/wRuSmRs0ke874A==", Status = true, PerfilId = 2 });
            context.Perfil.AddOrUpdate(
                p => p.Descricao,
                new Models.Perfil { Id = 1, Descricao = "Admin" },
                new Models.Perfil { Id = 2, Descricao = "Comum" });
            context.Estilos.AddOrUpdate(
                p => p.Nome,
                new Models.Estilos { Id = 1, Nome = "Tribal", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 2, Nome = "Tribal2", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 3, Nome = "Tribal3", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 4, Nome = "Tribal4", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 5, Nome = "Tribal5", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 6, Nome = "Tribal6", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 7, Nome = "Tribal7", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 8, Nome = "Tribal8", Descricao = "Teste", Status = true },
                new Models.Estilos { Id = 9, Nome = "Tribal9", Descricao = "Teste", Status = true });
            context.Estudio.AddOrUpdate(
                p => p.nomeEstudio,
                new Models.Estudio { Id = 1, nomeEstudio = "Teste", Cep = "12518250", Cidade = "Guaratinguetá", Logradouro = "Avenida Paulo Geraldo Pinto", Bairro = "Jardim Esperança", Cnpj = "11111111111111", Disponivel = true, Numero = "165", UsuarioId = 2 });
        }
    }
}
