using ERP_Core2.GeneralLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ICustomerLedger
    {
        ICustomerLedger Apply();
        ICustomerLedger CreateCustomerLedger(GeneralLedgerView ledgerView);

    }
}
