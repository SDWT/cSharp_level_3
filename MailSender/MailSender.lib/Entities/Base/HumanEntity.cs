namespace MailSender.lib.Entities.Base
{
    public abstract class HumanEntity : NamedEntity
    {
        public virtual string Address { get; set; }
    }
}
