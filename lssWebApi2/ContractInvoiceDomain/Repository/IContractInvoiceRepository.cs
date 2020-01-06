

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.ContractInvoiceDomain
{
public interface IContractInvoiceRepository
    {
        Task<ContractInvoice> GetEntityById(long ? contractInvoiceId);
	    Task<ContractInvoice> FindEntityByExpression(Expression<Func<ContractInvoice, bool>> predicate);
		Task<IList<ContractInvoice>> FindEntitiesByExpression(Expression<Func<ContractInvoice, bool>> predicate);
		IQueryable<ContractInvoice> GetEntitiesByExpression(Expression<Func<ContractInvoice, bool>> predicate);
    }
}
