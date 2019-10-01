using System.Collections.Generic;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class RecipientsList : NamedEntity
    {
        public ICollection<Recipient> Recipients { get; set; }
    }
}
