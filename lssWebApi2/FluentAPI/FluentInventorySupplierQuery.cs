using ERP_Core2.AbstractFactory;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInventorySupplierQuery : AbstractErrorHandling, IInventorySupplierQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentInventorySupplierQuery() { }
        public FluentInventorySupplierQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public AddressBook GetAddressBookbyEmail(Emails email)
        {
            AddressBook ab = _unitOfWork.supplierRepository.GetAddressBookByEmail(email);

            return ab;
        }
        public SupplierView GetSupplierViewByEmail(Emails email)
        {
            Task<SupplierView> resultTask = Task.Run(async () => await _unitOfWork.supplierRepository.GetSupplierViewByEmail(email));
            //Task.WaitAll(resultTask);
            return resultTask.Result;
        }
    }
}
