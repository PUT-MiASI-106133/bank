using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CAverageInterest : IState
    {
        private decimal interest;

        public CAverageInterest()
        {
            this.interest = 0.2m;
        }

        public decimal capitalize(CAccount acc)
        {
            decimal temp = acc.GetSaldo();
            temp += temp * this.interest;
            return temp;
        }
    }
}
