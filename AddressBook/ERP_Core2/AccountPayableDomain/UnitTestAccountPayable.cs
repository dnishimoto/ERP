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

            bool result = await unitOfWork.itemMasterRepository.CreateItemMaster(itemMaster);
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


            SupplierView supplierView = await unitOfWork.supplierRepository.CreateSupplierByAddressBook(addressBook, locationAddress, email);

         
            ChartOfAcct coa = await unitOfWork.supplierRepository.GetChartofAccount("1000", "1200", "240", "");

            Company company = await unitOfWork.supplierRepository.GetCompany();

            ItemMaster[] itemMasterLookup=new ItemMaster[5];

            itemMasterLookup[0] = await unitOfWork.itemMasterRepository.GetObjectAsync(5);
            itemMasterLookup[1] = await unitOfWork.itemMasterRepository.GetObjectAsync(6);
            itemMasterLookup[2] = await unitOfWork.itemMasterRepository.GetObjectAsync(7);
            itemMasterLookup[3] = await unitOfWork.itemMasterRepository.GetObjectAsync(8);
            itemMasterLookup[4] = await unitOfWork.itemMasterRepository.GetObjectAsync(9);

            UDC udcAcctPayDocType = await unitOfWork.accountPayableRepository.GetUdc("AcctPayDocType", "STD");

            string json = @"{
            ""DocType"" : """ + udcAcctPayDocType.KeyCode + @""",
            ""PaymentTerms"" : ""Net 30"",
            ""GLDate"" : """ + DateTime.Parse("7/30/2018") + @""",
            ""AccountId"" :"  + coa.AccountId + @",
            ""SupplierId"" :" + (supplierView.SupplierId ?? 0).ToString() +@",
            ""SupplierName"" :""" + supplierView.CompanyName +@""",
            ""Description"" :""Back to School Inventory"",
            ""PONumber"" :""PO -1"",
            ""TakenBy"" : ""David Nishimoto"",
            ""BuyerId"" :"  + company.CompanyId +@",
            ""TaxCode1"" :""" + company.TaxCode1 +@""",
            ""TaxCode2"" :""" + company.TaxCode2 +@""",
            ""ShippedToName"" :"""+ company.CompanyName + @""",
            ""ShippedToAddress1"" :""" + company.CompanyStreet + @""",
            ""ShippedToCity"" :""" + company.CompanyCity + @""",
            ""ShippedToState"" :""" + company.CompanyState + @""",
            ""ShippedToZipcode"" :""" + company.CompanyZipcode + @""",
            ""RequestedDate"" :""" + DateTime.Parse("7/24/2018") + @""",
            ""PromisedDeliveredDate"" :""" + DateTime.Parse("8/2/2018") + @""",
            ""TransactionDate"" :""" + DateTime.Parse("7/30/2018")+ @""", 

            ""PurchaseOrderDetailViews"":[
                    {
                    ""ItemId"": 5,
                    ""OrderDate"":"""+ DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 5,
                    ""UnitPrice"" : " + itemMasterLookup[0].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[0].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[0].UnitPrice*5 + @",
                    ""Description"": """+itemMasterLookup[0].Description + @""",
                    ""ExpectedDeliveryDate"" :""" + DateTime.Parse("8/2/2018") + @""",
                    ""ReceivedQuantity"":0,
                    ""RemainingQuantity"":5
                    },
                    {
                    ""ItemId"": 6,
                    ""OrderDate"":""" + DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 4,
                    ""UnitPrice"" : " + itemMasterLookup[1].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[1].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[1].UnitPrice * 4 + @",
                    ""Description"": """ + itemMasterLookup[1].Description + @""",
                    ""ExpectedDeliveryDate"" :""" + DateTime.Parse("8/2/2018") + @""",
                    ""ReceivedQuantity"":0,
                    ""RemainingQuantity"":4
                    },
                    {
                    ""ItemId"": 7,
                    ""OrderDate"":""" + DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 10,
                    ""UnitPrice"" : " + itemMasterLookup[2].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[2].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[2].UnitPrice * 10 + @",
                    ""Description"": """ + itemMasterLookup[2].Description + @""",
                    ""ExpectedDeliveryDate"" :""" + DateTime.Parse("8/2/2018") + @""",
                    ""ReceivedQuantity"":0,
                    ""RemainingQuantity"":10
                    },
                    {
                    ""ItemId"": 8,
                    ""OrderDate"":""" + DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 15,
                    ""UnitPrice"" : " + itemMasterLookup[3].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[3].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[3].UnitPrice * 15 + @",
                    ""Description"": """ + itemMasterLookup[3].Description + @""",
                    ""ExpectedDeliveryDate"" :""" + DateTime.Parse("8/2/2018") + @""",
                    ""ReceivedQuantity"":0,
                    ""RemainingQuantity"":15
                    },
                    {
                    ""ItemId"": 9,
                    ""OrderDate"":""" + DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 10,
                    ""UnitPrice"" : " + itemMasterLookup[4].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[3].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[4].UnitPrice * 10 + @",
                    ""Description"": """ + itemMasterLookup[4].Description + @""",
                    ""ExpectedDeliveryDate"" :""" + DateTime.Parse("8/2/2018") + @""",
                    ""ReceivedQuantity"":0,
                    ""RemainingQuantity"":10
                    }
                ]
    }";

        
            PurchaseOrderView purchaseOrderView = JsonConvert.DeserializeObject<PurchaseOrderView>(json);

            bool result2 = await unitOfWork.purchaseOrderRepository.CreatePurchaseOrderByView(purchaseOrderView);
            Assert.True(true);
        }
     
    }
}
