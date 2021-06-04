using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace MyLibrary
{
    public class Transfer
    {
        public Transfer(JToken transfer)
        {
            this.FirtsClientId = (int)transfer["firtsClientId"];
            this.FirtsClientName = (string)transfer["firtsClientName"];
            this.FirtsDepositId = (int)transfer["firtsDepositId"];
            this.SecondClientId = (int)transfer["secondClientId"];
            this.SecondClientName = (string)transfer["secondClientName"];
            this.SecondDepositId = (int)transfer["secondDepositId"];
            this.Amount = (int)transfer["amount"];
        }

        public Transfer(
            int FirtsClientId,
            string FirtsClientName,
            int FirtsDepositId,
            int SecondClientId,
            string SecondClientName,
            int SecondDepositId,
            int Amount)
        {
            this.FirtsClientId = FirtsClientId;
            this.FirtsClientName = FirtsClientName;
            this.FirtsDepositId = FirtsDepositId;
            this.SecondClientId = SecondClientId;
            this.SecondClientName = SecondClientName;
            this.SecondDepositId = SecondDepositId;
            this.Amount = Amount;
        }

        /// <summary>
        /// Id отправителя.
        /// </summary>
        public int FirtsClientId { get; }

        /// <summary>
        /// Имя отправителя.
        /// </summary>
        public string FirtsClientName { get; }

        /// <summary>
        /// Id депозита отправителя.
        /// </summary>
        public int FirtsDepositId { get; }

        /// <summary>
        /// Id получателя.
        /// </summary>
        public int SecondClientId { get; }

        /// <summary>
        /// Имя получателя.
        /// </summary>
        public string SecondClientName { get; }

        /// <summary>
        /// Id депозита получателя.
        /// </summary>
        public int SecondDepositId { get; }

        /// <summary>
        /// Сумма перевода.
        /// </summary>
        public int Amount { get; }

        public JToken Serialize()
        {
            return new JObject()
            {
                ["firtsClientId"] = this.FirtsClientId,
                ["firtsClientName"] = FirtsClientName,
                ["firtsDepositId"] = FirtsDepositId,
                ["secondClientId"] = SecondClientId,
                ["secondClientName"] = SecondClientName,
                ["secondDepositId"] = SecondDepositId,
                ["amount"] = Amount
            };
        }
    }

    public class TransferHistory
    {
        public TransferHistory()
        {
            Bank.TransferEvent += OnTransferEvent;

            this.Transfers = new ObservableCollection<Transfer>();
        }

        private void OnTransferEvent(Client client1, Deposit deposit1, Client client2, Deposit deposit2, int amount)
        {
            this.Transfers.Add(new Transfer(client1.Id, client1.Name, deposit1.Id, client2.Id, client2.Name, deposit2.Id, amount));
        }

        public void Add(Transfer transfer)
        {
            this.Transfers.Add(transfer);
        }

        public JToken Serialize()
        {
            JArray res = new JArray();

            foreach (var transfer in this.Transfers)
            {
                res.Add(transfer.Serialize());
            }

            return res;
        }

        public ObservableCollection<Transfer> Transfers { get; private set; }
    }
}

