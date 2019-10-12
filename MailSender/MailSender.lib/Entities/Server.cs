using MailSender.lib.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entities
{
    public class Server : NamedEntity
    {
        [Required]
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public bool UseSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string FullAddress { get => $"{Host}:{Port}"; }
    }
}
