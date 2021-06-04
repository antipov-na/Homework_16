using MyLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_16_1
{
    public class AddDepositViewModel : INotifyPropertyChanged
    {
        private Client client;
        private List<DepositParametr> depositParametrs;
        private DepositParametr selectedDepositParametr;
        private string name;
        private int stratBallance;

        private RelayCommand addDeposit;
        private RelayCommand exitCommand;

        public AddDepositViewModel(Client client, List<DepositParametr> depositParametrs)
        {
            this.client = client;
            this.depositParametrs = depositParametrs;
        }

        public DepositParametr SelectedDepositParametr
        {
            get
            {
                return this.selectedDepositParametr;
            }
            set
            {
                this.selectedDepositParametr = value;
                this.StratBallance = value.MinBallance;
                OnPropertyChanged("SelectedDepositParametr");
            }
        }

        public int StratBallance
        {
            get
            {
                return this.stratBallance;
            }
            set
            {
                this.stratBallance = value;
                OnPropertyChanged("StratBallance");
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<DepositParametr> DepositParametrs
        {
            get
            {
                return depositParametrs;
            }
        }

        public RelayCommand AddDeposit
        {
            get
            {
                return this.addDeposit ??
                    (this.addDeposit = new RelayCommand(obj =>
                    {
                        if (this.name == "")
                        {
                            MessageBox.Show("Введите имя!");
                            return;
                        }

                        if (this.stratBallance < this.selectedDepositParametr.MinBallance)
                        {
                            MessageBox.Show($"Балланс должен быть больше {this.selectedDepositParametr.MinBallance}!");
                            return;
                        }

                        this.client.AddDeposit(new Deposit(this.name, DateTime.Today, this.stratBallance, this.selectedDepositParametr));

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
