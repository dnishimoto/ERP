

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.SupplierInvoiceDomain
{
public interface ISupplierInvoiceRepository
    {
        Task<SupplierInvoice> GetEntityById(long ? supplierInvoiceId);
        Task<SupplierInvoice> GetEntityByPONumber(string poNumber);
        Task<SupplierInvoice> GetEntityByNumber(long? supplierInvoiceNumber);
    }
}
