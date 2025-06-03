namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Klients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false),
                        Nazwisko = c.String(nullable: false),
                        Email = c.String(),
                        Telefon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zwierzes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false),
                        Gatunek = c.String(nullable: false),
                        Rasa = c.String(nullable: false),
                        DataUrodzenia = c.DateTime(nullable: false),
                        KlientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klients", t => t.KlientId, cascadeDelete: true)
                .Index(t => t.KlientId);
            
            CreateTable(
                "dbo.Wizytas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Opis = c.String(),
                        ZwierzeId = c.Int(nullable: false),
                        WeterynarzId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weterynarzs", t => t.WeterynarzId, cascadeDelete: true)
                .ForeignKey("dbo.Zwierzes", t => t.ZwierzeId, cascadeDelete: true)
                .Index(t => t.ZwierzeId)
                .Index(t => t.WeterynarzId);
            
            CreateTable(
                "dbo.Leczenies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opis = c.String(),
                        Lek = c.String(),
                        Koszt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WizytaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wizytas", t => t.WizytaId, cascadeDelete: true)
                .Index(t => t.WizytaId);
            
            CreateTable(
                "dbo.Weterynarzs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false),
                        Nazwisko = c.String(nullable: false),
                        Specjalizacja = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wizytas", "ZwierzeId", "dbo.Zwierzes");
            DropForeignKey("dbo.Wizytas", "WeterynarzId", "dbo.Weterynarzs");
            DropForeignKey("dbo.Leczenies", "WizytaId", "dbo.Wizytas");
            DropForeignKey("dbo.Zwierzes", "KlientId", "dbo.Klients");
            DropIndex("dbo.Leczenies", new[] { "WizytaId" });
            DropIndex("dbo.Wizytas", new[] { "WeterynarzId" });
            DropIndex("dbo.Wizytas", new[] { "ZwierzeId" });
            DropIndex("dbo.Zwierzes", new[] { "KlientId" });
            DropTable("dbo.Weterynarzs");
            DropTable("dbo.Leczenies");
            DropTable("dbo.Wizytas");
            DropTable("dbo.Zwierzes");
            DropTable("dbo.Klients");
        }
    }
}
