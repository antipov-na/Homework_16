using Newtonsoft.Json.Linq;

namespace MyLibrary
{
    public class LegalClient : Client
    {
        string address;

        public LegalClient()
            : base()
        {

        }

        public LegalClient(JToken Client)
            : base()
        {
            Deserialize(Client);
        }

        public LegalClient(string Name, string Adress)
            : base(Name)
        {
            this.address = Adress;
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
                OnPropertyChanged("Address");
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
                ["type"] = "legalClient",
                ["id"] = this.Id,
                ["name"] = this.Name,
                ["address"] = this.Address,
                ["deposits"] = deposits
            };

            return result;
        }

        /// <summary>
        /// Десериализует информацию о клиенте из json.
        /// </summary>
        /// <param name="Client"></param>
        private void Deserialize(JToken Client)
        {
            this.Id = (int)Client["id"];
            this.Name = (string)Client["name"];
            this.Address = (string)Client["address"];
        }
    }
}
