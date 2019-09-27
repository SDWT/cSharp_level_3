using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.IO;

using MailSender.Model;
using System.Windows.Documents;
using System.Linq;
using System.Text;
using MailSender.lib.Entities;

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
            //string body = txtBxEmailBody.Text;   // Email body / Текст
            string body;

            #region RichTextBox
            // Create Stream for text
            using (var stream = new MemoryStream(100))
            {
                // Get text from RichTextBox
                TextRange range = new TextRange(rchTxtBxEmailBody.Document.ContentStart,
                    rchTxtBxEmailBody.Document.ContentEnd);


                // Save text to stream
                range.Save(stream, DataFormats.Text);

                // Set seek to begin
                stream.Seek(0, SeekOrigin.Begin);

                // Create Reader from stream
                var reader = new StreamReader(stream);

                // Convert stream to string
                body = reader.ReadToEnd();
            }
            body = body.Trim();

            if (body == string.Empty)
            {
                var eW = new ErrorWindow("Письмо не заполнено, переход в редактор писем.");
                eW.ShowDialog();
                MainTabControl.SelectedIndex = 2;
                return;
            }

            #endregion
            
            // every time null
            if (lcSmtpServer.SelectedItem == null)
            {
                var eW = new ErrorWindow("Smtp-server не указан.");
                eW.ShowDialog();
                MainTabControl.SelectedIndex = 0;
                return;
            }
            if (!(lcSmtpServer.SelectedItem is Server smtp))
            {
                var eW = new ErrorWindow("Ошибка в данных, передан не smtp-server.");
                eW.ShowDialog();
                MainTabControl.SelectedIndex = 0;
                return;
            }

            string FullAddress = smtp.FullAddress;
            var arr = FullAddress.Split(':');

            StringBuilder strB = new StringBuilder();

            foreach (var str in arr)
            {
                strB.Append(str);
            }
            string smtpUrl = strB.ToString(0, strB.Length - arr.Last().Length);
            int smtpPort;

            if (!(int.TryParse(arr.Last(), out smtpPort)))
            {
                var eW = new ErrorWindow(string.Format(
                            "Порт smpt сервера {0} указан неверно.", smtpUrl));
                return;
            }
            

            // Времменно отключена отправка для тестирования
            //return;

            try
            {
                var results = SendService.SmtpSendMessages(strSender, password,
                smtpUrl, smtpPort, title, body, listStrMails);


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

        private void GoToPlanningButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }
    }
}
