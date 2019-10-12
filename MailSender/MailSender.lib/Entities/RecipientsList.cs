using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class RecipientsList : NamedEntity
    {
        [Required]
        public ICollection<Recipient> Recipients { get; set; }
    }
}
