using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ContractInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ContractInvoiceDomain
{

    public class UnitContractInvoice
    {

        private readonly ITestOutputHelper output;

        public UnitContractInvoice(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ContractInvoiceModule ContractInvoiceMod = new ContractInvoiceModule();
            Contract contract = await ContractInvoiceMod.Contract.Query().GetEntityById(1);
            Invoice invoice = await ContractInvoiceMod.Invoice.Query().GetEntityById(5);

           ContractInvoiceView view = new ContractInvoiceView()
            {
                   ContractId=contract.ContractId,
                   InvoiceId=invoice.InvoiceId

            };
            NextNumber nnNextNumber = await ContractInvoiceMod.ContractInvoice.Query().GetNextNumber();

            view.ContractInvoiceNumber = nnNextNumber.NextNumberValue;

            ContractInvoice contractInvoice = await ContractInvoiceMod.ContractInvoice.Query().MapToEntity(view);

            ContractInvoiceMod.ContractInvoice.AddContractInvoice(contractInvoice).Apply();

            ContractInvoice newContractInvoice = await ContractInvoiceMod.ContractInvoice.Query().GetEntityByNumber(view.ContractInvoiceNumber);

            Assert.NotNull(newContractInvoice);

            newContractInvoice.InvoiceId=8;

            ContractInvoiceMod.ContractInvoice.UpdateContractInvoice(newContractInvoice).Apply();

            ContractInvoiceView updateView = await ContractInvoiceMod.ContractInvoice.Query().GetViewById(newContractInvoice.ContractInvoiceId);

           if(updateView.InvoiceId!=8) Assert.True(false);
              ContractInvoiceMod.ContractInvoice.DeleteContractInvoice(newContractInvoice).Apply();
            ContractInvoice lookupContractInvoice= await ContractInvoiceMod.ContractInvoice.Query().GetEntityById(view.ContractInvoiceId);

            Assert.Null(lookupContractInvoice);
        }
       
      

    }
}
