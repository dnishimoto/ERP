using ERP_Core2.AccountPayableDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.SupplierInvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDomain.Repository
{
    public interface IPackingSlipRepository
    {
        Task<PackingSlipView> GetPackingSlipViewBySlipDocument(string slipDocument);
        Task<CreateProcessStatus> CreatePackingSlipDetailsByView(PackingSlipView view);
        Task<CreateProcessStatus> CreatePackingSlipByView(PackingSlipView view);
       Task<IList<PackingSlip>> GetPackingSlipsByDocNumber(string PONumber);
    }
}
