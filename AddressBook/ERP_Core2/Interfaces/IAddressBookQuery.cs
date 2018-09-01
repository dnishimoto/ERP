using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        List<Phone> GetPhonesByAddressId(long addressId);
        List<Email> GetEmailsByAddressId(long addressId);
        List<AddressBook> GetAddressBookByName(string namePattern);
        AddressBook GetAddressBookByAddressId(long addressId);
        IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate);
    }
}
