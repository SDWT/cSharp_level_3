using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.lib;
using MailSender.lib.Entities;

namespace MailSender.WPFTest.ViewModel
{
    public class SchedulerViewModel : ViewModelBase
    {
        MainWindowViewModel MainWindowViewModel { get; }

        // Connect to MainWindowViewModel.ListsViewModel -> SelectedSender
        private Sender _Sender = default(Sender);
        public Sender Sender
        {
            get => _Sender;
            set => Set(ref _Sender, value);
        }

        // Connect to MainWindowViewModel.ListsViewModel -> SelectedServer
        private Server _Server = default(Server);
        public Server Server
        {
            get => _Server;
            set => Set(ref _Server, value);
        }

        // Connect to MainWindowViewModel.EmailEditViewModel -> Email
        private Email _Email = default(Email);
        public Email Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        // Connect to DatePicker
        private DateTime _PlanTime = default(DateTime);
        public DateTime PlanTime
        {
            get => _PlanTime;
            set => Set(ref _PlanTime, value);
        }

        // Connect to Calendar
        private DateTime _PlanDate = default(DateTime);
        public DateTime PlanDate
        {
            get => _PlanDate;
            set => Set(ref _PlanDate, value);
        }


        private DateTime dtService = new DateTime();

        public SchedulerViewModel(MainWindowViewModel mwvm)
        {
            MainWindowViewModel = mwvm;

            PlanDate = DateTime.Now;
            PlanTime = DateTime.Now;

            PlanCommand = new RelayCommand(OnPlanCommandExecuted, CanPlanCommandExecute);
            SendCommand = new RelayCommand(OnSendCommandExecuted, CanSendCommandExecute);
        }

        private bool SchedulerTaskComplete()
        {
            if (Sender is null || Server is null ||
                Email is null || Email.Body == string.Empty)
                return false;

            return true;
        }

        private bool SchedulerTaskComplete(DateTime plan)
        {
            if (plan <= DateTime.Now)
                return false;

            return SchedulerTaskComplete();
        }

        private bool CanSendCommandExecute()
        {
            return SchedulerTaskComplete();
        }

        private void OnSendCommandExecuted()
        {
            var task = new SchedulerTask
            {
                Sender = Sender,
                Server = Server,
                Email = Email,
                Time = DateTime.Now,
                Recipients = new RecipientsList
                {
                    // ???
                    //Recipients = MainWindowViewModel.Recipients.
                }
            };
        }

        private bool CanPlanCommandExecute()
        {
            return SchedulerTaskComplete(new DateTime(PlanDate.Year, PlanDate.Month, PlanDate.Day,
                PlanTime.Hour, PlanTime.Minute, 0));
        }

        private void OnPlanCommandExecuted()
        {
            var task = new SchedulerTask
            {
                Sender = Sender,
                Server = Server,
                Email = Email,
                Time = new DateTime(PlanDate.Year, PlanDate.Month, PlanDate.Day,
                PlanTime.Hour, PlanTime.Minute, 0),
                Recipients = new RecipientsList
                {
                    // ???
                    //Recipients = MainWindowViewModel.Recipients.
                }
            };
        }

        #region Commands
        public ICommand PlanCommand { get; }
        public ICommand SendCommand { get; }

        #endregion
    }
}
