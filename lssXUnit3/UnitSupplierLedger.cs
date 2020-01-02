using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SupplierLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.SupplierLedgerDomain
{

    public class UnitSupplierLedger
    {

        private readonly ITestOutputHelper output;

        public UnitSupplierLedger(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            SupplierLedgerModule SupplierLedgerMod = new SupplierLedgerModule();
            Supplier supplier = await SupplierLedgerMod.Supplier.Query().GetEntityById(3);
            Invoice invoice = await SupplierLedgerMod.Invoice.Query().GetEntityById(2);
            AccountPayable accountPayable = await SupplierLedgerMod.AccountPayable.Query().GetEntityById(2);
            GeneralLedger generalLedger = await SupplierLedgerMod.GeneralLedger.Query().GetEntityById(17);
            AddressBook addressBook = await SupplierLedgerMod.AddressBook.Query().GetEntityById(17);


           SupplierLedgerView view = new SupplierLedgerView()
            {
               SupplierId=supplier.SupplierId,
               InvoiceId=invoice.InvoiceId,
               AcctPayId=accountPayable.AcctPayId,
               Amount=300M,
               Gldate=DateTime.Parse("11/11/2019"),
               AccountId=generalLedger.AccountId,
               GeneralLedgerId=generalLedger.GeneralLedgerId,
               DocNumber=17,
               Comment="back to school",
               AddressId=addressBook.AddressId,
               CreatedDate=DateTime.Parse("11/11/2019"),
               DocType="PV",
               DebitAmount=null,
               CreditAmount=null,
               FiscalYear=2019,
               FiscalPeriod=11
            };
            NextNumber nnNextNumber = await SupplierLedgerMod.SupplierLedger.Query().GetNextNumber();

            view.SupplierLedgerNumber = nnNextNumber.NextNumberValue;

            SupplierLedger supplierLedger = await SupplierLedgerMod.SupplierLedger.Query().MapToEntity(view);

            SupplierLedgerMod.SupplierLedger.AddSupplierLedger(supplierLedger).Apply();

            SupplierLedger newSupplierLedger = await SupplierLedgerMod.SupplierLedger.Query().GetEntityByNumber(view.SupplierLedgerNumber);

            Assert.NotNull(newSupplierLedger);

            newSupplierLedger.Comment = "back to school update";

            SupplierLedgerMod.SupplierLedger.UpdateSupplierLedger(newSupplierLedger).Apply();

            SupplierLedgerView updateView = await SupplierLedgerMod.SupplierLedger.Query().GetViewById(newSupplierLedger.SupplierLedgerId);

            Assert.Same(updateView.Comment, "back to school update");
              SupplierLedgerMod.SupplierLedger.DeleteSupplierLedger(newSupplierLedger).Apply();
            SupplierLedger lookupSupplierLedger= await SupplierLedgerMod.SupplierLedger.Query().GetEntityById(view.SupplierLedgerId);

            Assert.Null(lookupSupplierLedger);
        }
       
      

    }
}
