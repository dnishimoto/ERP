using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentSupplierLedgerQuery
{
        Task<SupplierLedger> MapToEntity(SupplierLedgerView inputObject);
        Task<IList<SupplierLedger>> MapToEntity(IList<SupplierLedgerView> inputObjects);
    
        Task<SupplierLedgerView> MapToView(SupplierLedger inputObject);
        Task<NextNumber> GetNextNumber();
	Task<SupplierLedger> GetEntityById(long ? supplierLedgerId);
	  Task<SupplierLedger> GetEntityByNumber(long supplierLedgerNumber);
	Task<SupplierLedgerView> GetViewById(long ? supplierLedgerId);
	Task<SupplierLedgerView> GetViewByNumber(long supplierLedgerNumber);
    Task<SupplierLedgerView> GetSupplierLedgerByDocNumber(long? docNumber, string docType);
}
