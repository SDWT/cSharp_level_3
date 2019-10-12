namespace MailSender.ConsoleTest.HomeWork7.DBwork.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailSender.ConsoleTest.HomeWork7.DBwork.EF.HW7DBworkDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"HomeWork7\DBwork\EF\Migrations";
        }

        protected override void Seed(MailSender.ConsoleTest.HomeWork7.DBwork.EF.HW7DBworkDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
