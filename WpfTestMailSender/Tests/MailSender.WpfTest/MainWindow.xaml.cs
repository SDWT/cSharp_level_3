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
using System.Security;

namespace MailSender.WpfTest
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var host = "smtp.mail.ru";
            var port = 25;

            string msg = string.Empty;

            var user_name = UserNameEditor.Text;

            //var password = PasswordEditor.Password;
            var password = PasswordEditor.SecurePassword;

            using (var client = new SmtpClient(host, port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(user_name, password);
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("Sender@mail.ru", "Дима");
                    message.To.Add(new MailAddress("Sender@mail", "Дима"));
                    //message.To.Add(new MailAddress("Sender@mail", "Дима"));
                    message.Subject = "Заголовок письма от " + DateTime.Now;
                    message.Body = msg;
                    message.IsBodyHtml = false;

                    try
                    {
                        client.Send(message);
                        MessageBox.Show("Почта успешна отправлена", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при отправке {ex.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
