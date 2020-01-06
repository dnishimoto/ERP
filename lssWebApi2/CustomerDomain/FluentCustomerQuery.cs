using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.CustomerDomain
{
    public class FluentCustomerQuery : MapperAbstract<Customer,CustomerView>, IFluentCustomerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCustomerQuery() { }
        public FluentCustomerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<Customer> MapToEntity(CustomerView inputObject)
        {
         
            Customer outObject = mapper.Map<Customer>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Customer>> MapToEntity(IList<CustomerView> inputObjects)
        {
            IList<Customer> list = new List<Customer>();
         
            foreach (var item in inputObjects)
            {
                Customer outObject = mapper.Map<Customer>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        private async Task<LocationAddressView> MapToLocationAddressView(LocationAddress inputObject)
        {
            Mapper mapper = new Mapper();
            LocationAddressView outObject = mapper.Map<LocationAddressView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        private async Task<IList<LocationAddressView>> MapToLocationAddressViews(IList<LocationAddress> list)
        {
            IList<LocationAddressView> retList = new List<LocationAddressView>();
            foreach (var item in list)
            {
                retList.Add(await MapToLocationAddressView(item));
            }
            return retList;
        }
        public override async Task<CustomerView> MapToView(Customer inputObject)
        {
            CustomerView outObject = mapper.Map<CustomerView>(inputObject);
            await Task.Yield();

            Task<EmailEntity> emailTask = _unitOfWork.emailRepository.GetEntityById(inputObject.PrimaryEmailId ?? 0);
            Task<AddressBook> addressBookTask = _unitOfWork.addressBookRepository.GetEntityByCustomerId(inputObject.CustomerId);

            Task<LocationAddress> shipToLocationAddressTask = _unitOfWork.locationAddressRepository.GetEntityById(3);
            Task<LocationAddress> mailingLocationAddressTask = _unitOfWork.locationAddressRepository.GetEntityById(3);
            Task<LocationAddress> billingLocationAddressTask = _unitOfWork.locationAddressRepository.GetEntityById(3);
            Task<PhoneEntity> phoneTask = _unitOfWork.phoneRepository.GetEntityById(1);


            Task.WaitAll(emailTask, addressBookTask,shipToLocationAddressTask,mailingLocationAddressTask,billingLocationAddressTask,phoneTask );

            outObject.AccountEmail = emailTask.Result.Email;

            outObject.AccountEmailPassword = emailTask.Result.Password;
            outObject.AccountEmailLogin = emailTask.Result.LoginEmail ?? false;
            outObject.CustomerName = addressBookTask.Result.Name;
            outObject.FirstName = addressBookTask.Result.FirstName;
            outObject.LastName = addressBookTask.Result.LastName;

            outObject.ShipToAddressLine1 = shipToLocationAddressTask.Result?.AddressLine1;
            outObject.ShipToAddressLine2 = shipToLocationAddressTask.Result?.AddressLine2;
            outObject.ShipToCity = shipToLocationAddressTask.Result?.City;
            outObject.ShipToZipcode = shipToLocationAddressTask.Result?.Zipcode;

            outObject.MailingAddressLine1 = mailingLocationAddressTask.Result?.AddressLine1;
            outObject.MailingAddressLine2 = mailingLocationAddressTask.Result?.AddressLine2;
            outObject.MailingCity = mailingLocationAddressTask.Result?.City;
            outObject.MailingZipcode = mailingLocationAddressTask.Result?.Zipcode;

            outObject.BillingAddressLine1 = billingLocationAddressTask.Result?.AddressLine1;
            outObject.BillingAddressLine2 = billingLocationAddressTask.Result?.AddressLine2;
            outObject.BillingCity = billingLocationAddressTask.Result?.City;
            outObject.BillingZipcode = billingLocationAddressTask.Result?.Zipcode;
     
             outObject.PhoneNumber = phoneTask.Result.PhoneNumber;
        

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.customerRepository.GetNextNumber(TypeOfCustomer.CustomerNumber.ToString());
        }
        public override async Task<CustomerView> GetViewById(long ? customerId)
        {
            Customer detailItem = await _unitOfWork.customerRepository.GetEntityById(customerId);

            return await MapToView(detailItem);
        }
        public async Task<CustomerView> GetViewByNumber(long customerNumber)
        {
            Customer detailItem = await _unitOfWork.customerRepository.GetEntityByNumber(customerNumber);

            return await MapToView(detailItem);
        }

        public override async Task<Customer> GetEntityById(long ? customerId)
        {
            return await _unitOfWork.customerRepository.GetEntityById(customerId);

        }
        public async Task<Customer> GetEntityByNumber(long customerNumber)
        {
            return await _unitOfWork.customerRepository.GetEntityByNumber(customerNumber);
        }
     
            
}
}
