namespace CodeFirst_LaptrinhWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        SanPhamID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.SANPHAMCACANHs", t => t.SanPhamID, cascadeDelete: true)
                .Index(t => t.SanPhamID);
            
            AlterColumn("dbo.SANPHAMCACANHs", "TenSanPham", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "SanPhamID", "dbo.SANPHAMCACANHs");
            DropIndex("dbo.Carts", new[] { "SanPhamID" });
            AlterColumn("dbo.SANPHAMCACANHs", "TenSanPham", c => c.String());
            DropTable("dbo.Carts");
        }
    }
}
