using lssWebApi2.SupplierDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentSupplierQuery
{
    Task<Supplier> MapToEntity(SupplierView inputObject);
    Task<IList<Supplier>> MapToEntity(IList<SupplierView> inputObjects);
    Task<SupplierView> MapToView(Supplier inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Supplier> GetEntityById(long ? supplierId);
    Task<Supplier> GetEntityByNumber(long supplierNumber);
    Task<SupplierView> GetViewById(long ? supplierId);
    Task<SupplierView> GetViewByNumber(long supplierNumber);
    Task<SupplierView> GetSupplierBySupplierId(long supplierId);
    Task<SupplierView> GetViewByEmail(EmailEntity email);
}
