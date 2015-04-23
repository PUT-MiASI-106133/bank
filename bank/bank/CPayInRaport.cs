using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public class CPayInRaport : IRaport
    {
        public void accept(IRaportVisitor vis, CHistory history)
        {
            vis.visit(this, history);
        }
    }
}
