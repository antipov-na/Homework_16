using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLibrary
{
    public enum SortedCriterion
    {
        Id,
        Name,
        Surname,
        BirthDate,
        IsVIP,
        Adress
    }

    public abstract class Client : INotifyPropertyChanged
    {
        /// <summary>
        /// Статическое поле staticId
        /// </summary>
        public static int staticClientId;

        private string name;
        private int id;
        private ObservableCollection<Deposit> deposits;

        public Client()
        {
            this.Id = NextClientId();
            this.Deposits = new ObservableCollection<Deposit>();
        }

        public Client(string Name)
        {
            this.Id = NextClientId();
            this.name = Name;
            this.Deposits = new ObservableCollection<Deposit>();
        }

        /// <summary>
        /// Статический метод возвращающий следующий Id клиента.
        /// </summary>
        /// <returns></returns>
        private static int NextClientId()
        {
            staticClientId++;
            return staticClientId;
        }

        /// <summary>
        /// Идетификационный номер клиента.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Имя клиента.
        /// </summary>
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

        /// <summary>
        /// Депозиты клиента.
        /// </summary>
        public ObservableCollection<Deposit> Deposits
        {
            get
            {
                return deposits;
            }
            set
            {
                deposits = value;
                OnPropertyChanged("Deposits");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Добавляет клиенту депозит.
        /// </summary>
        /// <param name="deposit">Депозит.</param>
        public void AddDeposit(Deposit Deposit)
        {
            this.Deposits.Add(Deposit);
        }

        /// <summary>
        /// Удаляет выбранный депозит у клиента.
        /// </summary>
        /// <param name="DepositId">Id депозита для удаления.</param>
        public void DeleteDeposit(Deposit item)
        {
            this.Deposits.Remove(item);
        }

        public static IComparer<Client> SortedBy(SortedCriterion Criterion)
        {
            switch (Criterion)
            {
                case SortedCriterion.Id:
                    return new SortById();
                case SortedCriterion.Name:
                    return new SortByName();
                case SortedCriterion.Surname:
                    return new SortBySurname();
                case SortedCriterion.BirthDate:
                    return new SortByBirthDate();
                case SortedCriterion.Adress:
                    return new SortByAdress();
                default:
                    return new SortByVIP();
            }
        }

        private class SortById : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;

                if (X.Id == Y.Id) return 0;
                else if (X.Id > Y.Id) return 1;
                else return -1;
            }
        }

        private class SortByName : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;

                return String.Compare(X.Name, Y.Name);
            }
        }

        private class SortBySurname : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                IndividualClient X = (IndividualClient)x;
                IndividualClient Y = (IndividualClient)y;

                return String.Compare(X.Surname, Y.Surname);
            }
        }

        private class SortByBirthDate : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                IndividualClient X = (IndividualClient)x;
                IndividualClient Y = (IndividualClient)y;

                if (X.BirthDate == Y.BirthDate) return 0;
                else if (X.BirthDate > Y.BirthDate) return 1;
                else return -1;
            }
        }

        private class SortByAdress : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                LegalClient X = (LegalClient)x;
                LegalClient Y = (LegalClient)y;

                return String.Compare(X.Address, Y.Address);
            }
        }

        private class SortByVIP : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                IndividualClient X = (IndividualClient)x;
                IndividualClient Y = (IndividualClient)y;

                if (X.IsVIP && Y.IsVIP) { return 0; }
                else if (Y.IsVIP) { return 1; }
                else { return -1; }
            }
        }
    }
}

