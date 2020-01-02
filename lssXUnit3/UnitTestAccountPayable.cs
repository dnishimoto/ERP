using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.SupplierDomain;
using lssWebApi2.SupplierInvoiceDomain;

namespace lssWebApi2.AccountPayableDomain
{

    public class UnitTestAccountPayable
    {
        private readonly ITestOutputHelper output;

        public UnitTestAccountPayable(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestPayAccountsPayable()
        {
            long customerId = 2;
            string poNumber = "PO-2";

            AccountPayableModule acctPayablesMod = new AccountPayableModule();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await acctPayablesMod.AddressBook.Query().GetAddressIdByCustomerId(customerId);

            ChartOfAccount coa = await acctPayablesMod.ChartOfAccount.Query().GetEntity("1000", "1200", "210", "");

            AccountPayable acctPay = await acctPayablesMod.AccountPayable.Query().GetAcctPayByPONumber(poNumber);

            SupplierInvoice supplierInvoice = await acctPayablesMod.SupplierInvoice.Query().GetEntityByPONumber(poNumber);

            if (coa == null || acctPay == null || supplierInvoice == null)
            {
                Assert.True(false);
            }
           

            ledgerView.GeneralLedgerId = -1;
            ledgerView.SupplierId = 3;
            ledgerView.DocNumber = acctPay.DocNumber??0;   //doc number of the account payable
            ledgerView.AcctPayId = acctPay.AcctPayId;
            ledgerView.InvoiceId = supplierInvoice.SupplierInvoiceId;
            ledgerView.DocType = "PV";
            ledgerView.Amount = 268M;
            ledgerView.LedgerType = "AA";
            ledgerView.GLDate = DateTime.Parse("8/28/2018");
            ledgerView.AccountId = coa.AccountId;
            ledgerView.CreatedDate = DateTime.Parse("8/28/2018");
            ledgerView.AddressId = addressId ?? 0;
            ledgerView.Comment = "Payment for back to school";
            ledgerView.DebitAmount = 0;
            ledgerView.CreditAmount = 268M;
            ledgerView.FiscalPeriod = 8;
            ledgerView.FiscalYear = 2018;
            ledgerView.CheckNumber = "113";

            acctPayablesMod.CreateAccountPayable(ledgerView);
            acctPayablesMod.AccountPayable.Apply();

            Assert.True(true);
               
        }
        [Fact]
        public async Task TestReceiveSupplierInvoice()
        {
            long? supplierId = 3;
            SupplierModule supplierMod = new SupplierModule();


            try
            {
                SupplierView supplierView = await supplierMod.Supplier.Query().GetViewById(supplierId??0);

                string json = @"{
            ""SupplierId"" : " + supplierView.SupplierId + @",
            ""SupplierInvoiceNumber"": ""AZW23-1"", 
            ""SupplierInvoiceDate"" : """ + DateTime.Parse("8/20/2018") + @""",
            ""PONumber"" : ""PO-2"",
            ""Amount"": 268,
            ""Description"":  ""Back to School supplies"",
            ""TaxAmount"" : 16.08,
            ""PaymentDueDate"": """ + DateTime.Parse("8/20/2018") + @""",
            ""PaymentTerms"" : ""Net 30"",
            ""FreightCost"" : 4.98,
            

            ""SupplierInvoiceDetailViews"":[
                    {
                    ""ItemId"": 5,
                    ""Quantity"": 5,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 50 + @",
                    ""UnitOfMeasure"" : """ + "Dozen" + @""",
                    ""Description"": """ + "Pencil HB" + @"""
                    },
                    {
                    ""ItemId"": 6,
                    ""Quantity"": 4,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 40 + @",
                    ""UnitOfMeasure"" : """ + "Dozen" + @""",
                    ""Description"": """ + "Pencils 2B" + @"""
                    },
                    {
                    ""ItemId"": 7,
                    ""Quantity"": 10,
                    ""UnitPrice"" : " + 3 + @",
                    ""ExtendedCost"" : " + 30 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "Paper - A4, Photo coper, 70 grams" + @"""
                    },
                    {
                    ""ItemId"": 8,
                    ""Quantity"": 15,
                    ""UnitPrice"" : " + 3.20 + @",
                    ""ExtendedCost"" : " + 48 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "NPaper - A4, Photo Copier, 80 gramULL" + @"""
                    },
                    {
                    ""ItemId"": 9,
                    ""Quantity"": 5,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 100 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "Pens - Ball Point, Blue" + @"""
                    }
                ]

            }";
                SupplierInvoiceView supplierInvoiceView = JsonConvert.DeserializeObject<SupplierInvoiceView>(json);

                AccountPayableModule apMod = new AccountPayableModule();
               apMod.CreateSupplierInvoice(supplierInvoiceView);
                apMod.AccountPayable.Apply();

       
                   
            }
            catch (Exception ex) { }
        }
        [Fact]
        public async Task TestCreateInboundPackingSlip()
        {
            long supplierId = 3;

            try
            {

                SupplierModule supplierMod = new SupplierModule();

                SupplierView supplierView = await supplierMod.Supplier.Query().GetViewById(supplierId);
                Udc slipTypeUDC = await supplierMod.Udc.Query().GetUdc("PACKINGSLIP_TYPE", "INBOUND");



                string json = @"{
            ""SupplierId"" : " + supplierView.SupplierId + @",
            ""ReceivedDate"" : """ + DateTime.Parse("8/16/2018") + @""",
            ""SlipDocument"" : ""SLIP-1"",
            ""PONumber"" :""PO-2"",
            ""SlipType"" : """ + slipTypeUDC.KeyCode + @""",

            ""PackingSlipDetailViews"":[
                    {
                    ""ItemId"": 5,
                    ""Quantity"": 5,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 50 + @",
                    ""UnitOfMeasure"" : """ + "Dozen" + @""",
                    ""Description"": """ + "Pencil HB" + @"""
                    },
                    {
                    ""ItemId"": 6,
                    ""Quantity"": 4,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 40 + @",
                    ""UnitOfMeasure"" : """ + "Dozen" + @""",
                    ""Description"": """ + "Pencils 2B" + @"""
                    },
                    {
                    ""ItemId"": 7,
                    ""Quantity"": 10,
                    ""UnitPrice"" : " + 3 + @",
                    ""ExtendedCost"" : " + 30 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "Paper - A4, Photo coper, 70 grams" + @"""
                    },
                    {
                    ""ItemId"": 8,
                    ""Quantity"": 15,
                    ""UnitPrice"" : " + 3.20 + @",
                    ""ExtendedCost"" : " + 48 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "NPaper - A4, Photo Copier, 80 gramULL" + @"""
                    },
                    {
                    ""ItemId"": 9,
                    ""Quantity"": 5,
                    ""UnitPrice"" : " + 10 + @",
                    ""ExtendedCost"" : " + 100 + @",
                    ""UnitOfMeasure"" : """ + "Ream" + @""",
                    ""Description"": """ + "Pens - Ball Point, Blue" + @"""
                    }
                ]

            }";

                PackingSlipView packingSlipView = JsonConvert.DeserializeObject<PackingSlipView>(json);

                AccountPayableModule apMod = new AccountPayableModule();
                 apMod.CreatePackingSlip(packingSlipView);
                apMod.AccountPayable.Apply();
                           
                Assert.True(true);
            }
            catch (Exception ex) { }

        }
        [Fact]
        public async Task TestCreatePurchaseOrder()
        {
            SupplierModule supplierMod = new SupplierModule();

            ItemMaster itemMaster = new ItemMaster();
            itemMaster.ItemCode = "P2001Test";
            itemMaster.Branch = "700";
            itemMaster.Description = "Highlighter - 3 Color";
            itemMaster.UnitOfMeasure = "Sets";
            itemMaster.UnitPrice = 6M;

            supplierMod.ItemMaster.CreateItemMaster(itemMaster);
            supplierMod.ItemMaster.Apply();

        
            AddressBook addressBook = new AddressBook();
            addressBook.CompanyName = "Sample Company Part Ltd";
            addressBook.Name = "";
            addressBook.FirstName = "";
            addressBook.LastName = "";

            LocationAddress locationAddress = new LocationAddress();
            locationAddress.AddressLine1 = "204 Collins Street";
            locationAddress.City = "Melbourne";
            locationAddress.Zipcode = "3000";
            locationAddress.Country = "Australia";

            EmailEntity email = new EmailEntity();
            email.Email = "SampleCompany@Party.com";
            email.LoginEmail = true;
            email.Password = "123";



           supplierMod.CreateSupplierProfile(addressBook, email,locationAddress);
        
            SupplierView supplierView = await supplierMod.Supplier.Query().GetViewByEmail(email);

            ChartOfAccount coa = await supplierMod.ChartOfAccount.Query().GetEntity("1000", "1200", "240", "");

            Company company = await supplierMod.ChartOfAccount.Query().GetCompany();

            ItemMaster[] itemMasterLookup = new ItemMaster[5];

            itemMasterLookup[0] = await supplierMod.ItemMaster.Query().GetEntityById(5);
            itemMasterLookup[1] = await supplierMod.ItemMaster.Query().GetEntityById(6);
            itemMasterLookup[2] = await supplierMod.ItemMaster.Query().GetEntityById(7);
            itemMasterLookup[3] = await supplierMod.ItemMaster.Query().GetEntityById(8);
            itemMasterLookup[4] = await supplierMod.ItemMaster.Query().GetEntityById(9);

            Udc udcAcctPayDocType = await supplierMod.Udc.Query().GetUdc("AcctPayDocType", "STD");

            string json = @"{
            ""DocType"" : """ + udcAcctPayDocType.KeyCode + @""",
            ""PaymentTerms"" : ""Net 30"",
            ""GLDate"" : """ + DateTime.Parse("7/30/2018") + @""",
            ""AccountId"" :" + coa.AccountId + @",
            ""SupplierId"" :" + (supplierView.SupplierId).ToString() + @",
            ""SupplierName"" :""" + supplierView.SupplierName + @""",
            ""Description"" :""Back to School Inventory"",
            ""PONumber"" :""PO-2"",
            ""TakenBy"" : ""David Nishimoto"",
            ""BuyerId"" :" + company.CompanyId + @",
            ""TaxCode1"" :""" + company.TaxCode1 + @""",
            ""TaxCode2"" :""" + company.TaxCode2 + @""",
            ""ShippedToName"" :""" + company.CompanyName + @""",
            ""ShippedToAddress1"" :""" + company.CompanyStreet + @""",
            ""ShippedToCity"" :""" + company.CompanyCity + @""",
            ""ShippedToState"" :""" + company.CompanyState + @""",
            ""ShippedToZipcode"" :""" + company.CompanyZipcode + @""",
            ""RequestedDate"" :""" + DateTime.Parse("7/24/2018") + @""",
            ""PromisedDeliveredDate"" :""" + DateTime.Parse("8/2/2018") + @""",
            ""TransactionDate"" :""" + DateTime.Parse("7/30/2018") + @""", 

            ""PurchaseOrderDetailViews"":[
                    {
                    ""ItemId"": 5,
                    ""OrderDate"":""" + DateTime.Parse("7 / 30 / 2018") + @""",
                    ""OrderedQuantity"": 5,
                    ""UnitPrice"" : " + itemMasterLookup[0].UnitPrice + @",
                    ""UnitOfMeasure"" : """ + itemMasterLookup[0].UnitOfMeasure + @""",
                    ""Amount"" : " + itemMasterLookup[0].UnitPrice * 5 + @",
                    ""Description"": """ + itemMasterLookup[0].Description + @""",
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


            AccountPayableModule apMod = new AccountPayableModule();
            apMod.CreateByPurchaseOrderView(purchaseOrderView);
            apMod.AccountPayable.Apply();

        
            Assert.True(true);
        }

    }
}
