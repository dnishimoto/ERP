using ERP_Core2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IAddressBookQuery
    {
        BuyerView GetBuyerByBuyerId(long buyerId);
        CarrierView GetCarrierByCarrierId(long carrierId);
        SupplierView GetSupplierBySupplierId(long supplierId);
        EmployeeView GetEmployeeByEmployeeId(long employeeId);
        List<EmployeeView> GetEmployeesBySupervisorId(long supervisorId);
        SupervisorView GetSupervisorBySupervisorId(long supervisorId);
        List<Phones> GetPhonesByAddressId(long addressId);
        List<Emails> GetEmailsByAddressId(long addressId);
        List<AddressBookView> GetAddressBookByName(string name);
        AddressBook GetAddressBookByAddressId(long addressId);
        AddressBookView GetAddressBookViewByAddressId(long addressId);
        IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate);
    }
}
