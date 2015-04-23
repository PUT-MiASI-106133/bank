using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CCustomer
    {
        private string name;
        private string surname;
        private int id;
        private List<CAccount> accounts;

        public CCustomer(string name, string surname, int id)
        {
            this.accounts = new List<CAccount>();
            this.name = name;
            this.surname = surname;
            this.id = id;
        }

        public int GetCustomerID()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetSurname()
        {
            return this.surname;
        }

        public List<CAccount> GetAccounts()
        {
            return this.accounts;
        }

        public void AddAccount(CAccount acc)
        {
            accounts.Add(acc);
        }
    }
}
