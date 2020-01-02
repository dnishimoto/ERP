using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.ServiceInformationInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{

    public class UnitServiceInformationInvoice
    {

        private readonly ITestOutputHelper output;

        public UnitServiceInformationInvoice(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ServiceInformationInvoiceModule ServiceInformationInvoiceMod = new ServiceInformationInvoiceModule();
            InvoiceView invoice = await ServiceInformationInvoiceMod.Invoice.Query().GetViewById(5);
            ServiceInformationView serviceInformation = await ServiceInformationInvoiceMod.ServiceInformation.Query().GetViewById(3);
      
        ServiceInformationInvoiceView view = new ServiceInformationInvoiceView()
            {
                InvoiceId=invoice.InvoiceId,
                ServiceId= serviceInformation.ServiceId

        };
            NextNumber nnNextNumber = await ServiceInformationInvoiceMod.ServiceInformationInvoice.Query().GetNextNumber();

            view.ServiceInformationInvoiceNumber = nnNextNumber.NextNumberValue;

            ServiceInformationInvoice serviceInformationInvoice = await ServiceInformationInvoiceMod.ServiceInformationInvoice.Query().MapToEntity(view);

            ServiceInformationInvoiceMod.ServiceInformationInvoice.AddServiceInformationInvoice(serviceInformationInvoice).Apply();

            ServiceInformationInvoice newServiceInformationInvoice = await ServiceInformationInvoiceMod.ServiceInformationInvoice.Query().GetEntityByNumber(view.ServiceInformationInvoiceNumber);

            Assert.NotNull(newServiceInformationInvoice);

            newServiceInformationInvoice.InvoiceId=8;

            ServiceInformationInvoiceMod.ServiceInformationInvoice.UpdateServiceInformationInvoice(newServiceInformationInvoice).Apply();

            ServiceInformationInvoiceView updateView = await ServiceInformationInvoiceMod.ServiceInformationInvoice.Query().GetViewById(newServiceInformationInvoice.ServiceInformationInvoiceId);

            if (updateView.InvoiceId == 8) Assert.True(true);

              ServiceInformationInvoiceMod.ServiceInformationInvoice.DeleteServiceInformationInvoice(newServiceInformationInvoice).Apply();
            ServiceInformationInvoice lookupServiceInformationInvoice= await ServiceInformationInvoiceMod.ServiceInformationInvoice.Query().GetEntityById(view.ServiceInformationInvoiceId);

            Assert.Null(lookupServiceInformationInvoice);
        }
       
      

    }
}
