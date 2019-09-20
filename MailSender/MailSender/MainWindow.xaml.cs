using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            List<string> listStrMails = new List<string>();
            listStrMails.Add(tbxSender.Text);

            string strSender = tbxSender.Text;
            var password = passwordBox.SecurePassword;

            foreach (var mail in listStrMails)
            {
                using (var mm = new MailMessage(strSender, mail))
                {
                    mm.Subject = txtBxEmailTitle.Text;    // Email title / Тема
                    mm.Body = txtBxEmailBody.Text; // Email body / Текст
                    mm.IsBodyHtml = false;           // false - body hasnt html

                    using (var sc = new SmtpClient("smtp.mail.ru", 25))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(strSender, password);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при отправке {ex.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    } // smtp
                } // mm
            }
            //MessageBox.Show("Работа завершена.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            SendEndWindow sew = new SendEndWindow();
            sew.ShowDialog();
        }
    }
}
