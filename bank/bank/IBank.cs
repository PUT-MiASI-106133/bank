using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface IBank
    {
        void Receive(List<COperation> transfer);
        void Send();
        bool CheckAccID(int id);
    }
}
