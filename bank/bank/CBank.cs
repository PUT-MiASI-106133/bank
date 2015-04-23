using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CBank : IAccount, IBank
    {
        private List<CAccount> accounts;
        private List<CCustomer> customers;
        private CKIRProxy kirProxy;

        public CBank(IKIRMediator kir)
        {
            this.accounts = new List<CAccount>();
            this.customers = new List<CCustomer>();
            this.kirProxy = new CKIRProxy(this, kir);
        }

        public void StoreAccount(int id, int ownerID)
        {
            CAccount acc = new CAccount(id, ownerID);
            this.accounts.Add(acc);

            CCustomer c = GetCustomer(ownerID);
            c.AddAccount(acc);
        }

        public CCustomer AddCustomer(string name, string surname, int id)
        {
            CCustomer c = new CCustomer(name, surname, id);
            this.customers.Add(c);
            return c;
        }

        public bool CheckAccID(int id)
        {
            foreach (var v in this.accounts)
            {
                if (v.GetAccID() == id)
                    return false;
            }
            return true;
        }

        public CAccount GetAccount(int id)
        {
            foreach (var v in this.accounts)
            {
                if (v.GetAccID() == id)
                    return v;
            }
            return null;
        }

        public CCustomer GetCustomer(int id)
        {
            foreach (var v in this.customers)
            {
                if (v.GetCustomerID() == id)
                    return v;
            }
            return null;
        }

        public List<CCustomer> GetCustomerList()
        {
            return this.customers;
        }

        public void Receive(List<COperation> transfer)
        {
            if (transfer != null)
            {
                this.kirProxy.makeTransfer(transfer);
            }
            else
                throw new Exception("Transfer failed");
        }

        public void Send()
        {
            this.kirProxy.Send();
        }

        public void PayIn(CAccount acc, decimal amount)
        {
            PayIn payin = new PayIn(amount, acc);
            IOperation oper = payin;
            acc.DoOperation(oper);
            acc.GetHistory().AddToHistory(payin);
        }

        public void Transfer(CAccount from, CAccount to, decimal amount)
        {
            Transfer transfer = new Transfer(to, amount, from); 
            IOperation oper = transfer;
            from.DoOperation(oper);
            from.GetHistory().AddToHistory(transfer);
           // to.GetHistory().AddToHistory(transfer);
        }

        public bool WithDraw(CAccount acc, decimal amount)
        {
            bool positive = false;
            WithDraw withdraw = new WithDraw(amount, acc);
            IOperation oper = withdraw;
            if (acc.GetSaldo() >= amount)
            {
                acc.DoOperation(oper);
                acc.GetHistory().AddToHistory(withdraw);
                positive = true;
            }
            return positive;
        }
    }
}
