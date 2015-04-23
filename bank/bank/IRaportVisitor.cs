using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface IRaportVisitor
    {
        string visit(CMonthRaport monthraport, CHistory history);
        string visit(CPayInRaport payinraport, CHistory history);
        string visit(CTransferRaport transferraport, CHistory history);
    }
}
