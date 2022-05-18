namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estilos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Ativos = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Estudios",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    nomeEstudio = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Cep = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                    UsuarioId = c.Int(nullable: false),
                    Cnpj = c.String(nullable: false, maxLength: 18, storeType: "nvarchar"),
                    Logradouro = c.String(nullable: false, unicode: false),
                    Complemento = c.String(unicode: false),
                    Numero = c.String(nullable: false, unicode: false),
                    Cidade = c.String(nullable: false, unicode: false),
                    Estado = c.String(nullable: false, unicode: false),
                    Whatsapp = c.String(unicode: false),
                    Facebook = c.String(unicode: false),
                    Instagram = c.String(unicode: false),
                    Site = c.String(unicode: false),
                    Disponivel = c.Boolean(nullable: false),
                    Imagens = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true);


            CreateTable(
                "dbo.EstudioEstiloes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    estudioId = c.Int(nullable: false),
                    estilosId = c.Int(nullable: false),
                    estudioEstilo_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estilos", t => t.estilosId, cascadeDelete: true)
                .ForeignKey("dbo.EstudioEstiloes", t => t.estudioEstilo_Id)
                .ForeignKey("dbo.Estudios", t => t.estudioId, cascadeDelete: true);


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
                .ForeignKey("dbo.Perfils", t => t.PerfilId, cascadeDelete: true);
            
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estudios", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "PerfilId", "dbo.Perfils");
            DropForeignKey("dbo.EstudioEstiloes", "estudioId", "dbo.Estudios");
            DropForeignKey("dbo.EstudioEstiloes", "estudioEstilo_Id", "dbo.EstudioEstiloes");
            DropForeignKey("dbo.EstudioEstiloes", "estilosId", "dbo.Estilos");
            DropIndex("dbo.Usuarios", new[] { "PerfilId" });
            DropIndex("dbo.EstudioEstiloes", new[] { "estudioEstilo_Id" });
            DropIndex("dbo.EstudioEstiloes", new[] { "estilosId" });
            DropIndex("dbo.EstudioEstiloes", new[] { "estudioId" });
            DropIndex("dbo.Estudios", new[] { "UsuarioId" });
            DropTable("dbo.Perfils");
            DropTable("dbo.Usuarios");
            DropTable("dbo.EstudioEstiloes");
            DropTable("dbo.Estudios");
            DropTable("dbo.Estilos");
        }
    }
}
