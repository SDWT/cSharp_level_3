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
            string title = txtBxEmailTitle.Text; // Email title / Тема
            string body = txtBxEmailBody.Text;   // Email body / Текст
            //string body = rchTxtBxEmailBody.Document;   // Email body / Текст

            try
            {
                var results = SendService.SmtpSendMessages(strSender, password,
                Smtps.MailRu.SmptUrl, Smtps.MailRu.Port, title, body, listStrMails);


                for (int i = 0; i < results.Count; i++)
                {
                    if (!results[i])
                    {
                        
                        var eW = new ErrorWindow(string.Format(
                            "Ошибка письмо до {0} не отправлено.", listStrMails[i]));
                        eW.ShowDialog();
                    }
                }

                SendEndWindow sew = new SendEndWindow();
                sew.ShowDialog();
            }
            catch (ArgumentException ae)
            {
                string errMsg = string.Empty;
                if (ae.ParamName == "from")
                    errMsg = "Email адрес отправителя не указан.";
                else
                    throw;

                var eW = new ErrorWindow(errMsg);
                eW.ShowDialog();
            }
            catch (FormatException fe)
            {
                var eW = new ErrorWindow(fe.Message);
                eW.ShowDialog();
            }
        }
    }
}
