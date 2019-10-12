using MailSender.lib.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entities
{
    public class SchedulerTask : BaseEntity
    {
        public DateTime Time { get; set; }
        [Required]
        public Server Server { get; set; }
        [Required]
        public Sender Sender { get; set; }
        [Required]
        public RecipientsList Recipients { get; set; }
        [Required]
        public Email Email { get; set; }
    }
}
