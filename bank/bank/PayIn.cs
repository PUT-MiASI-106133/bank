using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class PayIn : COperation, IOperation
    {
        public PayIn(decimal amount, CAccount dest)
        {
            this.operationType = "PAYIN";
            this.amount = amount;
            this.destinationID = dest.GetAccID();
            this.sourceID = -1;
        }

        public void execute(CAccount acc)
        {
            acc.addMoney(amount);
            date = DateTime.Now;
        }
    }
}
