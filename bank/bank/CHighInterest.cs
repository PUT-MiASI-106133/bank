using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CHighInterest : IState
    {
        private decimal interest;

        public CHighInterest()
        {
            this.interest = 0.3m;
        }

        public decimal capitalize(CAccount acc)
        {
            decimal temp = acc.GetSaldo();
            temp += temp * this.interest;
            return temp;
        }
    }
}
