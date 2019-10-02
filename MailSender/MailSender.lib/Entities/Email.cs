using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class Email : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
