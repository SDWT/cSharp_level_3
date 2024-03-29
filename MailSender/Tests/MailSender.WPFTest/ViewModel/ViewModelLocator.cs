/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MailSender.WPFTest"
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
using MailSender.lib.Services.DataProviders.Interfaces;
using MailSender.lib.Services.DataProviders.InMemory;
using MailSender.lib.Services.DataProviders.Linq2SQL;
using MailSender.lib.Data.Linq2SQL;
using System;

namespace MailSender.WPFTest.ViewModel
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
            var services = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            services.Register<MainWindowViewModel>();

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            services
                .TryRegister<IRecipientsDataProvider, Linq2SQLRecipientsDataProvider>()
                .TryRegister(() => new MailSenderDBDataContext());

            //services
            //    .TryRegister<IRecipientsDataProvider, InMemoryRecipientsDataProvider>()
            //    .TryRegister<ISendersDataProvider, InMemorySendersDataProvider>()
            //    .TryRegister<IServersDataProvider, InMemoryServersDataProvider>();
        }

        //public MainViewModel Main
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<MainViewModel>();
        //    }
        //}

        public MainWindowViewModel MainWindowVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }

    public static class SimpleIocExtentions
    {
        public static SimpleIoc TryRegister<TIntrface, TService>(this SimpleIoc services)
            where TIntrface : class
            where TService : class, TIntrface
        {
            if (services.IsRegistered<TIntrface>()) return services;
            services.Register<TIntrface, TService>();
            return services;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc services)
            where TService : class
        {
            if (services.IsRegistered<TService>()) return services;
            services.Register<TService>();
            return services;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc services, Func<TService> Factory)
            where TService : class
        {
            if (services.IsRegistered<TService>()) return services;
            services.Register(Factory);
            return services;
        }
    }
}