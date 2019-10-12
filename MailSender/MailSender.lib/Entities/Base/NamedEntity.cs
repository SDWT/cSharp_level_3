using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entities.Base
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }
    }
}
