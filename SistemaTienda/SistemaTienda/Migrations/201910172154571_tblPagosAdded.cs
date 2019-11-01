namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblPagosAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblPagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        id_cxc = c.Int(),
                        id_cxp = c.Int(),
                        fecha = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCxC", t => t.id_cxc)
                .ForeignKey("dbo.tblCxP", t => t.id_cxp)
                .Index(t => t.id_cxc)
                .Index(t => t.id_cxp);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP");
            DropForeignKey("dbo.tblPagos", "id_cxc", "dbo.tblCxC");
            DropIndex("dbo.tblPagos", new[] { "id_cxp" });
            DropIndex("dbo.tblPagos", new[] { "id_cxc" });
            DropTable("dbo.tblPagos");
        }
    }
}
