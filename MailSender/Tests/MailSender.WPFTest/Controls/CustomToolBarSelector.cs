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
using FontAwesome.WPF;


namespace MailSender.WPFTest.Controls
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MailSender.WPFTest.Controls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MailSender.WPFTest.Controls;assembly=MailSender.WPFTest.Controls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:CustomToolBarSelector/>
    ///
    /// </summary>
    public class CustomToolBarSelector : ToolBar
    {
        //static CustomToolBarSelector()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomToolBarSelector), new FrameworkPropertyMetadata(typeof(CustomToolBarSelector)));
        //}

        #region Controls
        public Button btnEdit = new Button();
        public Button btnAdd = new Button();
        public Button btnDelete = new Button();
        public ComboBox cmbBx = new ComboBox();
        public Label lblText = new Label();
        #endregion
        #region Events
        public event EventHandler AddButtonClick;
        public event EventHandler EditButtonClick;
        public event EventHandler DeleteButtonClick;
        #endregion
        #region ItemSource
        public System.Collections.IEnumerable ItemSource
        {
            get { return (System.Collections.IEnumerable)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", 
                typeof(System.Collections.IEnumerable), 
                typeof(CustomToolBarSelector),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnSourceChange)));
        private static void OnSourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is System.Collections.IEnumerable itemSource)) return;
            if (!(d is CustomToolBarSelector slctr)) return;
            slctr.cmbBx.ItemsSource = itemSource;
        }


        #endregion
        #region DisplayMemberPath


        public string ComboBoxDisplayMemberPath
        {
            get { return (string)GetValue(ComboBoxDisplayMemberPathProperty); }
            set { SetValue(ComboBoxDisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxDisplayMemberPathProperty =
            DependencyProperty.Register("ComboBoxDisplayMemberPath", 
                typeof(string),
                typeof(CustomToolBarSelector),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnDisplayMemberPathChange)));



        private static void OnDisplayMemberPathChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is string dspMemPath)) return;
            if (!(d is CustomToolBarSelector slctr)) return;
            slctr.cmbBx.DisplayMemberPath = dspMemPath;
        }
        #endregion
        #region SelectedValuePath


        public string ComboBoxSelectedValuePath
        {
            get { return (string)GetValue(ComboBoxSelectedValuePathProperty); }
            set { SetValue(ComboBoxSelectedValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CmbBxSelectedValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxSelectedValuePathProperty =
            DependencyProperty.Register("ComboBoxSelectedValuePath", 
                typeof(string),
                typeof(CustomToolBarSelector),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnSelectedValuePathChange)));


        private static void OnSelectedValuePathChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is string slctMemPath)) return;
            if (!(d is CustomToolBarSelector slctr)) return;
            slctr.cmbBx.SelectedValuePath = slctMemPath;
        }
        #endregion
        #region LabelText
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), 
                typeof(CustomToolBarSelector),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnTextChange)));

        private static void OnTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is string text)) return;
            if (!(d is CustomToolBarSelector slctr)) return;
            slctr.lblText.Content = text;
        }


        #endregion

        public CustomToolBarSelector()
        {
            #region Label
            lblText.MinWidth = 85;

            this.AddChild(lblText);
            #endregion
            #region ComboBox
            cmbBx.MinWidth = 75;
            cmbBx.MaxWidth = 150;

            this.AddChild(cmbBx);
            #endregion

            #region Buttons
            #region images
            var imgAdd = new Image();
            imgAdd.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.Plus, Brushes.Green);
            imgAdd.Height = 18;

            var imgEdit = new Image();
            imgEdit.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.Edit, Brushes.Brown);
            imgEdit.Height = 18;


            var imgDelete = new Image();
            imgDelete.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, Brushes.Red);
            imgDelete.Height = 18;
            #endregion

            btnAdd.Click += new RoutedEventHandler(btnAdd_Click);
            btnEdit.Click += new RoutedEventHandler(btnEdit_Click);
            btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

            btnAdd.Content = imgAdd;
            btnEdit.Content = imgEdit;
            btnDelete.Content = imgDelete;

            this.AddChild(btnAdd);
            this.AddChild(btnEdit);
            this.AddChild(btnDelete);
            #endregion
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteButtonClick?.Invoke(this, EventArgs.Empty);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddButtonClick?.Invoke(this, EventArgs.Empty);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditButtonClick?.Invoke(this, EventArgs.Empty);
        }

    }
}
