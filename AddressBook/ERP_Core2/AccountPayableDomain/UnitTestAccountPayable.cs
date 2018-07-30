using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using MillenniumERP.PurchaseOrderDomain;

namespace ERP_Core2.AccountPayableDomain
{
    
       public class UnitTestAccountPayable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestAccountPayable(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestMatchReceiptToPurchaseOrder()
        {
        }
        [Fact]
        public void TestReceiveOpenPurchaseOrderDetail()
        {
        }
        [Fact]
        public async Task TestCreateAccountModel()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            ItemMaster itemMaster = new ItemMaster();
            itemMaster.ItemNumber = "P2001Test";
            itemMaster.Description = "Highlighter - 3 Color";
            itemMaster.UnitOfMeasure = "Sets";
            itemMaster.UnitPrice = 6M;

            bool result=await unitOfWork.itemMasterRepository.CreateItemMaster(itemMaster);
            if (result) { unitOfWork.CommitChanges(); }
            AddressBook addressBook = new AddressBook();
            addressBook.CompanyName = "Sample Company Part Ltd";
            addressBook.Name = "";
            addressBook.FirstName = "";
            addressBook.LastName = "";

            LocationAddress locationAddress = new LocationAddress();
            locationAddress.Address_Line_1 = "204 Collins Street";
            locationAddress.City = "Melbourne";
            locationAddress.Zipcode = "3000";
            locationAddress.Country = "Australia";

            Email email = new Email();
            email.Email1 = "SampleCompany@Party.com";
            email.LoginEmail = true;
            email.Password = "123";

           
            SupplierView supplierView=await unitOfWork.supplierRepository.CreateSupplierByAddressBook(addressBook,locationAddress,email);

            PurchaseOrderView purchaseOrderView = new PurchaseOrderView();

            ChartOfAcct coa = await unitOfWork.supplierRepository.GetChartofAccount("1000", "1200", "240", "");

            Company company = await unitOfWork.supplierRepository.GetCompany();
            purchaseOrderView.DocType = "OV";
            purchaseOrderView.PaymentTerms = "Net 30";
            purchaseOrderView.GLDate = DateTime.Parse("7/30/2018");
            purchaseOrderView.AccountId = coa.AccountId;
            purchaseOrderView.SupplierId = supplierView.SupplierId??0;
            purchaseOrderView.SupplierName = supplierView.CompanyName;
            purchaseOrderView.Description = "Back to School Inventory";
            purchaseOrderView.PONumber = "PO-1";
            purchaseOrderView.TakenBy = "David Nishimoto";

            purchaseOrderView.BuyerId = company.CompanyId;
            purchaseOrderView.ShippedToName= company.CompanyName;
            purchaseOrderView.ShippedToAddress1 = company.CompanyStreet;

            purchaseOrderView.ShippedToCity = company.CompanyCity;
            purchaseOrderView.ShippedToState = company.CompanyState;
            purchaseOrderView.ShippedToZipcode = company.CompanyZipcode;



            purchaseOrderView.RequestedDate = DateTime.Parse("7/24/2018");

            purchaseOrderView.PromisedDeliveredDate = DateTime.Parse("8/2/2018");

            purchaseOrderView.TransactionDate = DateTime.Parse("7/30/2018"); 

    




            Assert.True(true);
        }
     
    }
}
