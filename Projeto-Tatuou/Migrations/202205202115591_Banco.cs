namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estilos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Descricao = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Ativos = c.Boolean(nullable: false),
                        Foto = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstudioEstiloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstudioId = c.Int(nullable: false),
                        EstilosId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estilos", t => t.EstilosId, cascadeDelete: true)
                .ForeignKey("dbo.Estudios", t => t.EstudioId, cascadeDelete: true)
                .Index(t => t.EstudioId)
                .Index(t => t.EstilosId);
            
            CreateTable(
                "dbo.Estudios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nomeEstudio = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Cep = c.String(nullable: false, maxLength: 9, storeType: "nvarchar"),
                        UsuarioId = c.Int(nullable: false),
                        Cnpj = c.String(nullable: false, maxLength: 18, storeType: "nvarchar"),
                        Bairro = c.String(unicode: false),
                        Logradouro = c.String(unicode: false),
                        Complemento = c.String(unicode: false),
                        Numero = c.String(nullable: false, unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                        Whatsapp = c.String(unicode: false),
                        Facebook = c.String(unicode: false),
                        Instagram = c.String(unicode: false),
                        Site = c.String(unicode: false),
                        Disponivel = c.Boolean(nullable: false),
                        Foto = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Senha = c.String(unicode: false),
                        PerfilId = c.Int(nullable: false),
                        Hash = c.String(unicode: false),
                        Cpf = c.String(maxLength: 14, storeType: "nvarchar"),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfils", t => t.PerfilId, cascadeDelete: true)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstudioId = c.Int(nullable: false),
                        Imagem = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estudios", t => t.EstudioId, cascadeDelete: true)
                .Index(t => t.EstudioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Portfolios", "EstudioId", "dbo.Estudios");
            DropForeignKey("dbo.Estudios", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "PerfilId", "dbo.Perfils");
            DropForeignKey("dbo.EstudioEstiloes", "EstudioId", "dbo.Estudios");
            DropForeignKey("dbo.EstudioEstiloes", "EstilosId", "dbo.Estilos");
            DropIndex("dbo.Portfolios", new[] { "EstudioId" });
            DropIndex("dbo.Usuarios", new[] { "PerfilId" });
            DropIndex("dbo.Estudios", new[] { "UsuarioId" });
            DropIndex("dbo.EstudioEstiloes", new[] { "EstilosId" });
            DropIndex("dbo.EstudioEstiloes", new[] { "EstudioId" });
            DropTable("dbo.Portfolios");
            DropTable("dbo.Perfils");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Estudios");
            DropTable("dbo.EstudioEstiloes");
            DropTable("dbo.Estilos");
        }
    }
}
