using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class DecoratorDebit : IAccount
    {
      //  IAccount acc;
        IAccount bank;

        public DecoratorDebit(IAccount bank)
        {
        //    this.acc = acc;
            this.bank = bank;
        }

        public bool WithDraw(CBank bank, CAccount acc, decimal amount)
        {
            if (this.bank.WithDraw(bank, acc, amount)) return true;
            else
            {
                WithDraw withdraw = new WithDraw(amount, acc);
                IOperation oper = withdraw;

                acc.DoOperation(oper);
                acc.GetHistory().AddToHistory(withdraw);
                return true;
            }
        }
    }
}
