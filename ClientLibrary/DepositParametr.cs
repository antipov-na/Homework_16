namespace MyLibrary
{
    public class DepositParametr
    {
        public DepositParametr(string depositName, int minBallance, int interest, bool isCapitalized, int depositPeriod)
        {
            DepositName = depositName;
            MinBallance = minBallance;
            Interest = interest;
            IsCapitalized = isCapitalized;
            DepositPeriod = depositPeriod;
        }

        /// <summary>
        /// Название депозита
        /// </summary>
        public string DepositName { get; private set; }

        /// <summary>
        /// Минимальный взнос
        /// </summary>
        public int MinBallance { get; private set; }

        /// <summary>
        /// Годовой процент
        /// </summary>
        public int Interest { get; private set; }

        /// <summary>
        /// Капитализируемый
        /// </summary>
        public bool IsCapitalized { get; private set; }

        /// <summary>
        /// Продолжительность депозита
        /// </summary>
        public int DepositPeriod { get; private set; }
    }
}
