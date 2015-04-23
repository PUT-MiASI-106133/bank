using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class Transfer : COperation, IOperation
    {
        CAccount dest;

        public Transfer(CAccount to, decimal amount, CAccount from)
        {
            this.operationType = "TRANSFER";
            this.amount = amount;
            this.dest = to;
            this.destinationID = to.GetAccID();
            this.sourceID = from.GetAccID();
        }

        public void execute(CAccount from)
        {
            if (from.substrMoney(amount))
                dest.addMoney(amount);
        }
    }
}
