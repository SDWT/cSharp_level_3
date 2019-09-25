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

namespace MailSender.WPFTest.Controls
{
    /// <summary>
    /// Логика взаимодействия для ToolBarSelector.xaml
    /// </summary>
    public partial class ToolBarSelector : UserControl
    {
        public event EventHandler CreateButtonClick;
        public event EventHandler EditButtonClick;
        public event EventHandler DeleteButtonClick;

        public System.Collections.IEnumerable ItemsSource
        { 
            get  => tlBrCmbBx.ItemsSource;
            set => tlBrCmbBx.ItemsSource = value;
        }

        public ToolBarSelector()
        {
            InitializeComponent();
        }

        private void ToolBar_Click(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is Button button)) return;

            switch (button.Name)
            {
                case "tlBrCreateButton":
                    CreateButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                case "tlBrEditButton":
                    EditButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                case "tlBrDeleteButton":
                    DeleteButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }
    }
}
