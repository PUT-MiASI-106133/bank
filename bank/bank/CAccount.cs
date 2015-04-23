using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CAccount
    {
        protected int accountID;
        protected decimal saldo;
        private int ownerID;
        private CHistory history;
        public IState State;

        public CAccount(int id, int ownerID)
        {    
            this.saldo = 0;
            this.accountID = id;
            this.ownerID = ownerID;
            this.history = new CHistory();
        }

        public decimal GetSaldo()
        {
            return this.saldo;
        }

        public void SetState(IState state)
        {
            this.State = state;
        }

        public CHistory GetHistory()
        {
            return this.history;
        }

        public int GetAccID()
        {
            return this.accountID;
        }

        private void setNewState()
        {
            if (this.saldo > 0 && this.saldo < 1000)
                this.SetState(new CLowInterest());
            else if (this.saldo >= 1000 && this.saldo < 10000)
                this.SetState(new CAverageInterest());
            else if (this.saldo >= 10000)
                this.SetState(new CHighInterest());
        }

        public void Request()
        {
            setNewState();

            this.saldo = this.State.capitalize(this);
        }

        public void addMoney(decimal amount)
        {
            this.saldo += amount;
        }


        public bool substrMoney(decimal amount)
        {
            bool success = false;
            if (saldo >= amount)
            {
                this.saldo -= amount;
                success = true;
            }
            return success;
        }

        public void DoOperation(IOperation oper)
        {
            oper.execute(this);
        }
    }
}
