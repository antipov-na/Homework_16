using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLibrary
{
    public class BankDepartment<T> : INotifyPropertyChanged
              where T : Client
    {
        private FasterObservableCollection<T> clients;

        public BankDepartment()
        {
            this.clients = new FasterObservableCollection<T>();
        }

        /// <summary>
        /// Рассчитывает сумму которая должна лежать на счету.
        /// </summary>
        public void UpdateDeposits(DateTime CurrentDate)
        {
            foreach (var client in clients)
            {
                foreach (var deposit in client.Deposits)
                {
                    deposit.UpdateBallance(CurrentDate);
                }
            }
        }

        /// <summary>
        /// Коллекция со всеми клиентами в этом отделе.
        /// </summary>
        public FasterObservableCollection<T> Clients
        {
            get
            {
                return this.clients;
            }
            set
            {
                this.clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
