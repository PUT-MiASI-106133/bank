using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface IKIRMediator
    {
        void AddToKIR(List<COperation> pack);
        void AddBank(CBank bank);
        void Send();
    }
}
