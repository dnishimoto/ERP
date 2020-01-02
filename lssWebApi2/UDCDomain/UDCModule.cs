using lssWebApi2.AbstractFactory;
using lssWebApi2.UDCDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.UDCDomain
{
    public class UdcModule : AbstractModule
    {
        public FluentUdc Udc = new FluentUdc();
    }
}
