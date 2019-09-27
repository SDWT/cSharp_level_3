using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.lib.Data.Linq2SQL;
using MailSender.lib.Services;

namespace MailSender.WPFTest.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
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

        private RecipientsDataProvider _RecipientsProvider;

        public ObservableCollection<Recipient> Recipients { get; } = new ObservableCollection<Recipient>();

        #region Commands
        public ICommand RefreshDataCommand { get; }
        public ICommand SaveSelectedRecipientCommand { get; }
        public ICommand CreateSelectedRecipientCommand { get; }

        #endregion

        public MainWindowViewModel(RecipientsDataProvider RecipientsProvider)
        {
            _RecipientsProvider = RecipientsProvider;

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
            SaveSelectedRecipientCommand = new RelayCommand(OnSaveSelectedRecipientCommandExecuted);
            CreateSelectedRecipientCommand = new RelayCommand(OnCreateSelectedRecipientCommandExecuted);
            //RefreshData();
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
            var recipients = Recipients;
            recipients.Clear();

            foreach (var recipient in _RecipientsProvider.GetAll())
                recipients.Add(recipient);
        }
    }
}
