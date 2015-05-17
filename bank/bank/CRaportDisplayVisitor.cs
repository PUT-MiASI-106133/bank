using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank 
{
    public class CRaportDisplayVisitor : IRaportVisitor
    {
        public string visit(CMonthRaport monthraport, CHistory history)
        {
            List<COperation> temp = history.GetOperations();
            string raport = "";
            DateTime dt = DateTime.Now;
            int month = dt.Month;
            foreach (COperation op in temp)
            {
                if (month == op.GetDate().Month)
                {
                    raport += op.GetAmount() + "-";
                    raport += op.GetOperationType() + "...";
                }
            }
            return raport.Substring(0, raport.Length - 3);
        }

        public string visit(CPayInRaport payinraport, CHistory history)
        {
            List<COperation> temp = history.GetOperations();
            string raport = "";
            foreach (COperation op in temp)
            {
                if (op.GetOperationType() == "PAYIN")
                {
                    raport += op.GetAmount() + "-";
                    raport += op.GetOperationType() + "...";
                } 
            }
            return raport.Substring(0, raport.Length - 3);
        }

        public string visit(CTransferRaport transferraport, CHistory history)
        {
            List<COperation> temp = history.GetOperations();
            string raport = "";
            foreach (COperation op in temp)
            {
                if (op.GetOperationType() == "TRANSFER")
                {
                    raport += op.GetAmount() + "-";
                    raport += op.GetOperationType() + "...";
                } 
            }
            return raport.Substring(0, raport.Length-3);
        }
    }
}
