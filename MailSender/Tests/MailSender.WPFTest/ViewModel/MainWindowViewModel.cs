using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

using MailSender.lib.Entities;
using MailSender.lib.Services.Interfaces;
using MailSender.lib.Services.DataProviders.Interfaces;
using MailSender.lib.Services.DataProviders.InMemory;
using MailSender.lib.Services.DataProviders.Linq2SQL;

namespace MailSender.WPFTest.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SchedulerViewModel SchedulerViewModel { get; }
        #region _WindowTitle
        private string _WindowTitle = "EmailSender";

        public string WindowTitle
        {
            get => _WindowTitle;
            set => Set(ref _WindowTitle, value);
        }
        #endregion

        #region SelectedRecipient
        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }
        #endregion

        #region RecipientsFilter
        private string _RecipientsFilter = default(string);
        public string RecipientsFilter
        {
            get => _RecipientsFilter;
            set
            {
                var newValue = value;
                if (Set(ref _RecipientsFilter, newValue))
                {
                    var yourCostumFilter = new Predicate<object>(item => ((Recipient)item).Name.Contains(newValue));
                    //now we add our Filter
                    Recipients.Filter = yourCostumFilter;
                }

            }
        }
        #endregion


        private IRecipientsDataProvider _RecipientsProvider;


        private ObservableCollection<Recipient> _Recipients = new ObservableCollection<Recipient>();
        public ICollectionView Recipients { get; private set; }

        //public ObservableCollection<Recipient> Recipients2
        //{
        //    get => _Recipients;
        //    set => Set(ref _Recipients, value);
        //}

        #region Commands
        public ICommand RefreshDataCommand { get; }
        public ICommand SaveSelectedRecipientCommand { get; }
        public ICommand CreateSelectedRecipientCommand { get; }

        #endregion

        public MainWindowViewModel(IRecipientsDataProvider RecipientsProvider)
        {
            SchedulerViewModel = new SchedulerViewModel(this);
            _RecipientsProvider = RecipientsProvider;

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
            SaveSelectedRecipientCommand = new RelayCommand(OnSaveSelectedRecipientCommandExecuted);
            CreateSelectedRecipientCommand = new RelayCommand(OnCreateSelectedRecipientCommandExecuted);
            //RefreshData();
            Recipients = CollectionViewSource.GetDefaultView(_Recipients);
        }

        private void OnCreateSelectedRecipientCommandExecuted()
        {
            throw new NotImplementedException();
        }

        private void OnSaveSelectedRecipientCommandExecuted()
        {
            _RecipientsProvider.SaveChanges();
        }

        private bool CanRefreshDataCommandExecute() => true;

        private void OnRefreshDataCommandExecuted()
        {
            RefreshData();
        }

        private void RefreshData()
        {
            var recipients = _Recipients;
            recipients.Clear();

            foreach (var recipient in _RecipientsProvider.GetAll())
                recipients.Add(recipient);
        }
    }
}
