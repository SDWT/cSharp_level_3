using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.IO;

using MailSender.Model;
using System.Windows.Documents;

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

            // Времменно отключена отправка для тестирования
            return;

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
