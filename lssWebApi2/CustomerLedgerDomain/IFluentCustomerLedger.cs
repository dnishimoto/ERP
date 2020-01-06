

using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.CustomerLedgerDomain
{ 

public interface IFluentCustomerLedger
    {
        IFluentCustomerLedgerQuery Query();
        IFluentCustomerLedger Apply();
        IFluentCustomerLedger AddCustomerLedger(CustomerLedger customerLedger);
        IFluentCustomerLedger UpdateCustomerLedger(CustomerLedger customerLedger);
        IFluentCustomerLedger DeleteCustomerLedger(CustomerLedger customerLedger);
     	IFluentCustomerLedger UpdateCustomerLedgers(IList<CustomerLedger> newObjects);
        IFluentCustomerLedger AddCustomerLedgers(List<CustomerLedger> newObjects);
        IFluentCustomerLedger DeleteCustomerLedgers(List<CustomerLedger> deleteObjects);
        IFluentCustomerLedger CreateEntityByGeneralLedgerView(GeneralLedgerView ledgerView);
    }
}
