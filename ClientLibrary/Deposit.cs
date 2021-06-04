using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLibrary
{
    public class Deposit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        static public int staticDepositId;

        private readonly int id;
        private string name;
        private readonly DateTime depositDate;
        private readonly DateTime fullPaymentDate;
        private DateTime nextPaymentDate;
        private float ballance;
        private readonly float startBallance;
        private readonly float interest;
        private readonly bool isCapitalized;

        public Deposit(JToken Deposit)
        {
            this.id = (int)Deposit["id"];
            this.name = (string)Deposit["name"];
            this.depositDate = (DateTime)Deposit["depositDate"];
            this.nextPaymentDate = (DateTime)Deposit["nextPaymentDate"];
            this.fullPaymentDate = (DateTime)Deposit["fullPaymentDate"];
            this.startBallance = (int)Deposit["startBallance"];
            this.ballance = (float)Deposit["ballance"];
            this.interest = (float)Deposit["interest"];
            this.isCapitalized = (bool)Deposit["isCapitalized"];
        }

        public Deposit(string Name,
                       DateTime DepositDay,
                       float StartBallance,
                       DepositParametr DepositParametr)
        {
            this.id = NextDepositId();
            this.name = Name;
            this.depositDate = DepositDay;
            this.nextPaymentDate = DepositDay.AddDays(31);
            this.fullPaymentDate = DepositDate.AddDays(DepositParametr.DepositPeriod * 31);
            this.ballance = StartBallance;
            this.startBallance = StartBallance;
            this.interest = DepositParametr.Interest;
            this.isCapitalized = DepositParametr.IsCapitalized;
        }

        /// <summary>
        /// Идентификационный номер депозита.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Название депозита.
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
        /// Дата создания вклада.
        /// </summary>
        public DateTime DepositDate
        {
            get
            {
                return this.depositDate;
            }
        }

        /// <summary>
        /// Дата следующей выплаты.
        /// </summary>
        public DateTime NextPaymentDate
        {
            get
            {
                return this.nextPaymentDate;
            }
            private set
            {
                this.nextPaymentDate = value;
                OnPropertyChanged("NextPaymentDay");
            }
        }

        /// <summary>
        /// Дата полной выплаты.
        /// </summary>
        public DateTime FullPaymentDate
        {
            get
            {
                return this.fullPaymentDate;
            }
        }

        /// <summary>
        /// Балланс депозита.
        /// </summary>
        public float Ballance
        {
            get
            {
                return this.ballance;
            }
            private set
            {
                this.ballance = value;
                OnPropertyChanged("Ballance");
            }
        }

        /// <summary>
        /// Процент депозита.
        /// </summary>
        public float Interest
        {
            get
            {
                return this.interest;
            }
        }

        /// <summary>
        /// Является ли вклад капитализированным
        /// </summary>
        public bool IsCapitalized
        {
            get
            {
                return this.isCapitalized;
            }
        }

        /// <summary>
        /// Обновляет балланс депозита.
        /// </summary>
        public void UpdateBallance(DateTime CureentDate)
        {
            while (CureentDate >= this.nextPaymentDate &&
                   CureentDate <= this.fullPaymentDate)
            {
                if (this.isCapitalized)
                {
                    this.Ballance += this.ballance * (this.interest / 100);
                }
                else
                {
                    this.Ballance += this.startBallance * (this.interest / 100);
                }
                this.NextPaymentDate = this.nextPaymentDate.AddDays(31);
            }

        }

        /// <summary>
        /// Снимает деньги со счета.
        /// </summary>
        /// <param name="Amount">Сумма.</param>
        /// <returns>Возвращает true - снятие денег прошло успешно, 
        ///                     false - не достаточно денег на счету.</returns>
        public bool Withdraw(int Amount)
        {
            if (Amount >= this.ballance) { return false; }
            this.Ballance -= Amount;
            //OnPropertyChanged();
            return true;
        }

        /// <summary>
        /// Зачисялет деньги на счет.
        /// </summary>
        /// <param name="Amount"></param>
        public void Append(int Amount)
        {
            this.Ballance += Amount;
            OnPropertyChanged();
        }

        /// <summary>
        /// Статический метод возвращающий следующий Id депозита.
        /// </summary>
        /// <returns></returns>
        private static int NextDepositId()
        {
            staticDepositId++;
            return staticDepositId;
        }

        public JObject Serialize()
        {
            return new JObject
            {
                ["id"] = this.Id,
                ["name"] = this.Name,
                ["depositDate"] = this.DepositDate,
                ["nextPaymentDate"] = this.NextPaymentDate,
                ["fullPaymentDate"] = this.FullPaymentDate,
                ["startBallance"] = this.startBallance,
                ["ballance"] = this.Ballance,
                ["interest"] = this.Interest,
                ["isCapitalized"] = this.IsCapitalized
            };
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
