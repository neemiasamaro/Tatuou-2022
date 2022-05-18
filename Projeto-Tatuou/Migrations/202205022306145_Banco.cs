namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estudios", "Bairro", c => c.String(unicode: false));
            AlterColumn("dbo.Estudios", "Logradouro", c => c.String(unicode: false));
            AlterColumn("dbo.Estudios", "Cidade", c => c.String(unicode: false));
            AlterColumn("dbo.Estudios", "Estado", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudios", "Estado", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Estudios", "Cidade", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Estudios", "Logradouro", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Estudios", "Bairro", c => c.String(nullable: false, unicode: false));
        }
    }
}
