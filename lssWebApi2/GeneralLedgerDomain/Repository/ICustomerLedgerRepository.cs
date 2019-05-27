using ERP_Core2.AccountPayableDomain;
using ERP_Core2.CustomerLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.GeneralLedgerDomain.Repository
{
    public interface ICustomerLedgerRepository
    {
       Task<CreateProcessStatus> CreateLedgerFromView(CustomerLedgerView view);
       Task<CreateProcessStatus> UpdateCustomerLedger(CustomerLedger CustomerLedger);
        CreateProcessStatus DeleteCustomerLedger(CustomerLedger CustomerLedger);
    }
}
