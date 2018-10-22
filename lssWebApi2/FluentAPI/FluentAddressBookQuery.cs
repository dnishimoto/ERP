using ERP_Core2.AbstractFactory;
using ERP_Core2.Interfaces;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.FluentAPI
{
    public class FluentAddressBookQuery : AbstractModule, IAddressBookQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentAddressBookQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }


        public BuyerView GetBuyerByBuyerId(long buyerId)
        {
            try
            {
                Task<BuyerView> resultTask = Task.Run(async() => await _unitOfWork.buyerRepository.GetBuyerViewByBuyerId(buyerId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public CarrierView GetCarrierByCarrierId(long carrierId)
        {
            try
            {
                Task<CarrierView> resultTask = Task.Run(async() => await _unitOfWork.carrierRepository.GetCarrierViewByCarrierId(carrierId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public SupplierView GetSupplierBySupplierId(long supplierId)
        {

            try
            {
                Task<SupplierView> resultTask = Task.Run(async () => await _unitOfWork.supplierRepository.GetSupplierViewBySupplierId(supplierId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public EmployeeView GetEmployeeByEmployeeId(long employeeId)
        {
            try
            {
                Task<EmployeeView> resultTask = Task.Run(async() => await _unitOfWork.employeeRepository.GetEmployeeViewByEmployeeId(employeeId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }


        }
        public List<EmployeeView> GetEmployeesBySupervisorId(long supervisorId)
        {
            try
            {
                Task<List<EmployeeView>> resultTask = Task.Run(() => _unitOfWork.supervisorRepository.GetEmployeesBySupervisorId(supervisorId));
                Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public SupervisorView GetSupervisorBySupervisorId(long supervisorId)
        {
            try
            {
                Task<SupervisorView> resultTask = Task.Run(async() => await _unitOfWork.supervisorRepository.GetSupervisorBySupervisorId(supervisorId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public List<Phones> GetPhonesByAddressId(long addressId)
        {
            try
            {
                Task<List<Phones>> resultTask = Task.Run(() => _unitOfWork.addressBookRepository.GetPhonesByAddressId(addressId));
                Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);

            }
        }
        public List<Emails> GetEmailsByAddressId(long addressId)
        {
            try
            {
                Task<List<Emails>> resultTask = Task.Run(() => _unitOfWork.addressBookRepository.GetEmailsByAddressId(addressId));
                Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public List<AddressBook> GetAddressBookByName(string namePattern)
        {
            try
            {
                Task<List<AddressBook>> resultTask = Task.Run(async() => await _unitOfWork.addressBookRepository.GetAddressBookByName(namePattern));
                //Task.WaitAll(resultTask);

                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public AddressBook GetAddressBookByAddressId(long addressId)
        {
            try
            {
                Task<AddressBook> resultTask = Task.Run(async() => await _unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId));
                //Task.WaitAll(resultTask);
                return resultTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

        public IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate)
        {
            try
            {
                //queryableAddressBook = 
                return _unitOfWork.addressBookRepository.GetObjectsQueryable(predicate) as IQueryable<AddressBook>;
                //return this as IAddressBookQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
