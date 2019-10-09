using MailSender.lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MailSender.lib.Services
{
    /// <summary>Объект для отправки почты по средствам smtp-протокола</summary>
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

                using (var message = new MailMessage())
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

        public void SendParallel(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
                ThreadPool.QueueUserWorkItem(o => Send(Message, From, recipient));
        }

        public async Task SendAsync(Email Message, Sender From, Recipient To)
        {
            using (var client = new SmtpClient(_Host, _Port) { EnableSsl = _UseSSL })
            {
                client.Credentials = new NetworkCredential
                {
                    UserName = _Login,
                    Password = _Password
                };

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(From.Address, From.Name);
                    message.To.Add(new MailAddress(To.Address, To.Name));
                    message.Subject = Message.Subject;
                    message.Body = Message.Body;

                    //await Task.Run(() => client.Send(message));
                    await client.SendMailAsync(message).ConfigureAwait(false);
                }
            }
        }

        public async Task SendParallelAsync(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            //await Task
            //   .WhenAll(To.Select(recipient => SendAsync(Message, From, recipient)))
            //   .ConfigureAwait(false);

            var send_tasks = new List<Task>();
            foreach (var recipient in To)
                send_tasks.Add(SendAsync(Message, From, recipient));

            await Task.WhenAll(send_tasks).ConfigureAwait(false);
        }

        public async Task SendAsync(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
                await SendAsync(Message, From, recipient).ConfigureAwait(false);
        }
    }
}
