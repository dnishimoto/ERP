

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{
public interface IServiceInformationInvoiceRepository
    {
        Task<ServiceInformationInvoice> GetEntityById(long ? serviceInformationInvoiceId);
	    Task<ServiceInformationInvoice> FindEntityByExpression(Expression<Func<ServiceInformationInvoice, bool>> predicate);
		Task<IList<ServiceInformationInvoice>> FindEntitiesByExpression(Expression<Func<ServiceInformationInvoice, bool>> predicate);
        Task<IList<ServiceInformationInvoice>> GetEntitiesByServiceId(long? serviceId);
    }
}
