using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CBigTransfer : ITransferResposibility
    {
        private ITransferResposibility next;
        private US us;

        public void setNextTransferResp(ITransferResposibility next)
        {
            this.next = next;
        }

        public void SetUS(US us)
        {
            this.us = us;
        }

        public void transfer(Transfer transfer)
        {
            if (transfer.GetAmount() >= 20000m)
            {
                us.LogTransfer(transfer);
            }
            else
            {
                ;
            }

        }
    }
}
