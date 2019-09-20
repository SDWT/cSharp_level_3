using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;

using MailSender.Model;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        //private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        //{
        //    List<string> listStrMails = new List<string>();
        //    listStrMails.Add(tbxSender.Text);

        //    string strSender = tbxSender.Text;
        //    var password = passwordBox.SecurePassword;

        //    foreach (var mail in listStrMails)
        //    {
        //        using (var mm = new MailMessage(strSender, mail))
        //        {
        //            mm.Subject = txtBxEmailTitle.Text;    // Email title / Тема
        //            mm.Body = txtBxEmailBody.Text; // Email body / Текст
        //            mm.IsBodyHtml = false;           // false - body hasnt html

        //            using (var sc = new SmtpClient(Smtps.MailRu.SmptUrl, Smtps.MailRu.Port))
        //            {
        //                sc.EnableSsl = true;
        //                sc.Credentials = new NetworkCredential(strSender, password);
        //                try
        //                {
        //                    sc.Send(mm);
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show($"Ошибка при отправке {ex.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //                }
        //            } // smtp
        //        } // mm
        //    }
        //    //MessageBox.Show("Работа завершена.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        //    SendEndWindow sew = new SendEndWindow();
        //    sew.ShowDialog();
        //}

        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            var SendService = new MailSenderServiceClass();

            // Givers
            List<string> listStrMails = new List<string>();
            listStrMails.Add(tbxSender.Text);

            // Sender
            string strSender = tbxSender.Text;
            var password = passwordBox.SecurePassword;

            // Email
            string title = txtBxEmailTitle.Text;    // Email title / Тема
            string body = txtBxEmailBody.Text; // Email body / Текст

            var results = SendService.SmtpSendMessages(strSender, password,
                Smtps.MailRu.SmptUrl, Smtps.MailRu.Port, title, body, listStrMails);
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i])
                {
                    MessageBox.Show($"Message to {listStrMails[i]} sended.", 
                        "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Ошибка письмо до {listStrMails[i]} не отправлено.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            SendEndWindow sew = new SendEndWindow();
            sew.ShowDialog();
        }
    }
}
