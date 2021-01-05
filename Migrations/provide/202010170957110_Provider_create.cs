namespace Project_With_Angular.Migrations.provide
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Provider_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        DriverName = c.String(),
                        Fee = c.Int(nullable: false),
                        Experiance = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Available = c.Boolean(nullable: false),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(nullable: false, maxLength: 40),
                        Address = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 50),
                        Web = c.String(),
                    })
                .PrimaryKey(t => t.ProviderId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drivers", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Vehicles", "ProviderId", "dbo.Providers");
            DropIndex("dbo.Vehicles", new[] { "ProviderId" });
            DropIndex("dbo.Drivers", new[] { "ProviderId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Providers");
            DropTable("dbo.Drivers");
        }
    }
}
