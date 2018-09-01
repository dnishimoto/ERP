using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.Interfaces;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentAddressBook : AbstractModule, IAddressBook
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        public FluentAddressBook() { }

        public FluentAddressBookQuery _query;
        //= new FluentAddressBookQuery();

        public IAddressBookQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentAddressBookQuery(unitOfWork);
            }
            return _query as IAddressBookQuery;
        }
        public IAddressBook Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted || processStatus == CreateProcessStatus.Updated || processStatus == CreateProcessStatus.Deleted)
            { unitOfWork.CommitChanges(); }
            return this as IAddressBook;
        }

        public IAddressBook UpdateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.UpdateObject(addressBook);
                processStatus = CreateProcessStatus.Updated;
                return this as IAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public IAddressBook CreateAddressBook(AddressBook addressBook)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObject(addressBook);
                processStatus = CreateProcessStatus.Inserted;
                return this as IAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public IAddressBook DeleteAddressBook(AddressBook addressBook)
        {
            try
            {
                //Task<AddressBook> lookupTask = Task.Run(()=>unitOfWork.addressBookRepository.GetObjectAsync(addressBook.AddressId));
                //Task.WaitAll(lookupTask);

                unitOfWork.addressBookRepository.DeleteObject(addressBook);
                processStatus = CreateProcessStatus.Deleted;
                return this as IAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IAddressBook CreateAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.AddObjects(list);
                processStatus = CreateProcessStatus.Updated;
                return this as IAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IAddressBook DeleteAddressBooks(List<AddressBook> list)
        {
            try
            {
                unitOfWork.addressBookRepository.DeleteObjects(list);
                processStatus = CreateProcessStatus.Deleted;
                return this as IAddressBook;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }


        }


    }
}
