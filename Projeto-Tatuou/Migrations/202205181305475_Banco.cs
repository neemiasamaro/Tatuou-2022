namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Estilos", "Foto", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AddColumn("dbo.Estudios", "Foto", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Portfolios", "EstudioId", "dbo.Estudios");
            DropIndex("dbo.Portfolios", new[] { "EstudioId" });
            DropColumn("dbo.Estudios", "Foto");
            DropColumn("dbo.Estilos", "Foto");
            DropTable("dbo.Portfolios");
        }
    }
}
