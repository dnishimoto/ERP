using lssWebApi2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.JobCostLedgerDomain;
using lssWebApi2.JobMasterDomain;
using lssWebApi2.JobPhaseDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.JobMasterDomain
{

    public class UnitJobMaster
    {

        private readonly ITestOutputHelper output;

        public UnitJobMaster(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            JobMasterModule JobMasterMod = new JobMasterModule();
            Customer customer = await JobMasterMod.Customer.Query().GetEntityById(12);
            AddressBook addressBookCustomer = await JobMasterMod.AddressBook.Query().GetEntityById(customer?.AddressId);
            Contract contract = await JobMasterMod.Contract.Query().GetEntityById(5);
            //public long? ProjectManagerId { get; set; }
            JobMasterView view = new JobMasterView()
            {
                CustomerId = customer.CustomerId,
                CustomerName = addressBookCustomer?.Name,
                ContractId = contract.ContractId,
                ContractTitle = contract?.Title,
                JobDescription = "Kuna 4 plex project",
                Address1 = " 123 ABC",
                City = "Kuna",
                State = "Id",
                Zipcode = "83709",
                TotalCommittedAmount = 600000,
                RemainingCommittedAmount = 400000,
                RetainageAmount = 200000,
                JobMasterNumber=(await JobMasterMod.JobMaster.Query().GetNextNumber()).NextNumberValue
            };

            JobMaster jobMaster = await JobMasterMod.JobMaster.Query().MapToEntity(view);

            JobMasterMod.JobMaster.AddJobMaster(jobMaster).Apply();

            JobMaster newJobMaster = await JobMasterMod.JobMaster.Query().GetEntityByNumber(view.JobMasterNumber);
            JobCostType jobPhaseCostTypeMisc = await JobMasterMod.JobCostType.Query().GetEntityById(4);
              Assert.NotNull(newJobMaster);

            IList<JobPhaseView> listJobPhaseViews = new List<JobPhaseView> {
                new JobPhaseView{ ContractId=contract.ContractId,
                ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Work Site Preparation - Safety",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                },
                 new JobPhaseView{ ContractId=contract.ContractId,
                 ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Foundations",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                 },
                  new JobPhaseView{ ContractId=contract.ContractId,
                  ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Building Structure",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                  }
                  ,
                  new JobPhaseView{ ContractId=contract.ContractId,
                  ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Facade",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                }
                  ,
                  new JobPhaseView{ ContractId=contract.ContractId,
                     ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Interior Construction",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                }
                  ,
                  new JobPhaseView{ ContractId=contract.ContractId,
                  ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Commissioning",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                  }
                  ,
                  new JobPhaseView{ ContractId=contract.ContractId,
                  ContractTitle=contract.Title,
                    JobMasterId =newJobMaster.JobMasterId,
                    JobDescription=newJobMaster.JobDescription,
                    PhaseGroup=1,
                    Phase="Grading and Landscaping",
                    JobPhaseNumber= (await JobMasterMod.JobPhase.Query().GetNextNumber()).NextNumberValue,
                    JobCostTypeId=jobPhaseCostTypeMisc.JobCostTypeId,
                    CostCode=jobPhaseCostTypeMisc.CostCode
                  }
            };

            IList<JobPhase> listJobPhases = await JobMasterMod.JobPhase.Query().MapToEntity(listJobPhaseViews);

            JobMasterMod.JobPhase.AddJobPhases(listJobPhases.ToList<JobPhase>()).Apply();

            //Add Purchase Order
            PurchaseOrderModule PurchaseOrderMod = new PurchaseOrderModule();
            ChartOfAccount chartOfAccount = await PurchaseOrderMod.ChartOfAccount.Query().GetEntityById(16);
            Supplier supplier = await PurchaseOrderMod.Supplier.Query().GetEntityById(3);
            AddressBook addressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(supplier?.AddressId);
            Contract pocontract = await PurchaseOrderMod.Contract.Query().GetEntityById(5);
            Buyer buyer = await PurchaseOrderMod.Buyer.Query().GetEntityById(1);
            AddressBook buyerAddressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(buyer?.AddressId);
            TaxRatesByCode taxRatesByCode = await PurchaseOrderMod.TaxRatesByCode.Query().GetEntityById(1);
            //create purchase order and detail
            PurchaseOrderView viewPurchaseOrder = new PurchaseOrderView()
            {
                DocType = "STD",
                PaymentTerms = "Net 30",
                Amount = (24 * 75) + 25,
                AmountPaid = 0,
                Remark = " installation not included ",
                Gldate = DateTime.Parse("1/14/2020"),
                AccountId = chartOfAccount.AccountId,
                Location = chartOfAccount.Location,
                BusUnit = chartOfAccount.BusUnit,
                Subsidiary = chartOfAccount.Subsidiary,
                SubSub = chartOfAccount.SubSub,
                Account = chartOfAccount.Account,
                AccountDescription = chartOfAccount.Description,
                SupplierId = supplier.SupplierId,
                CustomerId = contract?.CustomerId,
                SupplierName = addressBook.Name,
                ContractId = contract?.ContractId,
                Description = " Standard doors - white",
                Ponumber = "PO-123",
                TakenBy = "David Nishimoto",
                ShippedToName = " abc corp",
                ShippedToAddress1 = " 123 abc",
                ShippedToAddress2 = " zone 1",
                ShippedToCity = "Kuna",
                ShippedToState = "ID",
                ShippedToZipcode = "83709",
                BuyerId = buyer.BuyerId,
                BuyerName = buyerAddressBook?.Name,
                RequestedDate = DateTime.Parse("1/20/2014"),
                PromisedDeliveredDate = DateTime.Parse("1/20/2014"),
                Tax = 24 * 75 * taxRatesByCode.TaxRate,
                TransactionDate = DateTime.Parse("1/14/2020"),
                TaxCode1 = taxRatesByCode.TaxCode,
                TaxCode2 = "",
                TaxRate = taxRatesByCode.TaxRate ?? 0,
                PurchaseOrderNumber=(await PurchaseOrderMod.PurchaseOrder.Query().GetNextNumber()).NextNumberValue
            };

            PurchaseOrder purchaseOrder = await PurchaseOrderMod.PurchaseOrder.Query().MapToEntity(viewPurchaseOrder);
            PurchaseOrderMod.PurchaseOrder.AddPurchaseOrder(purchaseOrder).Apply();

            PurchaseOrder newPurchaseOrder = await PurchaseOrderMod.PurchaseOrder.Query().GetEntityByNumber(viewPurchaseOrder.PurchaseOrderNumber);
            Supplier supplierPODetail = await PurchaseOrderMod.Supplier.Query().GetEntityById(newPurchaseOrder?.SupplierId);
            AddressBook addressBookSupplier = await PurchaseOrderMod.AddressBook.Query().GetEntityById(supplierPODetail?.AddressId);

            IList<PurchaseOrderDetailView> listPurchaseOrderDetailViews = new List<PurchaseOrderDetailView>{
                 new PurchaseOrderDetailView()
                 {
                PurchaseOrderId = newPurchaseOrder.PurchaseOrderId,
                Amount = 75*24,
                OrderedQuantity = 24,
                LineDescription="Standard Door",
                LineNumber=1,
                UnitPrice = 75,
                UnitOfMeasure = "Each",
                ExpectedDeliveryDate = DateTime.Parse("1/30/2020"),
                OrderDate = DateTime.Parse("1/16/2020"),
                ReceivedQuantity = 0,
                RemainingQuantity = 24,
                SupplierId= newPurchaseOrder.SupplierId,
                SupplierName=addressBookSupplier?.Name,
                PurchaseOrderDetailNumber=(await PurchaseOrderMod.PurchaseOrderDetail.Query().GetNextNumber()).NextNumberValue
        },
                  new PurchaseOrderDetailView()
                 {
                PurchaseOrderId = newPurchaseOrder.PurchaseOrderId,
                Amount = 25*1,
                OrderedQuantity = 25,
                LineDescription="Door Hinges",
                LineNumber=2,
                UnitPrice = 25,
                UnitOfMeasure = "Box",
                ExpectedDeliveryDate = DateTime.Parse("1/30/2020"),
                OrderDate = DateTime.Parse("1/16/2020"),
                ReceivedQuantity = 0,
                RemainingQuantity = 24,
                SupplierId= newPurchaseOrder.SupplierId,
                SupplierName=addressBookSupplier?.Name,
                PurchaseOrderDetailNumber=(await PurchaseOrderMod.PurchaseOrderDetail.Query().GetNextNumber()).NextNumberValue
        }
    };
            IList<PurchaseOrderDetail> listPurchaseOrderDetail = await PurchaseOrderMod.PurchaseOrderDetail.Query().MapToEntity(listPurchaseOrderDetailViews);
            PurchaseOrderMod.PurchaseOrderDetail.AddPurchaseOrderDetails(listPurchaseOrderDetail.ToList<PurchaseOrderDetail>()).Apply();

            IList<PurchaseOrderDetail> listNewPurchaseOrderDetail = await PurchaseOrderMod.PurchaseOrderDetail.Query().GetEntitiesByPurchaseOrderId(newPurchaseOrder.PurchaseOrderId);


            //*****************Create Accounts Payable
     
            AccountPayableModule AccountPayableMod = new AccountPayableModule();
            ChartOfAccount chartOfAccount2 = await AccountPayableMod.ChartOfAccount.Query().GetEntityById(17);

            AccountPayableView accountPayableView = new AccountPayableView()
            {
                DocNumber = (await AccountPayableMod.AccountPayable.Query().GetNextDocNumber()),
                GrossAmount = newPurchaseOrder.Amount,
                Tax = newPurchaseOrder.Tax,
                DiscountAmount = null,
                Remark = null,
                Gldate = DateTime.Now,
                SupplierId = newPurchaseOrder.SupplierId,
                ContractId = newPurchaseOrder.ContractId,
                PoquoteId = null,
                Description = newPurchaseOrder.Description,
                PurchaseOrderId = newPurchaseOrder.PurchaseOrderId,
                AccountId = chartOfAccount.AccountId,
                DocType = "STD",
                PaymentTerms = newPurchaseOrder.PaymentTerms,
                DiscountPercent = 0,
                AmountOpen = newPurchaseOrder.Amount,
                OrderNumber = newPurchaseOrder.Ponumber,
                AmountPaid = 0,
                AccountPayableNumber = (await AccountPayableMod.AccountPayable.Query().GetNextNumber()).NextNumberValue
            };

            AccountPayable accountPayable = await AccountPayableMod.AccountPayable.Query().MapToEntity(accountPayableView);
            AccountPayableMod.AccountPayable.AddAccountPayable(accountPayable).Apply();
            AccountPayable lookupAccountPayable = await AccountPayableMod.AccountPayable.Query().GetEntityByNumber(accountPayableView.AccountPayableNumber);

            //****************Create the invoice payment
            InvoiceModule invoiceMod = new InvoiceModule();
            Supplier supplier2 = await invoiceMod.Supplier.Query().GetEntityById(purchaseOrder.SupplierId);
            AddressBook addressBookSupplinvModier2 = await invoiceMod.AddressBook.Query().GetEntityById(supplier?.AddressId);
            TaxRatesByCode taxRatesByCode2 = await invoiceMod.TaxRatesByCode.Query().GetEntityByTaxCode(purchaseOrder.TaxCode1);
            NextNumber nextNumber = await invoiceMod.Invoice.Query().GetNextNumber();

            InvoiceView invoiceView = new InvoiceView
            {

                InvoiceDocument = "Inv-" + nextNumber.NextNumberValue.ToString(),
                InvoiceDate = DateTime.Parse("1/17/2020"),
                Amount = purchaseOrder.Amount,
                SupplierId = supplier2?.SupplierId,
                SupplierName = addressBookCustomer?.Name,
                Description = purchaseOrder.Description,
                PaymentTerms = purchaseOrder.PaymentTerms,
                TaxAmount = taxRatesByCode2.TaxRate * purchaseOrder.Amount,
                CompanyId = 1,
                TaxRatesByCodeId = taxRatesByCode2.TaxRatesByCodeId,
                InvoiceNumber = nextNumber.NextNumberValue
            };
        
            Invoice invoice = await invoiceMod.Invoice.Query().MapToEntity(invoiceView);
            invoiceMod.Invoice.AddInvoice(invoice).Apply();

            Invoice newInvoice = await invoiceMod.Invoice.Query().GetEntityByNumber(invoiceView.InvoiceNumber);

            InvoiceDetailModule invDetailMod = new InvoiceDetailModule();
            IList<InvoiceDetailView> listInvoiceDetailViews = new List<InvoiceDetailView>();
            foreach (var item in listNewPurchaseOrderDetail)
            {
                InvoiceDetailView invoiceDetailView = new InvoiceDetailView()
                {
                    Amount = item.Amount,
                    InvoiceId = newInvoice.InvoiceId,
                    InvoiceDetailNumber = (await invDetailMod.InvoiceDetail.Query().GetNextNumber()).NextNumberValue,
                    UnitOfMeasure = item.UnitOfMeasure,
                    Quantity = (int)item.OrderedQuantity,
                    UnitPrice = item.UnitPrice,
                    DiscountPercent = 0,
                    DiscountAmount = 0,
                    SupplierId = item.SupplierId,
                    SupplierName = (await (invDetailMod.AddressBook.Query().GetEntityById((await invDetailMod.Supplier.Query().GetEntityById(item.SupplierId)).AddressId))).Name,
                    PurchaseOrderId = item.PurchaseOrderId,
                    PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                    ExtendedDescription = item.LineDescription

                };
                listInvoiceDetailViews.Add(invoiceDetailView);
            }

            List<InvoiceDetail> listInvoiceDetails = (await invDetailMod.InvoiceDetail.Query().MapToEntity(listInvoiceDetailViews)).ToList<InvoiceDetail>();
            invDetailMod.InvoiceDetail.AddInvoiceDetails(listInvoiceDetails).Apply();


            IList<InvoiceDetail> listLookupInvoiceDetails = await invDetailMod.InvoiceDetail.Query().GetEntitiesByInvoiceId(newInvoice.InvoiceId);

            //Update Accounts Payable - by invoices associated to a po

            IList<Invoice> listInvoiceByPurchaseOrder = await invoiceMod.Invoice.Query().GetEntitiesByPurchaseOrderId(newPurchaseOrder.PurchaseOrderId);
            lookupAccountPayable.AmountOpen = lookupAccountPayable.GrossAmount - listInvoiceByPurchaseOrder.Sum(e => e.Amount);
            AccountPayableMod.AccountPayable.UpdateAccountPayable(lookupAccountPayable).Apply();

            //add to job costing PO
            JobCostLedgerModule JobCostLedgerMod = new JobCostLedgerModule();
            JobPhase jobPhase2 = await JobMasterMod.JobPhase.Query().GetEntityByJobIdAndPhase(newJobMaster.JobMasterId, "Work Site Preparation - Safety");
            JobCostType jobCostTypeMisc = await JobMasterMod.JobCostType.Query().GetEntityById(2);
            JobCostType jobCostTypeMaterial = await JobMasterMod.JobCostType.Query().GetEntityById(1);

            IList<JobCostLedgerView> listJobCostLedgerView = new List<JobCostLedgerView>
            {
                new JobCostLedgerView(){
                    JobMasterId = jobMaster.JobMasterId,
                    ContractId = jobMaster.ContractId,
                    EstimatedHours = 0,
                    EstimatedAmount = 0,
                    JobPhaseId = jobPhase2.JobPhaseId,
                    ActualHours = 0,
                    ActualCost = 0,
                    ProjectedHours = 0,
                    CommittedHours = 0,
                    PurchaseOrderId=newPurchaseOrder.PurchaseOrderId,
                    SupplierId=newPurchaseOrder.SupplierId,
                    CommittedAmount = newPurchaseOrder.Amount,
                    Description = newPurchaseOrder.Description,
                    TransactionType = "PO",
                    Source = "Job Costing",
                    JobCostTypeId = jobCostTypeMisc.JobCostTypeId,
                    JobCostLedgerNumber=( await JobCostLedgerMod.JobCostLedger.Query().GetNextNumber()).NextNumberValue
        },
                new JobCostLedgerView(){
                    JobMasterId = jobMaster.JobMasterId,
                    ContractId = jobMaster.ContractId,
                    EstimatedHours = 0,
                    EstimatedAmount = 0,
                    JobPhaseId = jobPhase2.JobPhaseId,
                    ActualHours = 0,
                    ActualCost = lookupAccountPayable.AmountPaid,
                    ProjectedHours = 0,
                    CommittedHours = 0,
                    PurchaseOrderId=lookupAccountPayable.PurchaseOrderId,
                    SupplierId=lookupAccountPayable.SupplierId,
                    CommittedAmount = 0,
                    Description = lookupAccountPayable.Description,
                    TransactionType = "AP",
                    Source = "Job Costing",
                    TaxAmount=lookupAccountPayable.Tax,
                    JobCostTypeId = jobCostTypeMaterial.JobCostTypeId,
                    JobCostLedgerNumber=( await JobCostLedgerMod.JobCostLedger.Query().GetNextNumber()).NextNumberValue
                }
            };

            IList<JobCostLedger> listJobCostLedger = await JobCostLedgerMod.JobCostLedger.Query().MapToEntity(listJobCostLedgerView);
            JobCostLedgerMod.JobCostLedger.AddJobCostLedgers(listJobCostLedger.ToList<JobCostLedger>()).Apply();


            //Create the general ledger entry
            //Create the supplier ledger entry
            //Create Pay Roll
            //Add Pay Roll to job costing


            invDetailMod.InvoiceDetail.DeleteInvoiceDetails((listLookupInvoiceDetails).ToList<InvoiceDetail>()).Apply();
            invoiceMod.Invoice.DeleteInvoice(newInvoice).Apply();
            AccountPayableMod.AccountPayable.DeleteAccountPayable(lookupAccountPayable).Apply();
            PurchaseOrderMod.PurchaseOrderDetail.DeletePurchaseOrderDetails(listPurchaseOrderDetail.ToList<PurchaseOrderDetail>()).Apply();
            JobMasterMod.JobPhase.DeleteJobPhases(listJobPhases.ToList<JobPhase>()).Apply();
            IList<JobPhase> lookupListJobPhases = await JobMasterMod.JobPhase.Query().GetEntitiesByJobMasterId(newJobMaster.JobMasterId);
            if (lookupListJobPhases.Count > 0) Assert.True(false);
            newJobMaster.JobDescription = "JobMaster Test Update";
            JobMasterMod.JobMaster.UpdateJobMaster(newJobMaster).Apply();
            JobMasterView updateView = await JobMasterMod.JobMaster.Query().GetViewById(newJobMaster.JobMasterId);
            Assert.Same(updateView.JobDescription, "JobMaster Test Update");
            JobMasterMod.JobMaster.DeleteJobMaster(newJobMaster).Apply();
            JobMaster lookupJobMaster = await JobMasterMod.JobMaster.Query().GetEntityById(view.JobMasterId);

            Assert.Null(lookupJobMaster);
        }



    }
}
