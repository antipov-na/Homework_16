using MyLibrary;
using System;
using System.Windows;

namespace Homework_16_1
{
    class AddClientViewModel :ICloseRequest
    {
        private IndividualClient individualClient;

        private RelayCommand addIndividualClient;

        private RelayCommand exitCommand;

        public IndividualClient IndividualClient
        {
            get
            {
                return individualClient ??
                  (this.individualClient = new IndividualClient()
                  {
                      Name = "",
                      Surname = "",
                      BirthDate = DateTime.Today,
                      IsVIP = false
                  });
            }
            set
            {
                individualClient = value;

            }
        }

        public RelayCommand AddIndividualClient
        {
            get
            {
                return this.addIndividualClient ??
                    (this.addIndividualClient = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (this.individualClient.Name == "" ||
                                this.individualClient.Surname == "")
                            {
                                throw new FormatException();
                            }
                            Convert.ToInt64(this.individualClient.BirthDate.Ticks);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Имя или фамилия введены неверно!");
                            return;
                        }

                        catch (InvalidCastException)
                        {
                            MessageBox.Show("Дата введена неверно!");
                            return;
                        }

                        MainWindowViewModel.Bank.AddClient(this.individualClient);
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
