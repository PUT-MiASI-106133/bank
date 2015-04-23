using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CKIRProxy
    {
        private List<COperation> kirOperations;
       // private bool toKIR;
       // private int bankID;
        private CBank bankUtility;
        private IKIRMediator kirUtility;

        public CKIRProxy(CBank bu, IKIRMediator ku)
        {
            this.bankUtility = bu;
            this.kirUtility = ku;
            this.kirOperations = new List<COperation>();
        }

        public void AddOperation(COperation co)
        {
           // checkID(co);
          //  if (toKIR)
                this.kirOperations.Add(co);
          //  else
          //      Send(co);
        }

        public void Send()  //Send pack to KIR
        {
            this.kirUtility.AddToKIR(this.kirOperations);
            this.kirOperations = new List<COperation>();
        }

        public void makeTransfer(List<COperation> transfer) //Get operations from KIR
        {
            foreach (var v in transfer)
                Send(v);
        }

        private void Send(COperation co)    //Send operations from KIR
        {
            int dest = co.GetDestinationID();
            this.bankUtility.GetAccount(dest).addMoney(co.GetAmount());
            this.bankUtility.GetAccount(dest).GetHistory().AddToHistory(co);
        }

        /*private void SetToKir(bool b)
        {
            this.toKIR = b;
        }*/

        /*private void checkID(COperation co)
        {
            SetToKir(this.bankUtility.CheckAccID(co.GetDestinationID()));
        }*/
    }
}
