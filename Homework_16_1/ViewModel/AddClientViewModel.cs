using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_16_1
{
    public class AddClientViewModel
    {
        private IndividualClient individualClient;
        private LegalClient legalClient;

        private RelayCommand addIndividualClient;
        private RelayCommand addLegalClient;
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
    }
}
