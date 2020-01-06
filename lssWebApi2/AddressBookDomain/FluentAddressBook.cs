using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.CustomerDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAddressBook : AbstractModule, IFluentAddressBook
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentAddressBook() { }

        public FluentAddressBookQuery _query;

        public IFluentAddressBookQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentAddressBookQuery(unitOfWork);
            }
            return _query as IFluentAddressBookQuery;
        }
        public IFluentAddressBook CreateSupplierAddressBook(AddressBook addressBook, EmailEntity email)

        {

            Task<Udc> udcTask = Task.Run(async()=>await unitOfWork.udcRepository.GetUdc("AB_Type", "Supplier"));
            Task<AddressBook> addressBookLookupTask = Task.Run(async () => await unitOfWork.addressBookRepository.FindEntityByAddressIdAndEmail(addressBook.AddressId,email.Email));
            Task.WaitAll(udcTask, addressBookLookupTask);

            if (udcTask.Result != null){addressBook.PeopleXrefId = udcTask.Result.XrefId; }

            if (addressBookLookupTask.Result == null)
            {
                AddAddressBook(addressBook);
                return this as IFluentAddressBook;
            }
            processStatus=CreateProcessStatus.AlreadyExists;
            return this as IFluentAddressBook;
        }

        public IFluentAddressBook AddAddressBook(CustomerView customerView)
        {
            try
            {
                Task<AddressBook> lookupAddressBookTask = Task.Run(async()=>await unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView));
                Task.WaitAll(lookupAddressBookTask);
                if (lookupAddressBookTask.Result == null)
                {
                    AddressBook addressBook = new AddressBook();

                    
                    applicationViewFactory.MapAddressBookEntity(ref addressBook, customerView);
                    AddAddressBook(addressBook);

                    return this as IFluentAddressBook;
                }

                processStatus=CreateProcessStatus.AlreadyExists;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentAddressBook Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAddressBook;
        }
        //public IFluentAddressBook MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView)
        //{
        // unitOfWork.addressBookRepository.MapAddressBookEntity(ref addressBook, addressBookView);
        // return this as IFluentAddressBook;
        //}
        private async Task<AddressBook> MapToEntity(AddressBookView inputObject)
        {
            Mapper mapper = new Mapper();
            AddressBook outObject = mapper.Map<AddressBook>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public IFluentAddressBook UpdateAddressBookByView(AddressBookView view)
        {
            Task<AddressBook> addressBookTask = Task.Run(async () => await MapToEntity(view));
            Task.WaitAll(addressBookTask);
            AddAddressBook(addressBookTask.Result);
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
        public IFluentAddressBook AddAddressBook(AddressBook addressBook)
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
                unitOfWork.addressBookRepository.DeleteObject(addressBook);
                processStatus = CreateProcessStatus.Delete;
                return this as IFluentAddressBook;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentAddressBook AddAddressBooks(List<AddressBook> list)
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
        public IFluentAddressBook UpdateAddressBooks(IList<AddressBook> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.addressBookRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAddressBook;
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
