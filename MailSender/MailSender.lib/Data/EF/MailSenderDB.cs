using MailSender.lib.Entities;
using System.Data.Entity;

namespace MailSender.lib.Data.EF
{
    public class MailSenderDB : DbContext
    {
        #region Entities

        public DbSet<Email> Emails { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<RecipientsList> RecipientsLists { get; set; }
        public DbSet<SchedulerTask> SchedulerTasks { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Server> Servers { get; set; }

        #endregion

        public MailSenderDB() : this("name=MailSenderDB") { }

        public MailSenderDB(string Connection) : base(Connection) { }

    }
}
