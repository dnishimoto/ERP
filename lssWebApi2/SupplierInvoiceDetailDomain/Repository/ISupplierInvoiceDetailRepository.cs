

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{
public interface ISupplierInvoiceDetailRepository
    {
        Task<SupplierInvoiceDetail> GetEntityById(long ? supplierInvoiceDetailId);
	    Task<IList<SupplierInvoiceDetail>> getEntitiesByInvoiceId(long? supplierInvoiceId);
    }
}
