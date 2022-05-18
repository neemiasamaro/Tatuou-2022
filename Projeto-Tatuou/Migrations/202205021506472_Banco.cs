namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estudios", "Bairro", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Estudios", "Cep", c => c.String(nullable: false, maxLength: 9, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estudios", "Cep", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
            DropColumn("dbo.Estudios", "Bairro");
        }
    }
}
