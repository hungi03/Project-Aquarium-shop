namespace CodeFirst_LaptrinhWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LOAICAs",
                c => new
                    {
                        LoaiCaID = c.Int(nullable: false, identity: true),
                        TenLoaiCa = c.String(),
                    })
                .PrimaryKey(t => t.LoaiCaID);
            
            CreateTable(
                "dbo.SANPHAMCACANHs",
                c => new
                    {
                        SanPhamID = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(),
                        Gia = c.Single(nullable: false),
                        LoaiCaID = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        MoTa1 = c.String(),
                        MoTa2 = c.String(),
                        MoTa3 = c.String(),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.SanPhamID)
                .ForeignKey("dbo.LOAICAs", t => t.LoaiCaID, cascadeDelete: true)
                .Index(t => t.LoaiCaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SANPHAMCACANHs", "LoaiCaID", "dbo.LOAICAs");
            DropIndex("dbo.SANPHAMCACANHs", new[] { "LoaiCaID" });
            DropTable("dbo.SANPHAMCACANHs");
            DropTable("dbo.LOAICAs");
        }
    }
}
