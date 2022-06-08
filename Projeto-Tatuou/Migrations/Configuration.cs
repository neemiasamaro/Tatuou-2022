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
                new Models.Estilos { Id = 1, Nome = "WOOD CARVING", Descricao = "Um tipo específico de ilusão de ótica, a tatuagem é um desenho feito em uma madeira.", Status = true, Foto= "woodcarving.jpg" },
                new Models.Estilos { Id = 2, Nome = "OLD SCHOOL", Descricao = "São caracterizadas pelo traço preto grosso e cores vermelha, amarela, verde e azul chapadas com pigmentação intensa. Os desenhos normalmente lembram o mundo dos marinheiros.", Status = true, Foto = "oldschool.jpg" },
                new Models.Estilos { Id = 3, Nome = "NEW SCHOOL", Descricao = "As cores vibrantes são a principal característica das tatuagens new school. Porém, diferente das old school, elas possuem uma maior variedade de tons e traços.", Status = true, Foto = "newschool.jpg" },
                new Models.Estilos { Id = 4, Nome = "BOLD LINE", Descricao = "As bold lines são uma espécie de mistura entre as tatuagens old school e as new school, com inspiração em desenhos animados e personagens de quadrinhos.", Status = true, Foto = "boldline.jpg" },
                new Models.Estilos { Id = 5, Nome = "NEO TRADICIONAL", Descricao = "As neo tradicionais têm influência dos desenhos das tatuagens old school, com a infinidade do uso de cores do estilo new school. É um dos estilos de tatuagem mais novos no ramo, que mescla entre contornos finos e grossos, e transição de cores.", Status = true, Foto = "neotradicional.jpg" },
                new Models.Estilos { Id = 6, Nome = "TRIBAL", Descricao = "Foi um dos estilos de tatuagem que mais fez sucesso nos anos 90, principalmente entre surfistas e afins.", Status = true, Foto = "tribal.jpg" },
                new Models.Estilos { Id = 7, Nome = "ORIENTAL", Descricao = "Cores fortes e elementos da cultura japonesa são a característica essencial nesse estilo de tatuagem.", Status = true, Foto = "oriental.jpg" },
                new Models.Estilos { Id = 8, Nome = "PONTILHISMO", Descricao = "São desenhos que pouco usam traços e garantem as delimitações das artes ou o processo de sombreamento todo feito com micropontos um a um na sua pele.", Status = true, Foto = "pontilhismo.jpg" },
                new Models.Estilos { Id = 9, Nome = "GEOMÉTRICO", Descricao = "As geométricas, além do uso de pontilhismo, possuem obviamente formas geométricas em diversos desenhos e formatos, com presença forte de ilusões tridimensionais e psicodélicas.", Status = true, Foto = "geometrico.jpg" },
                new Models.Estilos { Id = 10, Nome = "AQUARELA", Descricao = "Esse é um estilo bem atual, com influência das pinturas em tinta aquarela, marcadas pelas cores azul, rosa e roxo ultrapassando as linhas do desenho.", Status = true, Foto = "aquarela.jpg" },
                new Models.Estilos { Id = 11, Nome = "BLACKWORK", Descricao = "Uma das últimas tendências no mundo da tattoo, as tatuagens em blackwork possuem traços específicos, uso de pontilhismo, além de diversas técnicas de traços e preenchimento sempre com presença notável da tinta preta.", Status = true, Foto = "blackwork.jpg" },
                new Models.Estilos { Id = 12, Nome = "LETTERING", Descricao = "Com grande influência da arte do graffiti, letterings são tatuagens marcadas pela ornamentação de escritas, criação de fontes e letras em sua pele.", Status = true, Foto = "lettering.jpg" });
            context.Estudio.AddOrUpdate(
                p => p.nomeEstudio,
                new Models.Estudio { Id = 1, nomeEstudio = "Estúdio Teste", Cep = "12518250", Cidade = "Guaratinguetá", Estado= "SP",  Logradouro = "Avenida Paulo Geraldo Pinto", Bairro = "Jardim Esperança", Cnpj = "11111111111111", Disponivel = true, Numero = "165", UsuarioId = 2, Foto= "Uploads/user.png", Bio ="Olá, me chamo Teste e tenho 28 anos, sou tatuador a 5 anos e aqui irei falar um pouco sobre mim.", Whatsapp= "https://wa.me/12996344685", Facebook= "https://www.facebook.com/", Instagram= "https://www.instagram.com/", Linkedin= "https://www.linkedin.com/", Twitter= "https://twitter.com/"},
                new Models.Estudio { Id = 2, nomeEstudio = "Estúdio Teste 2", Cep = "12518250", Cidade = "Guaratinguetá", Estado = "SP", Logradouro = "Avenida Paulo Geraldo Pinto", Bairro = "Jardim Esperança", Cnpj = "11111111111111", Disponivel = true, Numero = "165", UsuarioId = 2, Foto = "Uploads/user.png", Bio = "Olá, me chamo Teste e tenho 28 anos, sou tatuador a 5 anos e aqui irei falar um pouco sobre mim.", Whatsapp = "https://wa.me/12996344685", Facebook = "https://www.facebook.com/", Instagram = "https://www.instagram.com/", Linkedin = "https://www.linkedin.com/", Twitter = "https://twitter.com/" },
                new Models.Estudio { Id = 3, nomeEstudio = "Estúdio Teste 3", Cep = "12518250", Cidade = "Guaratinguetá", Estado = "SP", Logradouro = "Avenida Paulo Geraldo Pinto", Bairro = "Jardim Esperança", Cnpj = "11111111111111", Disponivel = true, Numero = "165", UsuarioId = 2, Foto = "Uploads/user.png", Bio = "Olá, me chamo Teste e tenho 28 anos, sou tatuador a 5 anos e aqui irei falar um pouco sobre mim.", Whatsapp = "https://wa.me/12996344685", Facebook = "https://www.facebook.com/", Instagram = "https://www.instagram.com/", Linkedin = "https://www.linkedin.com/", Twitter = "https://twitter.com/" });
        }
    }
}
