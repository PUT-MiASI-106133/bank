using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class COperation
    {
        protected string operationType;
        protected DateTime date;
        protected decimal amount;
        protected int destinationID;
        protected int sourceID;

        public string GetOperationType()
        {
            return this.operationType;
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public decimal GetAmount()
        {
            return this.amount;
        }

        public int GetDestinationID()
        {
            return this.destinationID;
        }

        public int GetSourceID()
        {
            return this.sourceID;
        }
    }
}
