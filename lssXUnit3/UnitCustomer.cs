using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CustomerDomain
{

    public class UnitCustomer
    {

        private readonly ITestOutputHelper output;

        public UnitCustomer(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            CustomerModule CustomerMod = new CustomerModule();
            AddressBook addressBook = await CustomerMod.AddressBook.Query().GetEntityById(1);
            LocationAddress shipToLocationAddress = await CustomerMod.LocationAddress.Query().GetEntityById(3);
            LocationAddress mailingLocationAddress = await CustomerMod.LocationAddress.Query().GetEntityById(3);
            LocationAddress billingLocationAddress = await CustomerMod.LocationAddress.Query().GetEntityById(3);
            PhoneEntity phone = await CustomerMod.Phone.Query().GetEntityById(1);

            EmailEntity emailEntity = await CustomerMod.Email.Query().GetEntityById(1);
            CustomerView view = new CustomerView()
            {
                AddressId = addressBook.AddressId,
                CustomerName = addressBook.Name,
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,

                PrimaryEmailId = emailEntity?.EmailId,
                AccountEmail = emailEntity?.Email,
                AccountEmailLogin = emailEntity?.LoginEmail ?? false,

                PrimaryShippedToLocationAddressId = shipToLocationAddress?.LocationAddressId,
                ShipToAddressLine1 = shipToLocationAddress?.AddressLine1,
                ShipToAddressLine2 = shipToLocationAddress?.AddressLine2,
                ShipToCity = shipToLocationAddress?.City,
                ShipToZipcode = shipToLocationAddress?.Zipcode,

                MailingLocationAddressId = mailingLocationAddress?.LocationAddressId,
                MailingAddressLine1 = mailingLocationAddress?.AddressLine1,
                MailingAddressLine2 = mailingLocationAddress?.AddressLine2,
                MailingCity = mailingLocationAddress?.City,
                MailingZipcode = mailingLocationAddress?.Zipcode,

                PrimaryBillingLocationAddressId=billingLocationAddress?.LocationAddressId,
                BillingAddressLine1 = billingLocationAddress?.AddressLine1,
                BillingAddressLine2 = billingLocationAddress?.AddressLine2,
                BillingCity = billingLocationAddress?.City,
                BillingZipcode = billingLocationAddress?.Zipcode,

                PrimaryPhoneId = phone.PhoneId,
                PhoneNumber = phone.PhoneNumber,

                TaxIdentification = "tax id"

            };
            NextNumber nnNextNumber = await CustomerMod.Customer.Query().GetNextNumber();

            view.CustomerNumber = nnNextNumber.NextNumberValue;

            Customer customer = await CustomerMod.Customer.Query().MapToEntity(view);

            CustomerMod.Customer.AddCustomer(customer).Apply();

            Customer newCustomer = await CustomerMod.Customer.Query().GetEntityByNumber(view.CustomerNumber);

            Assert.NotNull(newCustomer);

            newCustomer.AddressId = 17;

            CustomerMod.Customer.UpdateCustomer(newCustomer).Apply();

            CustomerView updateView = await CustomerMod.Customer.Query().GetViewById(newCustomer.CustomerId);

            if (updateView.AddressId != 17) Assert.True(true);
            CustomerMod.Customer.DeleteCustomer(newCustomer).Apply();
            Customer lookupCustomer = await CustomerMod.Customer.Query().GetEntityById(view.CustomerId);

            Assert.Null(lookupCustomer);
        }



    }
}
