using System.Collections.Generic;

namespace MyLibrary
{
    public class DepositParametrs
    {

        public List<DepositParametr> IndividualParametrs;
        public List<DepositParametr> VipParametrs;
        public List<DepositParametr> LegalParametrs;

        public DepositParametrs()
        {
            this.IndividualParametrs = new List<DepositParametr>();
            this.VipParametrs = new List<DepositParametr>();
            this.LegalParametrs = new List<DepositParametr>();
        }

    }
}
