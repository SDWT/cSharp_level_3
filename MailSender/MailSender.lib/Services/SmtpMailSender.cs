using MailSender.lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Services
{
    class SmtpMailSender
    {
        private readonly string _Host;
        private readonly int _Port;
        private readonly bool _UseSSL;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Host, int Port, bool UseSSL, string Login, string Password)
        {
            _Host = Host;
            _Port = Port;
            _UseSSL = UseSSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(Email Message, Sender From, Recipient To)
        {
            using (var client =  new SmtpClient(_Host, _Port) { EnableSsl = _UseSSL})
            {
                client.Credentials = new NetworkCredential
                {
                    UserName = _Login,
                    Password = _Password
                };

                using (var message = new MailMessage)
                {
                    message.From = new MailAddress(From.Address, From.Name);
                    message.To.Add(new MailAddress(To.Address, To.Name));
                    message.Subject = Message.Subject;
                    message.Body = Message.Body;

                    client.Send(message);
                }
            }
        }

        public void Send(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
            {
                Send(Message, From, recipient);
            }
        }

    }
}
