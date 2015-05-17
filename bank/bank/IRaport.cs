using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface IRaport
    {
        string accept(IRaportVisitor vis, CHistory history);
    }
}
