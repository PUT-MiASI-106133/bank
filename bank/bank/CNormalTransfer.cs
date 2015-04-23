using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CNormalTransfer : ITransferResposibility
    {
        private ITransferResposibility next;

        public void setNextTransferResp(ITransferResposibility next)
        {
            this.next = next;
        }

        public void transfer(Transfer transfer)
        {
            if (transfer.GetAmount() < 20000m) ;
            else
            {
                next.transfer(transfer);
            }

        }
    }
}
