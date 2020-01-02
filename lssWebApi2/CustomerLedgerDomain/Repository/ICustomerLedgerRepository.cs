using lssWebApi2.AccountPayableDomain;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.GeneralLedgerDomain.Repository
{
    public interface ICustomerLedgerRepository
    {
        Task<IList<CustomerLedger>> GetCustomerLedgersByCustomerId(long customerId);
        Task<CustomerLedger> GetEntityById(long ? customerLedgerId);
        Task<CustomerLedger> GetEntityByNumber(long customerLedgerNumber);
        Task<IList<CustomerLedger>> GetEntitiesByCustomerId(long customerId);
        Task<CustomerLedger> FindEntityByGeneralLedgerId(long? generalLedgerId);
        Task<CustomerLedger> GetCustomerLedgerByDocNumber(long? docNumber, string docType);
    }
}
