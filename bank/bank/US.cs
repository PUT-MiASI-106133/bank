using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class US
    {
        private List<Transfer> transferList;

        public US()
        {
            this.transferList = new List<Transfer>();
        }

        public List<Transfer> GetTransferList()
        {
            return transferList;
        }

        public void LogTransfer(Transfer transfer)
        {
            transferList.Add(transfer);
        }
    }
}
