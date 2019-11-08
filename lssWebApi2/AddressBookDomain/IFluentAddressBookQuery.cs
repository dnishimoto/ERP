using ERP_Core2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentAddressBookQuery
    {
        BuyerView GetBuyerByBuyerId(long buyerId);
        CarrierView GetCarrierByCarrierId(long carrierId);
        SupplierView GetSupplierBySupplierId(long supplierId);
    
        SupervisorView GetSupervisorBySupervisorId(long supervisorId);
        List<Phones> GetPhonesByAddressId(long addressId);
        List<Emails> GetEmailsByAddressId(long addressId);
        List<AddressBookView> GetAddressBookByName(string name);
        AddressBook GetEntityById(long addressId);
        AddressBookView GetViewById(long addressId);
        IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate);
    }
}
