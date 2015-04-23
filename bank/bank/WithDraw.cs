using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class WithDraw : COperation, IOperation
    {
        public WithDraw(decimal amount, CAccount acc)
        {
            this.operationType = "WITHDRAW";
            this.amount = amount;
            this.sourceID = acc.GetAccID();
            this.destinationID = -1;
        }

        public void execute(CAccount acc)
        {
                acc.substrMoney(amount);
        }
    }
}
