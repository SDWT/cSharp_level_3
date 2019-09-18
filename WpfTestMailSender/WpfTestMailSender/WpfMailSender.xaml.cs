using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Net.Mail;


namespace WpfTestMailSender
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
            List<string> listStrMails = new List<string>();
            listStrMails.Add(tbxSender.Text);

            string strSender = tbxSender.Text;
            string strPassword = passwordBox.Password;

            foreach (var mail in listStrMails)
            {
                using (var mm = new MailMessage(strSender, mail))
                {
                    mm.Subject = "Hello from C#";    // Email title / Тема
                    mm.Body = "Hello world! hahaha"; // Email body / Текст
                    mm.IsBodyHtml = false;           // false - body hasnt html
            
                    using (var sc = new SmtpClient("smtp.mail.ru", 25))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(strSender, strPassword);
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
