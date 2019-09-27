/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MailSender"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using MailSender.lib.Services.Interfaces;
using MailSender.lib.Services.InMemory;
using MailSender.lib.Services.Linq2SQL;
using MailSender.lib.Data.Linq2SQL;

namespace MailSender.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var sevices = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);



            SimpleIoc.Default.Register<WpfMailSenderViewModel>();
            sevices.Register<IRecipientsDataProvider, Linq2SQLRecipientsDataProvider>();
            //sevices.Register<IRecipientsDataProvider, InMemoryRecipientsDataProvider>();
            sevices.Register(() => new MailSenderDBDataContext());
        }

        public WpfMailSenderViewModel MailSenderViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WpfMailSenderViewModel>();
            }
        }
        
        public static void Cleanup()
        {
        }
    }
}