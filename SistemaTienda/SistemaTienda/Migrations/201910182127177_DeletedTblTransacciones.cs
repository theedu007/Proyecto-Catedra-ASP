namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedTblTransacciones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblTransacciones", "id_cxc", "dbo.tblCxC");
            DropForeignKey("dbo.tblTransacciones", "id_cxp", "dbo.tblCxP");
            DropIndex("dbo.tblTransacciones", new[] { "id_cxc" });
            DropIndex("dbo.tblTransacciones", new[] { "id_cxp" });
            DropTable("dbo.tblTransacciones");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblTransacciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        id_cxc = c.Int(),
                        id_cxp = c.Int(),
                        fecha = c.DateTime(),
                        abonado = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.tblTransacciones", "id_cxp");
            CreateIndex("dbo.tblTransacciones", "id_cxc");
            AddForeignKey("dbo.tblTransacciones", "id_cxp", "dbo.tblCxP", "Id");
            AddForeignKey("dbo.tblTransacciones", "id_cxc", "dbo.tblCxC", "Id");
        }
    }
}
