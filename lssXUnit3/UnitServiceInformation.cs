using lssWebApi2.ContractDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.ServiceInformationInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ServiceInformationDomain
{

    public class UnitServiceInformation
    {

        private readonly ITestOutputHelper output;

        public UnitServiceInformation(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        async Task TestGetInvoices()
        {
            ServiceInformationModule ServiceInformationMod = new ServiceInformationModule();
            IList<ServiceInformationInvoiceView> views = await ServiceInformationMod.ServiceInformationInvoice.Query().GetViewsByServiceId(3);
            Assert.NotNull(views);
        }
        [Fact]
        public async Task TestGetScheduleEvents()
        {
            ServiceInformationModule ServiceInformationMod = new ServiceInformationModule();
            IList<ScheduleEventView> views = await ServiceInformationMod.ScheduleEvent.Query().GetViewsByServiceId(3);
            Assert.NotNull(views);
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ServiceInformationModule ServiceInformationMod = new ServiceInformationModule();
            LocationAddressView locationAddressView = await ServiceInformationMod.LocationAddress.Query().GetViewById(3);
            CustomerView customerView = await ServiceInformationMod.Customer.Query().GetViewById(3);
            AddressBook addressBook = await ServiceInformationMod.AddressBook.Query().GetEntityById(customerView.AddressId);
            ContractView contractView = await ServiceInformationMod.Contract.Query().GetViewById(1);
            Udc udc = await ServiceInformationMod.Udc.Query().GetEntityById(2);


            ServiceInformationView view = new ServiceInformationView()
            {
                ServiceDescription = " truck oil change",
                Price = 80.76M,
                AddOns = "none",
                ServiceTypeXrefId = udc.XrefId,
                ServiceType = udc.Value,
                CreatedDate = DateTime.Parse("12/25/2019"),
                LocationId = locationAddressView.LocationAddressId,
                CustomerId = customerView.CustomerId,
                ContractId = contractView.ContractId,
                vwCustomer = customerView,
                vwContract = contractView,
                vwLocationAddress = locationAddressView,
                SquareFeetOfStructure = 100,
                LocationDescription = "Eiensten brothers on orchard",
                LocationGps = "",
                Comments = "",
                Status = true

            };
            NextNumber nnNextNumber = await ServiceInformationMod.ServiceInformation.Query().GetNextNumber();

            view.ServiceInformationNumber = nnNextNumber.NextNumberValue;

            ServiceInformation serviceInformation = await ServiceInformationMod.ServiceInformation.Query().MapToEntity(view);

            ServiceInformationMod.ServiceInformation.AddServiceInformation(serviceInformation).Apply();

            ServiceInformation newServiceInformation = await ServiceInformationMod.ServiceInformation.Query().GetEntityByNumber(view.ServiceInformationNumber);

            Assert.NotNull(newServiceInformation);

            newServiceInformation.ServiceDescription = "ServiceInformation Test Update";

            ServiceInformationMod.ServiceInformation.UpdateServiceInformation(newServiceInformation).Apply();

            ServiceInformationView updateView = await ServiceInformationMod.ServiceInformation.Query().GetViewById(newServiceInformation.ServiceId);

            Assert.Same(updateView.ServiceDescription, "ServiceInformation Test Update");
            ServiceInformationMod.ServiceInformation.DeleteServiceInformation(newServiceInformation).Apply();
            ServiceInformation lookupServiceInformation = await ServiceInformationMod.ServiceInformation.Query().GetEntityById(view.ServiceId);

            Assert.Null(lookupServiceInformation);
        }



    }
}
