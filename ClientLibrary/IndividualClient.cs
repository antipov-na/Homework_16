using Newtonsoft.Json.Linq;
using System;

namespace MyLibrary
{
    public class IndividualClient : Client
    {
        string surname;
        DateTime birthDate;
        bool isVIP;

        public IndividualClient()
            : base()
        {

        }

        public IndividualClient(JToken client)
            : base()
        {
            Deserialize(client);
        }

        public IndividualClient(string Name, string Surname, DateTime BirthDate, bool IsVIP)
            : base(Name)
        {
            this.surname = Surname;
            this.birthDate = BirthDate;
            this.isVIP = IsVIP;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Surname
        {
            get
            {
                return this.surname;
            }
            set
            {
                this.surname = value;
                OnPropertyChanged("Surname");
            }
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }
            set
            {
                this.birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        /// <summary>
        /// Являеется ли клиетн VIP.
        /// </summary>
        public bool IsVIP
        {
            get
            {
                return this.isVIP;
            }
            set
            {
                this.isVIP = value;
                OnPropertyChanged("isVIP");
            }
        }

        /// <summary>
        /// Сериализует данные о клиенте.
        /// </summary>
        /// <returns></returns>
        public JObject Serialize()
        {
            JArray deposits = new JArray();
            foreach (var deposit in this.Deposits)
            {
                deposits.Add(deposit.Serialize());
            }

            JObject result = new JObject
            {
                ["type"] = "individualClient",
                ["id"] = this.Id,
                ["name"] = this.Name,
                ["surname"] = this.Surname,
                ["birthDate"] = this.BirthDate,
                ["isVIP"] = this.IsVIP,
                ["deposits"] = deposits
            };

            return result;
        }

        private void Deserialize(JToken client)
        {
            this.Id = (int)client["id"];
            this.Name = (string)client["name"];
            this.Surname = (string)client["surname"];
            this.BirthDate = (DateTime)client["birthDate"];
            this.IsVIP = (bool)client["isVIP"];
        }
    }
}

