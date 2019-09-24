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
    /// Логика взаимодействия для TabItemsSwither.xaml
    /// </summary>
    public partial class TabItemsSwither : UserControl
    {
        public event EventHandler LeftButtonClick;
        public event EventHandler RightButtonClick;

        public bool LeftButtonVisible
        {
            get => LeftButton.Visibility == Visibility;
            set => LeftButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool RightButtonVisible
        {
            get => RightButton.Visibility == Visibility;
            set => RightButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }



        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(TabItemsSwither), 
                new FrameworkPropertyMetadata(1, 
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnCountChange)));

        private static void OnCountChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is int cnt)) return;
            if (!(d is TabItemsSwither swtchr)) return;

            if (cnt < 0)
                swtchr.Count = 0;
            if (cnt > 0)
                swtchr.CheckPosition();
        }

        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(int), typeof(TabItemsSwither),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnPositionChange)));

        private static void OnPositionChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is int pos)) return;
            if (!(d is TabItemsSwither swtchr)) return;

            swtchr.CheckPosition();
        }

        public TabItemsSwither()
        {
            InitializeComponent();
            CheckPosition();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is Button button)) return;

            switch (button.Name)
            {
                case "LeftButton":
                    Position--;
                    LeftButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                case "RightButton":
                    Position++;
                    RightButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }

        private void CheckPosition()
        {
            LeftButtonVisible = true;
            RightButtonVisible = true;

            if (Position <= 0)
            {
                Position = 0;
                LeftButtonVisible = false;
            }
            if (Position >= Count - 1)
            {
                Position = Count - 1;
                RightButtonVisible = false;
            }
        }
    }
}
