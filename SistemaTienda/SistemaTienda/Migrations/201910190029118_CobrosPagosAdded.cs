namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CobrosPagosAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCobros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        id_cxc = c.Int(),
                        fecha = c.DateTime(),
                        abono = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCxC", t => t.id_cxc)
                .Index(t => t.id_cxc);
            
            CreateTable(
                "dbo.tblPagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        id_cxp = c.Int(),
                        fecha = c.DateTime(),
                        abono = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCxP", t => t.id_cxp)
                .Index(t => t.id_cxp);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP");
            DropForeignKey("dbo.tblCobros", "id_cxc", "dbo.tblCxC");
            DropIndex("dbo.tblPagos", new[] { "id_cxp" });
            DropIndex("dbo.tblCobros", new[] { "id_cxc" });
            DropTable("dbo.tblPagos");
            DropTable("dbo.tblCobros");
        }
    }
}
