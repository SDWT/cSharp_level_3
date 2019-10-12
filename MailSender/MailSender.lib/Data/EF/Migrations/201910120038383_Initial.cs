namespace MailSender.lib.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        RecipientsList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecipientsLists", t => t.RecipientsList_Id)
                .Index(t => t.RecipientsList_Id);
            
            CreateTable(
                "dbo.RecipientsLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchedulerTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Email_Id = c.Int(nullable: false),
                        Recipients_Id = c.Int(nullable: false),
                        Sender_Id = c.Int(nullable: false),
                        Server_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Emails", t => t.Email_Id, cascadeDelete: true)
                .ForeignKey("dbo.RecipientsLists", t => t.Recipients_Id, cascadeDelete: true)
                .ForeignKey("dbo.Senders", t => t.Sender_Id, cascadeDelete: true)
                .ForeignKey("dbo.Servers", t => t.Server_Id, cascadeDelete: true)
                .Index(t => t.Email_Id)
                .Index(t => t.Recipients_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.Server_Id);
            
            CreateTable(
                "dbo.Senders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Host = c.String(nullable: false),
                        Port = c.Int(nullable: false),
                        UseSSL = c.Boolean(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Description = c.String(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerTasks", "Server_Id", "dbo.Servers");
            DropForeignKey("dbo.SchedulerTasks", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.SchedulerTasks", "Recipients_Id", "dbo.RecipientsLists");
            DropForeignKey("dbo.SchedulerTasks", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.Recipients", "RecipientsList_Id", "dbo.RecipientsLists");
            DropIndex("dbo.SchedulerTasks", new[] { "Server_Id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Sender_Id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Recipients_Id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Email_Id" });
            DropIndex("dbo.Recipients", new[] { "RecipientsList_Id" });
            DropTable("dbo.Servers");
            DropTable("dbo.Senders");
            DropTable("dbo.SchedulerTasks");
            DropTable("dbo.RecipientsLists");
            DropTable("dbo.Recipients");
            DropTable("dbo.Emails");
        }
    }
}
