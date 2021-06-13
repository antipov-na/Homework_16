using MyLibrary;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Homework_16_1
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private Client selectedClient;
        private Deposit selectedDeposit;
        private int selectedTab;

        private RelayCommand addClientCommand;
        private RelayCommand addDepositCommand;
        private RelayCommand deleteClientCommand;
        private RelayCommand deleteDepositCommand;
        private RelayCommand addClientsAutomaticlyCommand;


        public static Bank Bank;

        private FasterObservableCollection<IndividualClient> individualClients;
        private FasterObservableCollection<LegalClient> legalClients;

        private IDialogService dialogService;

        public MainWindowViewModel(IDialogService dialogService) : this()
        {
            this.dialogService = dialogService;

        }
        private MainWindowViewModel()
        {
            Bank = new Bank();
            this.individualClients = Bank.IndividualClients;
            this.legalClients = Bank.LegalClients;
        }

        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public Client SelectedClient
        {
            get
            {
                return this.selectedClient;
            }
            set
            {
                this.selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        /// <summary>
        /// Выбранный депозит
        /// </summary>
        public Deposit SelectedDeposit
        {
            get
            {
                return selectedDeposit;
            }
            set
            {
                selectedDeposit = value;
                OnPropertyChanged("SelectedDeposit");
            }
        }

        /// <summary>
        /// Выбранная вкладка
        /// </summary>
        public int SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                selectedTab = value;
                this.SelectedClient = null;
                this.SelectedDeposit = null;
                OnPropertyChanged("SelectedTab");
            }
        }

        public FasterObservableCollection<LegalClient> LegalClients
        {
            get
            {
                return legalClients;
            }
            private set
            {
                legalClients = value;
                OnPropertyChanged("LegalClients");
            }
        }
        public FasterObservableCollection<IndividualClient> IndividualClients
        {
            get
            {
                return individualClients;
            }
            private set
            {
                individualClients = value;
                OnPropertyChanged("IndividualClients");
            }
        }

        public RelayCommand AddClientCommand
        {
            get
            {
                return this.addClientCommand ??
                  (this.addClientCommand = new RelayCommand(obj =>
                  {
                      if (this.selectedTab == 0)
                      {
                          AddClientViewModel vm = new AddClientViewModel();
                          bool? dialogResult = dialogService.Show(vm);
                      }
                      else
                      {
                          AddLegalClientViewModel vm = new AddLegalClientViewModel();
                          bool? dialogResult = dialogService.Show(vm);
                      }
                  }));
            }
        }

        public RelayCommand DeleteClientCommand
        {
            get
            {
                return this.deleteClientCommand ??
                    (this.deleteClientCommand = new RelayCommand(obj =>
                    {
                        Client client = (Client)obj;
                        if (client != null)
                        {
                            Bank.DeleteClient(client.Id);
                        }
                    }));
            }
        }

        public RelayCommand DeleteDepositCommand
        {
            get
            {
                return this.deleteDepositCommand ??
                    (this.deleteDepositCommand = new RelayCommand(obj =>
                    {
                        if (this.selectedClient == null || this.selectedDeposit == null)
                        {
                            return;
                        }

                        this.selectedClient.DeleteDeposit(this.selectedDeposit);
                    }));
            }
        }

        public RelayCommand AddDepositCommand
        {
            get
            {
                return this.addDepositCommand ??
                  (this.addDepositCommand = new RelayCommand(obj =>
                  {
                      List<DepositParametr> param;
                      if (this.selectedClient == null)
                      {
                          return;
                      }
                      if (this.selectedClient.GetType() == typeof(IndividualClient))
                      {
                          if ((this.selectedClient as IndividualClient).IsVIP == false)
                          {
                              param = Bank.DepositParametrs.IndividualParametrs;
                          }
                          else
                          {
                              param = Bank.DepositParametrs.VipParametrs;
                          }
                      }
                      else
                      {
                          param = Bank.DepositParametrs.LegalParametrs;
                      }

                      AddDepositViewModel vm = new AddDepositViewModel(this.selectedClient, param);
                      dialogService.Show(vm);
                  }));
            }

        }

        public RelayCommand AddClientsAutomaticlyCommand
        {
            get
            {
                return this.addClientsAutomaticlyCommand ??
                    (this.addClientsAutomaticlyCommand = new RelayCommand(obj =>
                    {
                        GenerateClientsViewModel vm = new GenerateClientsViewModel();
                        dialogService.Show(vm);
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
