

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.InvoiceDetailDomain
{
public interface IInvoiceDetailRepository
    {
        Task<InvoiceDetail> GetEntityById(long ? invoiceDetailId);
	    Task<InvoiceDetail> FindEntityByExpression(Expression<Func<InvoiceDetail, bool>> predicate);
        Task<IList<InvoiceDetail>> GetEntitiesByInvoiceId(long? invoiceId);
    }
}
