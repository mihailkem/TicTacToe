namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlayertoPlayerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "PlayerName", c => c.String(nullable: false));
            DropColumn("dbo.Games", "Player");            
        }
        
        public override void Down()
        {           
            AddColumn("dbo.Games", "Player", c => c.String(nullable: false));
            DropColumn("dbo.Games", "PlayerName");
        }
    }
}
