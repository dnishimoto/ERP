﻿using ERP_Core2.AbstractFactory;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using System.Linq.Expressions;

namespace ERP_Core2.AddressBookDomain
{
    class AddressBookModule : AbstractModule
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<BuyerView> GetBuyerByBuyerId(long buyerId)
        {
            try
            {
                BuyerView buyerView = await unitOfWork.buyerRepository.GetBuyerViewByBuyerId(buyerId);
                return buyerView;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public async Task<CarrierView> GetCarrierByCarrierId(long carrierId)
        {
            try
            {
                CarrierView carrierView = await unitOfWork.carrierRepository.GetCarrierViewByCarrierId(carrierId);
                return carrierView;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public async Task<SupplierView> GetSupplierBySupplierId(long supplierId)
        {

            try
            {
                SupplierView supplierView = await unitOfWork.supplierRepository.GetSupplierViewBySupplierId(supplierId);
                return supplierView;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<EmployeeView> GetEmployeeByEmployeeId(long employeeId)
        {
            try
            {
                EmployeeView employeeView = await unitOfWork.employeeRepository.GetEmployeeViewByEmployeeId(employeeId);
                return employeeView;
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
                List<EmployeeView> list = unitOfWork.supervisorRepository.GetEmployeesBySupervisorId(supervisorId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<SupervisorView> GetSupervisorBySupervisorId(long supervisorId)
        {
            try
            {
                SupervisorView view = await unitOfWork.supervisorRepository.GetSupervisorBySupervisorId(supervisorId);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public List<Phone> GetPhonesByAddressId(long addressId)
        {
            try
            {
                List<Phone> list = unitOfWork.addressBookRepository.GetPhonesByAddressId(addressId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);

            }
        }
        public List<Email> GetEmailsByAddressId(long addressId)
        {
            try
            {
                List<Email> list =  unitOfWork.addressBookRepository.GetEmailsByAddressId(addressId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<List<AddressBook>> GetAddressBookByName(string namePattern)
        {
            try
            {
                List<AddressBook> list = await unitOfWork.addressBookRepository.GetAddressBookByName(namePattern);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<AddressBook> GetAddressBookByAddressId(long addressId)
        {
            try
            {
                AddressBook ab = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
                return ab;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool UpdateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.UpdateObject(addressBook);
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool CreateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObject(addressBook);
                unitOfWork.CommitChanges();
                return true;
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
                return  unitOfWork.addressBookRepository.GetObjectsQueryable(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.DeleteObject(addressBook);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public bool CreateAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObjects(list);
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public bool DeleteAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.DeleteObjects(list);
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }


        }
    }
}