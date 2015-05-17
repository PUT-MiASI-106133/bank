using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CMonthRaport : IRaport
    {
        public string accept(IRaportVisitor vis, CHistory history)
        {
            return vis.visit(this, history);
        }
    }
}
