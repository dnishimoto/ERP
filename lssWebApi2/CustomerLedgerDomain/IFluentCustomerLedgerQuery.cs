using lssWebApi2.AutoMapper;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentCustomerLedgerQuery
{
    Task<CustomerLedger> MapToEntity(CustomerLedgerView inputObject);
    Task<IList<CustomerLedger>> MapToEntity(IList<CustomerLedgerView> inputObjects);
    Task<CustomerLedgerView> MapToView(CustomerLedger inputObject);
    Task<NextNumber> GetNextNumber();
    Task<CustomerLedger> GetEntityById(long ? customerLedgerId);
    Task<CustomerLedger> GetEntityByNumber(long customerLedgerNumber);
    Task<CustomerLedgerView> GetViewById(long ? customerLedgerId);
    Task<CustomerLedgerView> GetViewByNumber(long customerLedgerNumber);
    Task<IList<CustomerLedgerView>> GetViewsByCustomerId(long customerId);
}
