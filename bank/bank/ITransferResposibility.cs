using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    public interface ITransferResposibility
    {
        void setNextTransferResp(ITransferResposibility next);
        void transfer(Transfer transfer);
    }
}
