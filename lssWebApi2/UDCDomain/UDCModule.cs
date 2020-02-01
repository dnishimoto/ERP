using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.UDCDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.UDCDomain
{
    public class UdcModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentUdc Udc;
        public UdcModule()
        {
            unitOfWork = new UnitOfWork();
            Udc = new FluentUdc(unitOfWork);
        }
    }
}
