using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CKIR
    {
        private List<COperation> listKIR;
        private Dictionary<CBank, List<COperation>> listOfBanks;
        private List<COperation> sendList;

        public CKIR()
        {
            this.sendList = new List<COperation>();
            this.listKIR = new List<COperation>();
            this.listOfBanks = new Dictionary<CBank, List<COperation>>();
        }

        public void AddToKIR(List<COperation> pack)
        {
            this.listKIR.AddRange(pack);
        }

        public void AddBank(CBank bank)
        {
            foreach (var v in this.listOfBanks)
            {
                if (v.Key == bank)  //(v.Key.GetBankID() == bank.GetBankID())
                    throw new Exception("Bank with given ID already exists in KIR");
            }
            this.listOfBanks.Add(bank, new List<COperation>());
        }

        private void addOperation(CBank bu, COperation o)
        {
            listOfBanks[bu].Add(o);
        }

        public void Send()  //List<COperation> transferList
        {
            foreach (var v in this.listKIR)
                foreach (var bank in this.listOfBanks)
                    if (!bank.Key.CheckAccID(v.GetDestinationID()))
                    {
                        this.addOperation(bank.Key, v);
                        break;
                    }

            foreach (var bank in this.listOfBanks)
            {
                if (bank.Value != null)
                {
                    bank.Key.AddTransfer(bank.Value);
                }
            }
        }

        /*public bool IsBankExist(int ID)
        {
            foreach (var v in this.listOfBanks)
            {
                if (v.Key.GetBankID() == ID)
                    return true;
            }
            return false;
        }*/

        public List<COperation> GetListKir()
        {
            return this.listKIR;
        }

        public Dictionary<CBank, List<COperation>> GetListOfBanks()
        {
            return this.listOfBanks;
        }
    }
}
