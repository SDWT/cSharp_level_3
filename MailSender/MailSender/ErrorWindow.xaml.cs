using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для SendEndWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {

        public ErrorWindow()
        {
            InitializeComponent();
        }

        public ErrorWindow(string strError) : this()
        {
            txtBlError.Text = $"Error: {strError}";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
