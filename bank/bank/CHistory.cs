using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CHistory
    {
        private List<COperation> operations;

        public CHistory()
        {
            this.operations = new List<COperation>();
        }

        public void AddToHistory(COperation co)
        {
            operations.Add(co);
        }

        public List<COperation> GetOperations()
        {
            return this.operations;
        }
    }
}
