namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZmianaTelefonu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klients", "Telefon", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klients", "Telefon", c => c.String());
        }
    }
}
