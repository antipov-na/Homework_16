using MyLibrary;
using System;
using System.Windows;

namespace Homework_16_1
{
    class AddLegalClientViewModel: ICloseRequest
    {
        private LegalClient legalClient;

        private RelayCommand addLegalClient;
        private RelayCommand exitCommand;
 
        public LegalClient LegalClient
        {
            get
            {
                return legalClient ??
                  (this.legalClient = new LegalClient()
                  {
                      Name = "",
                      Address = ""
                  });
            }
            set
            {
                legalClient = value;
            }
        }

        public RelayCommand AddLegalClient
        {
            get
            {
                return this.addLegalClient ??
                    (this.addLegalClient = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (this.legalClient.Name == "" ||
                                this.legalClient.Address == "")
                            {
                                throw new FormatException();
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Имя или адрес введены неверно!");
                            return;
                        }

                        MainWindowViewModel.Bank.AddClient(this.legalClient);
                        (obj as Window).Close();
                    }));
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return this.exitCommand ??
                    (this.exitCommand = new RelayCommand(obj => {
                        (obj as Window).Close();
                    }));
            }
        }

        public event EventHandler<CloseRequestArgs> CloseRequest;
    }
}
