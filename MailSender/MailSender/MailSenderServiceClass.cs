using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Security;

namespace MailSender
{
    class MailSenderServiceClass
    {
        public MailSenderServiceClass()
        {

        }

        /// <summary>
        /// Send email message by smtp without html
        /// </summary>
        /// <param name="sender">Sender email</param>
        /// <param name="password">Sender password</param>
        /// <param name="smtpUrl">smtp server url</param>
        /// <param name="smtpPort">smtp server port</param>
        /// <param name="title">Email title</param>
        /// <param name="body">Email main part</param>
        /// <param name="destination">Giver email</param>
        /// <returns>false if message don't send</returns>
        public bool SmtpSendMessage(string sender, SecureString password,
            string smtpUrl, int smtpPort,
            string title, string body,
            string destination)
        {
            var listStrEmails = new List<string>();
            listStrEmails.Add(destination);

            var tries = SmtpSendMessages(sender, password,
                smtpUrl, smtpPort, title, body, listStrEmails);
            if (tries.Count == 1)
                return tries[0];
            throw new ArgumentException("Destination not added or create a lot of emails.");
        }

        /// <summary>
        /// Send email message by smtp without html
        /// </summary>
        /// <param name="sender">Sender email</param>
        /// <param name="password">Sender password</param>
        /// <param name="smtpUrl">smtp server url</param>
        /// <param name="smtpPort">smtp server port</param>
        /// <param name="title">Email title</param>
        /// <param name="body">Email main part</param>
        /// <param name="listStrEmails">Giver emails list</param>
        /// <returns>list of try sending results, false if message don't send</returns>
        public List<bool> SmtpSendMessages(string sender, SecureString password, 
            string smtpUrl, int smtpPort,
            string title, string body, 
            List<string> listStrEmails)
        {
            List<bool> tries = new List<bool>();

            foreach (var mail in listStrEmails)
            {
                using (var mm = new MailMessage(sender, mail))
                {
                    mm.Subject = title;    // Email title / Тема
                    mm.Body = body;        // Email body / Текст
                    mm.IsBodyHtml = false; // false - body hasnt html

                    using (var sc = new SmtpClient(smtpUrl, smtpPort))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(sender, password);
                        try
                        {
                            sc.Send(mm);
                            tries.Add(true);
                        }
                        catch (Exception ex)
                        {
                            tries.Add(false);
                            throw; // Временное отслеживание
                        }
                    } // smtp
                } // mm
            }
            return tries;
        }
    }
}
