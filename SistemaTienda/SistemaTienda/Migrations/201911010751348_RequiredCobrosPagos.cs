namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredCobrosPagos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP");
            DropForeignKey("dbo.tblCobros", "id_cxc", "dbo.tblCxC");
            DropIndex("dbo.tblPagos", new[] { "id_cxp" });
            DropIndex("dbo.tblCobros", new[] { "id_cxc" });
            AlterColumn("dbo.tblPagos", "id_cxp", c => c.Int(nullable: false));
            AlterColumn("dbo.tblPagos", "fecha", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tblPagos", "abono", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tblCobros", "id_cxc", c => c.Int(nullable: false));
            AlterColumn("dbo.tblCobros", "fecha", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tblCobros", "abono", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.tblPagos", "id_cxp");
            CreateIndex("dbo.tblCobros", "id_cxc");
            AddForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblCobros", "id_cxc", "dbo.tblCxC", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCobros", "id_cxc", "dbo.tblCxC");
            DropForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP");
            DropIndex("dbo.tblCobros", new[] { "id_cxc" });
            DropIndex("dbo.tblPagos", new[] { "id_cxp" });
            AlterColumn("dbo.tblCobros", "abono", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblCobros", "fecha", c => c.DateTime());
            AlterColumn("dbo.tblCobros", "id_cxc", c => c.Int());
            AlterColumn("dbo.tblPagos", "abono", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tblPagos", "fecha", c => c.DateTime());
            AlterColumn("dbo.tblPagos", "id_cxp", c => c.Int());
            CreateIndex("dbo.tblCobros", "id_cxc");
            CreateIndex("dbo.tblPagos", "id_cxp");
            AddForeignKey("dbo.tblCobros", "id_cxc", "dbo.tblCxC", "Id");
            AddForeignKey("dbo.tblPagos", "id_cxp", "dbo.tblCxP", "Id");
        }
    }
}
