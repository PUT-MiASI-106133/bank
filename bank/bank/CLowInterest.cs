using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CLowInterest : IState
    {
        private decimal interest;

        public CLowInterest()
        {
            this.interest = 0.1m;
        }

        public decimal capitalize(CAccount acc)
        {
            decimal temp = acc.GetSaldo();
            temp += temp * this.interest;
            return temp;
        }
    }
}
