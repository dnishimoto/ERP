using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;

namespace ERP_Core2.FluentAPI
{
    public class FluentAddressBook : AbstractModule, IFluentAddressBook
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        public FluentAddressBook() { }

        public FluentAddressBookQuery _query;
        //= new FluentAddressBookQuery();

        public IFluentAddressBookQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentAddressBookQuery(unitOfWork);
            }
            return _query as IFluentAddressBookQuery;
        }
        public IFluentAddressBook Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAddressBook;
        }
        public IFluentAddressBook MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView)
        {
            unitOfWork.addressBookRepository.MapAddressBookEntity(ref addressBook, addressBookView);
            return this as IFluentAddressBook;
        }
        public IFluentAddressBook UpdateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.UpdateObject(addressBook);
                processStatus = CreateProcessStatus.Update;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public IFluentAddressBook CreateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObject(addressBook);
                processStatus = CreateProcessStatus.Insert;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public IFluentAddressBook DeleteAddressBook(AddressBook addressBook)
        {
            try
            {
                //Task<AddressBook> lookupTask = Task.Run(()=>unitOfWork.addressBookRepository.GetObjectAsync(addressBook.AddressId));
                //Task.WaitAll(lookupTask);

                unitOfWork.addressBookRepository.DeleteObject(addressBook);
                processStatus = CreateProcessStatus.Delete;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentAddressBook CreateAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObjects(list);
                processStatus = CreateProcessStatus.Update;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentAddressBook DeleteAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.DeleteObjects(list);
                processStatus = CreateProcessStatus.Delete;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }


        }


    }
}
