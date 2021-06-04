using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Bank
    {
        private DepositParametrs depositParametrs;

        private FasterObservableCollection<IndividualClient> individualDepartment;
        private FasterObservableCollection<LegalClient> legalDepartment;

        private DateTime currentDate;

        private TransferHistory transferHistory;

        public static event Action<Client, Deposit, Client, Deposit, int> TransferEvent;

        public Bank()
        {
            LoadDepositParametrs();
            this.individualDepartment = new  FasterObservableCollection<IndividualClient>();

            this.legalDepartment = new FasterObservableCollection<LegalClient>();
        }

        public DateTime CurrentDate
        {
            get
            {
                return this.currentDate;
            }
            set
            {
                this.currentDate = value;
            }
        }

        public TransferHistory TransferHistory
        {
            get
            {
                return transferHistory;
            }
            set
            {
                transferHistory = value;
            }
        }

        public DepositParametrs DepositParametrs
        {
            get
            {
                return depositParametrs;
            }
            set
            {
                depositParametrs = value;
            }
        }

        public FasterObservableCollection<IndividualClient> IndividualClients
        {
            get
            {
                return individualDepartment;
            }
            set
            {
                individualDepartment = value;
            }
        }

        public FasterObservableCollection<LegalClient> LegalClients
        {
            get
            {
                return legalDepartment;
            }
            set
            {
                legalDepartment = value;
            }
        }

        /// <summary>
        /// Добавляет физического клиента в банк.
        /// </summary>
        /// <param name="client">Клиент.</param>
        public void AddClient(IndividualClient client)
        {
            this.individualDepartment.Add(client);
        }

        /// <summary>
        /// Добавляет юридического клиента в банк.
        /// </summary>
        /// <param name="client">Клиент.</param>
        public void AddClient(LegalClient client)
        {
            this.legalDepartment.Add(client);
        }

        /// <summary>
        /// Удаляет клиента по его id.
        /// </summary>
        /// <param name="id">иде</param>
        public void DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Заменяет клиента с указанным id на нового.
        /// </summary>
        /// <param name="id">Id старого клиента.</param>
        /// <param name="client">Новый клиент.</param>
        public void EditClient(int id, Client client)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Рассчитывает сумму, которая должна лежать на счетах всех клиентов.
        /// </summary>
        public void UpdateAllDeposits()
        {
            throw new NotImplementedException();
        }

        public void GenerateClients(int individualAmount, int legalAmount)
        {
            Random r = new Random();
            List<Client> clientsCol = new List<Client>();
            int count = 0;
            for (int i = 0; i <= individualAmount; i++)
            {
                IndividualClient individualClient = new IndividualClient(
                    $"Имя_{i}",
                    $"Фамилия_{i}",
                    DateTime.Today.Subtract(TimeSpan.FromDays(r.Next(7304, 25566))),
                    Convert.ToBoolean(r.Next(2))
                    );
                clientsCol.Add(individualClient);

                int l = r.Next(0, 2);
                if (individualClient.IsVIP)
                {
                    individualClient.AddDeposit(new Deposit(
                        $"Депозит_{i}",
                        DateTime.Today,
                        this.depositParametrs.VipParametrs[l].MinBallance,
                        this.depositParametrs.VipParametrs[l]
                        ));
                }
                else
                {
                    individualClient.AddDeposit(new Deposit(
                       $"Депозит_{i}",
                       DateTime.Today,
                       this.depositParametrs.IndividualParametrs[l].MinBallance,
                       this.depositParametrs.IndividualParametrs[l]
                       ));
                }

                if (++count >= 100_000 || i == individualAmount)
                {
                    File.WriteAllText($"./IndividualDepartment/{DateTime.Now.Ticks}.json", JsonConvert.SerializeObject(clientsCol));
                    clientsCol.Clear();
                    count = 0;
                }
            }

            count = 0;
            clientsCol.Clear();

            for (int i = 0; i <= legalAmount; i++)
            {
                LegalClient legalClient;
                legalClient = new LegalClient(
                    $"Имя_{i}",
                    $"Адрес_{i}"
                    );
                clientsCol.Add(legalClient);

                int l = r.Next(0, 2);
                legalClient.AddDeposit(new Deposit(
                    $"Депозит_{i}",
                    DateTime.Today,
                    this.depositParametrs.LegalParametrs[l].MinBallance,
                    this.depositParametrs.LegalParametrs[l]
                    ));

                if (++count >= 100_000 || i == legalAmount)
                {
                    File.WriteAllText($"./LegalDepartment/{DateTime.Now.Ticks}.json", JsonConvert.SerializeObject(clientsCol));
                    clientsCol.Clear();
                    count = 0;
                }
            }
        }

        public async void GenerateClientsAsync(int individualAmount, int legalAmount)
        {
            await Task.Run(() => GenerateClients(individualAmount, legalAmount));
        }

        private void LoadIndividualDepartments()
        {
            throw new NotImplementedException();
        }

        private void LoadLegalDepartments()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Загружает параметры депозитов. Если файла "DepositParametrs.json" не существует, то исполюзует стандартные параметры.
        /// </summary>
        private void LoadDepositParametrs()
        {
            try
            {
                string json = File.ReadAllText("DepositParametrs.json");
                this.depositParametrs = JsonConvert.DeserializeObject<DepositParametrs>(json);
            }
            catch
            {
                this.depositParametrs = new DepositParametrs()
                {
                    IndividualParametrs = new List<DepositParametr>()
                    {
                        new DepositParametr("Индивидуальный_1", 1000, 2, false, 4),
                        new DepositParametr("Индивидуальный_2", 5000, 3, false, 8),
                        new DepositParametr("Индивидуальный_3", 10000, 4, false, 12)
                    },
                    VipParametrs = new List<DepositParametr>()
                    {
                        new DepositParametr("VIP_1", 500, 3, false, 4),
                        new DepositParametr("VIP_2", 2500, 4, false, 8),
                        new DepositParametr("VIP_3", 5000, 5, false, 10)
                    },
                    LegalParametrs = new List<DepositParametr>()
                    {
                         new DepositParametr("Юридический_1", 5000, 5, false, 12),
                        new DepositParametr("Юридический_2", 10000, 6, false, 24)
                    }
                };
            }
        }

        /// <summary>
        /// Перевод денег с одного счета на другой.
        /// </summary>
        /// <param name="firstDeposit">Счет-отправитель.</param>
        /// <param name="secondDeposit">Счет-получатель.</param>
        /// <param name="amount">Сумма.</param>
        /// <returns>Возвращает true - если перевод выполнен успешно, false - если перевод не удался.</returns>
        public bool Transfer(Client firstClient, Deposit firstDeposit, Client secondClient, Deposit secondDeposit, int amount)
        {
            if (firstDeposit.Withdraw(amount))
            {
                secondDeposit.Append(amount);
                TransferEvent?.Invoke(firstClient, firstDeposit, secondClient, secondDeposit, amount);
                return true;
            }
            return false;
        }
    }
}
