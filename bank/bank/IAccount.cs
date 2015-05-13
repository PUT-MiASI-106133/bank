using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface IAccount
    {
        bool WithDraw(CBank bank, CAccount acc, decimal amount);
    }
}
