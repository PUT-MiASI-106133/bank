using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;

namespace bank
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            // configure IMailSender and ILogging to resolve to their specified concrete implementations
            Bind<IBank>().To<CBank>();
            Bind<IKIRMediator>().To<CKIR>();
        }
    }
}