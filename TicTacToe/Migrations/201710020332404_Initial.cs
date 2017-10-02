namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        f1 = c.String(),
                        f2 = c.String(),
                        f3 = c.String(),
                        f4 = c.String(),
                        f5 = c.String(),
                        f6 = c.String(),
                        f7 = c.String(),
                        f8 = c.String(),
                        f9 = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 30),
                        PlayerTeamId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                        WhoWin = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fields", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "LevelId", "dbo.Levels");
            DropIndex("dbo.Games", new[] { "LevelId" });
            DropIndex("dbo.Fields", new[] { "GameId" });
            DropTable("dbo.Levels");
            DropTable("dbo.Games");
            DropTable("dbo.Fields");
        }
    }
}
