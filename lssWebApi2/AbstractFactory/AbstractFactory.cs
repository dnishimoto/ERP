using ERP_Core2.AccountsPayableDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoiceDetailsDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.PurchaseOrderDomain;
using ERP_Core2.SupplierInvoicesDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using ERP_Core2.ChartOfAccountsDomain;
using ERP_Core2.BudgetDomain;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.AbstractFactory
{
    //Address Book Domain
    public abstract partial class AbstractFactory
    {
        public abstract SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor);
        public abstract EmployeeView MapEmployeeView(Employee employee);
        public abstract SupplierView MapSupplierView(Supplier supplier);
        public abstract CarrierView MapCarrierView(Carrier carrier);
    }
    //Customer Domain
    public abstract partial class AbstractFactory
    {
        public abstract BuyerView MapBuyerView(Buyer buyer);
        public abstract InvoiceView MapInvoiceView(Invoice invoice);
        public abstract InvoiceDetailView MapInvoiceDetails(InvoiceDetail invoiceDetail);
        public abstract CustomerClaimView MapCustomerClaimView(CustomerClaim customerClaim);
        public abstract ScheduleEventView MapScheduleEventView(ScheduleEvent scheduleEvent);
        public abstract ContractView MapContractView(Contract contract);
        public abstract LocationAddressView MapLocationAddressView(LocationAddress locationAddress);
        public abstract PhoneView MapPhoneView(Phones phone);
        public abstract EmailView MapEmailView(Emails email);
        public abstract AccountReceiveableView MapAccountReceivableView(AcctRec acctRec);
        public abstract void MapEmailEntity(ref Emails email, EmailView emailView);
        public abstract void MapAddressBookEntity(ref AddressBook addressBook, CustomerView customerView);
        public abstract void MapCustomerEntity(ref Customer customer, CustomerView customerView);
        public abstract void MapLocationAddressEntity(ref LocationAddress locationAddress, LocationAddressView view);
        public abstract ChartOfAccountView MapChartOfAccountView(ChartOfAccts chartOfAcct);
        public abstract void MapChartOfAccountEntity(ref ChartOfAccts item, ChartOfAccountView chartOfAccountView);
        public abstract GeneralLedgerView MapGeneralLedgerView(GeneralLedger generalLedger);
        public abstract void MapInvoiceEntity(ref Invoice invoice, InvoiceView invoiceView);
        public abstract void MapInvoiceDetailEntity(ref InvoiceDetail invoiceDetail, InvoiceDetailView invoiceDetailView);
        public abstract void MapCustomerLedgerEntity(ref CustomerLedger customerLedger, CustomerLedgerView customerLedgerView);
        public abstract CustomerLedgerView MapCustomerLedgerView(CustomerLedger customerLedger);
        public abstract AccountPayableView MapAccountPayableView(AcctPay acctPay);
        public abstract PurchaseOrderView MapPurchaseOrderView(PurchaseOrder purchaseOrder);
        public abstract PurchaseOrderDetailView MapPurchaseOrderDetailView(PurchaseOrderDetail purchaseOrderDetail);
        public abstract void MapPurchaseOrderEntity(ref PurchaseOrder purchaseOrder, PurchaseOrderView purchaseOrderView);
        public abstract void MapPurchaseOrderDetailEntity(ref PurchaseOrderDetail purchaseOrderDetail, PurchaseOrderDetailView purchaseOrderDetailView);
        public abstract PackingSlipView MapPackingSlipView(PackingSlip packingSlip);
        public abstract PackingSlipDetailView MapPackingSlipDetailView(PackingSlipDetail packingSlipDetail);
        public abstract void MapPackingSlipEntity(ref PackingSlip packingSlip, PackingSlipView packingSlipView);
        public abstract void MapPackingSlipDetailEntity(ref PackingSlipDetail packingSlipDetail, PackingSlipDetailView packingSlipDetailView);
        public abstract void MapPackingSlipIntoInventoryEntity(ref Inventory inventory, PackingSlipDetailView packingSlipDetailView);
        public abstract void MapSupplierInvoiceEntity(ref SupplierInvoice supplierInvoice, SupplierInvoiceView supplierInvoiceView);
        public abstract void MapSupplierInvoiceDetailEntity(ref SupplierInvoiceDetail supplierInvoiceDetail, SupplierInvoiceDetailView supplierInvoiceDetailView);
        public abstract SupplierLedgerView MapSupplierLedgerView(GeneralLedgerView generalLedgerView);
        public abstract SupplierLedgerView MapSupplierLedgerView(SupplierLedger supplierLedger);
        public abstract void MapSupplierLedgerEntity(ref SupplierLedger supplierLedger, SupplierLedgerView view);
        public abstract void MapTimeAndAttendanceScheduleEntity(ref TimeAndAttendanceSchedule schedule, TimeAndAttendanceScheduleView view);
        public abstract void MapTimeAndAttendanceScheduledToWorkEntity(ref TimeAndAttendanceScheduledToWork scheduledToWork, TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeView,string payCode);
        public abstract TimeAndAttendanceScheduleView MapTimeAndAttendanceScheduleView(TimeAndAttendanceSchedule item);
        public abstract void MapBudgetRangeEntity(ref BudgetRange budgetRange, BudgetRangeView view);
        public abstract BudgetRangeView MapBudgetRangeView(BudgetRange budgetRange);
        public abstract void MapBudgetEntity(ref Budget budget, BudgetView budgetView);
        public abstract void MapRangeToBudgetViewEntity(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        public abstract BudgetActualsView MapBudgetRangeToBudgetActuals(BudgetRangeView budgetRangeView);
        public abstract BudgetView MapBudgetView(Budget budget);
        public abstract void MapSupplierViewEntity(ref  Supplier supplier,  SupplierView view);
        public abstract void MapAcctRecFeeEntity(ref AcctRecFee acctRecFee, AccountReceivableFlatView view);
        public abstract AddressBookView MapAddressBookView(AddressBook item);
        public abstract void MapAddressBookEntity(ref AddressBook addressBook, AddressBookView view);

        }
    //Time and Attendance Domain
    public abstract partial class AbstractFactory
    {
        public abstract TimeAndAttendancePunchInView MapTAPunchinView(TimeAndAttendancePunchIn taPunchin);

    }
    //General Ledger
    public abstract partial class AbstractFactory
    {
        public abstract void MapGeneralLedgerEntity(ref GeneralLedger ledger, GeneralLedgerView view);

    }

    //Business View Factory
    //public abstract partial class BusinessViewFactory : AbstractFactory
   // {
        //pass through class
    //}


    //Address Book
    public partial class ApplicationViewFactory : AbstractFactory
    {
        public override SupervisorView MapSupervisorView(Supervisor supervisor, Supervisor parentSupervisor)
        {
            return new SupervisorView(supervisor, parentSupervisor);
        }
        public override EmployeeView MapEmployeeView(Employee employee)
        {
            return new EmployeeView(employee);
        }
        public override SupplierView MapSupplierView(Supplier supplier)
        {
            return new SupplierView(supplier);
        }
        public override CarrierView MapCarrierView(Carrier carrier)
        {
            return new CarrierView(carrier);
        }
        public override void MapLocationAddressEntity(ref LocationAddress locationAddress, LocationAddressView view)
        {
            locationAddress.AddressId = view.AddressId;
            locationAddress.AddressLine1 = view.AddressLine1;
            locationAddress.City = view.City;
            locationAddress.State = view.State;
            locationAddress.Country = view.Country;
            locationAddress.Zipcode = view.Zipcode;
            locationAddress.TypeXrefId = view.TypeXRefId;
        }
        public override AddressBookView MapAddressBookView(AddressBook item)
        {
            return new AddressBookView(item);
        }


    }
    //Time and Attendance
    //public partial class ApplicationViewFactory : BusinessViewFactory
    public partial class ApplicationViewFactory: AbstractFactory
    {
        public override TimeAndAttendancePunchInView MapTAPunchinView(TimeAndAttendancePunchIn taPunchin)
        {
            return new TimeAndAttendancePunchInView(taPunchin);
        }
    }

    //Customer Domain
    //public partial class ApplicationViewFactory : BusinessViewFactory
    public partial class ApplicationViewFactory : AbstractFactory
    {
        public override BuyerView MapBuyerView(Buyer buyer)
        {
            return new BuyerView(buyer);
        }
        public override InvoiceView MapInvoiceView(Invoice invoice)
        {
            InvoiceView invoiceView = new InvoiceView(invoice);
            List<InvoiceDetailView> list = new List<InvoiceDetailView>();


            foreach (var item in invoice.InvoiceDetail)
            {
                list.Add(MapInvoiceDetails(item));
            }
            invoiceView.InvoiceDetailViews = list;
            return invoiceView;
        }
        public override InvoiceDetailView MapInvoiceDetails(InvoiceDetail invoiceDetail)
        {
            return new InvoiceDetailView(invoiceDetail);
        }
        public override void MapAcctRecFeeEntity(ref AcctRecFee acctRecFee, AccountReceivableFlatView view)
        {

            //public decimal? FeeAmount { get; set; }
            acctRecFee.PaymentDueDate = view.PaymentDueDate;
            acctRecFee.CustomerId = view.CustomerId;
            acctRecFee.DocNumber = view.DocNumber??0;
            acctRecFee.AcctRecDocType = view.AcctRecDocType;
            acctRecFee.AcctRecId = view.AcctRecId??0;


    }
        public override void MapInvoiceEntity(ref Invoice invoice, InvoiceView invoiceView)
        {

            invoice.InvoiceNumber = invoiceView.InvoiceNumber;
            invoice.InvoiceDate = invoiceView.InvoiceDate;
            invoice.Amount = invoiceView.Amount;
            invoice.CustomerId = invoiceView.CustomerId ?? 0;
            invoice.Description = invoiceView.Description;
            invoice.TaxAmount = invoiceView.TaxAmount;
            invoice.PaymentDueDate = invoiceView.PaymentDueDate;
            invoice.PaymentTerms = invoiceView.PaymentTerms;
            invoice.CompanyId = invoiceView.CompanyId ?? 0;
            invoice.DiscountDueDate = invoiceView.DiscountDueDate;
            invoice.DiscountAmount = invoiceView.DiscountAmount;
            invoice.FreightCost = invoiceView.FreightCost ?? 0;

        }
        public override void MapInvoiceDetailEntity(ref InvoiceDetail invoiceDetail, InvoiceDetailView invoiceDetailView)
        {
            invoiceDetail.InvoiceId = invoiceDetailView.InvoiceId ?? 0;
            invoiceDetail.UnitOfMeasure = invoiceDetailView.UnitOfMeasure;
            invoiceDetail.Quantity = invoiceDetailView.Quantity;
            invoiceDetail.UnitPrice = invoiceDetailView.UnitPrice;
            invoiceDetail.Amount = invoiceDetailView.Amount;
            invoiceDetail.DiscountPercent = invoiceDetailView.DiscountPercent;
            invoiceDetail.DiscountAmount = invoiceDetailView.DiscountAmount;
            invoiceDetail.DiscountDueDate = invoiceDetailView.DiscountDueDate;
            invoiceDetail.ItemId = invoiceDetailView.ItemId ?? 0;

        }
        public override PurchaseOrderView MapPurchaseOrderView(PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrderView(purchaseOrder);
        }
        public override PurchaseOrderDetailView MapPurchaseOrderDetailView(PurchaseOrderDetail purchaseOrderDetail)
        {
            return new PurchaseOrderDetailView(purchaseOrderDetail);
        }
        public override void MapPurchaseOrderEntity(ref PurchaseOrder purchaseOrder, PurchaseOrderView purchaseOrderView)
        {
            purchaseOrder.PurchaseOrderId = purchaseOrderView.PurchaseOrderId ?? 0;
            purchaseOrder.DocType = purchaseOrderView.DocType;
            purchaseOrder.PaymentTerms = purchaseOrderView.PaymentTerms;
            purchaseOrder.GrossAmount = purchaseOrderView.GrossAmount;
            purchaseOrder.Remark = purchaseOrderView.Remark;
            purchaseOrder.Gldate = purchaseOrderView.GLDate;
            purchaseOrder.AccountId = purchaseOrderView.AccountId;
            purchaseOrder.SupplierId = purchaseOrderView.SupplierId;
            purchaseOrder.ContractId = purchaseOrderView.ContractId;
            purchaseOrder.PoquoteId = purchaseOrderView.POQuoteId;
            purchaseOrder.Description = purchaseOrderView.Description;
            purchaseOrder.Ponumber = purchaseOrderView.PONumber;
            purchaseOrder.TakenBy = purchaseOrderView.TakenBy;
            purchaseOrder.BuyerId = purchaseOrderView.BuyerId;
            purchaseOrder.RequestedDate = purchaseOrderView.RequestedDate;
            purchaseOrder.PromisedDeliveredDate = purchaseOrderView.PromisedDeliveredDate;
            purchaseOrder.Tax = purchaseOrderView.Tax;
            purchaseOrder.TaxCode1 = purchaseOrderView.TaxCode1;
            purchaseOrder.TaxCode2 = purchaseOrderView.TaxCode2;
            purchaseOrder.TransactionDate = purchaseOrderView.TransactionDate;
            purchaseOrder.AmountPaid = purchaseOrderView.AmountPaid;
            purchaseOrder.ShippedToName = purchaseOrderView.ShippedToName;
            purchaseOrder.ShippedToAddress1 = purchaseOrderView.ShippedToAddress1;
            purchaseOrder.ShippedToAddress2 = purchaseOrderView.ShippedToAddress2;
            purchaseOrder.ShippedToCity = purchaseOrderView.ShippedToCity;
            purchaseOrder.ShippedToZipcode = purchaseOrderView.ShippedToZipcode;
            purchaseOrder.ShippedToState = purchaseOrderView.ShippedToState;
        }
        public override void MapPurchaseOrderDetailEntity(ref PurchaseOrderDetail purchaseOrderDetail, PurchaseOrderDetailView purchaseOrderDetailView)
        {
            purchaseOrderDetail.PurchaseOrderDetailId = purchaseOrderDetailView.PurchaseOrderDetailId;
            purchaseOrderDetail.PurchaseOrderId = purchaseOrderDetailView.PurchaseOrderId;
            purchaseOrderDetail.Amount = purchaseOrderDetailView.Amount;
            purchaseOrderDetail.OrderedQuantity = purchaseOrderDetailView.OrderedQuantity;
            purchaseOrderDetail.ItemId = purchaseOrderDetailView.ItemId;
            purchaseOrderDetail.UnitPrice = purchaseOrderDetailView.UnitPrice;
            purchaseOrderDetail.UnitOfMeasure = purchaseOrderDetailView.UnitOfMeasure;
            purchaseOrderDetail.ReceivedDate = purchaseOrderDetailView.ReceivedDate;
            purchaseOrderDetail.ExpectedDeliveryDate = purchaseOrderDetailView.ExpectedDeliveryDate;
            purchaseOrderDetail.OrderDate = purchaseOrderDetailView.OrderDate;
            purchaseOrderDetail.ReceivedQuantity = purchaseOrderDetailView.ReceivedQuantity;
            purchaseOrderDetail.RemainingQuantity = purchaseOrderDetailView.RemainingQuantity;
            purchaseOrderDetail.Description = purchaseOrderDetailView.Description;
        }
        public override PackingSlipView MapPackingSlipView(PackingSlip packingSlip)
        {
            return new PackingSlipView(packingSlip);
        }
        public override void MapPackingSlipEntity(ref PackingSlip packingSlip, PackingSlipView packingSlipView)
        {
            packingSlip.PackingSlipId = packingSlipView.PackingSlipId;
            packingSlip.SupplierId = packingSlipView.SupplierId;
            packingSlip.ReceivedDate = packingSlipView.ReceivedDate;
            packingSlip.SlipDocument = packingSlipView.SlipDocument;
            packingSlip.Ponumber = packingSlipView.PONumber;
            packingSlip.Remark = packingSlipView.Remark;
            packingSlip.SlipType = packingSlipView.SlipType;
            packingSlip.Amount = packingSlipView.Amount ?? 0;
        }
        public override void MapPackingSlipDetailEntity(ref PackingSlipDetail packingSlipDetail, PackingSlipDetailView packingSlipDetailView)
        {

            packingSlipDetail.PackingSlipDetailId = packingSlipDetailView.PackingSlipDetailId;
            packingSlipDetail.PackingSlipId = packingSlipDetailView.PackingSlipId;
            packingSlipDetail.ItemId = packingSlipDetailView.ItemId;
            packingSlipDetail.Quantity = packingSlipDetailView.Quantity;
            packingSlipDetail.UnitPrice = packingSlipDetailView.UnitPrice;
            packingSlipDetail.ExtendedCost = packingSlipDetailView.ExtendedCost;
            packingSlipDetail.UnitOfMeasure = packingSlipDetailView.UnitOfMeasure;
            packingSlipDetail.Description = packingSlipDetailView.Description;
        }
        public override void MapSupplierInvoiceEntity(ref SupplierInvoice supplierInvoice, SupplierInvoiceView supplierInvoiceView)
        {
            supplierInvoice.SupplierInvoiceId = supplierInvoiceView.SupplierInvoiceId ?? 0;
            supplierInvoice.SupplierInvoiceNumber = supplierInvoiceView.SupplierInvoiceNumber;
            supplierInvoice.SupplierInvoiceDate = supplierInvoiceView.SupplierInvoiceDate;
            supplierInvoice.Ponumber = supplierInvoiceView.PONumber;
            supplierInvoice.Amount = supplierInvoiceView.Amount;
            supplierInvoice.Description = supplierInvoiceView.Description;
            supplierInvoice.TaxAmount = supplierInvoiceView.TaxAmount;
            supplierInvoice.PaymentDueDate = supplierInvoiceView.PaymentDueDate;
            supplierInvoice.PaymentTerms = supplierInvoiceView.PaymentTerms;
            supplierInvoice.DiscountDueDate = supplierInvoiceView.DiscountDueDate;
            supplierInvoice.SupplierId = supplierInvoiceView.SupplierId ?? 0;
            supplierInvoice.FreightCost = supplierInvoiceView.FreightCost;
            supplierInvoice.DiscountAmount = supplierInvoiceView.DiscountAmount;
        }
        public override void MapSupplierInvoiceDetailEntity(ref SupplierInvoiceDetail supplierInvoiceDetail, SupplierInvoiceDetailView supplierInvoiceDetailView)
        {
            supplierInvoiceDetail.SupplierInvoiceDetailId = supplierInvoiceDetailView.SupplierInvoiceDetailId ?? 0;
            supplierInvoiceDetail.SupplierInvoiceId = supplierInvoiceDetailView.SupplierInvoiceId ?? 0;
            supplierInvoiceDetail.UnitPrice = supplierInvoiceDetailView.UnitPrice;
            supplierInvoiceDetail.Quantity = supplierInvoiceDetailView.Quantity;
            supplierInvoiceDetail.UnitOfMeasure = supplierInvoiceDetailView.UnitOfMeasure;
            supplierInvoiceDetail.ExtendedCost = supplierInvoiceDetailView.ExtendedCost;
            supplierInvoiceDetail.ItemId = supplierInvoiceDetailView.ItemId ?? 0;
            supplierInvoiceDetail.Description = supplierInvoiceDetailView.Description;
            supplierInvoiceDetail.DiscountDueDate = supplierInvoiceDetailView.DiscountDueDate;
            supplierInvoiceDetail.DiscountAmount = supplierInvoiceDetailView.DiscountAmount;
            supplierInvoiceDetail.DiscountPercent = supplierInvoiceDetailView.DiscountPercent;
        }
        public override void MapPackingSlipIntoInventoryEntity(ref Inventory inventory, PackingSlipDetailView packingSlipDetailView)
        {
            inventory.ItemId = packingSlipDetailView.ItemId;
            inventory.Description = packingSlipDetailView.Description;
            inventory.UnitOfMeasure = packingSlipDetailView.UnitOfMeasure;
            inventory.Quantity = packingSlipDetailView.Quantity;
            inventory.ExtendedPrice = packingSlipDetailView.ExtendedCost;
            inventory.PackingSlipDetailId = packingSlipDetailView.PackingSlipDetailId;
        }
        public override void MapSupplierLedgerEntity(ref SupplierLedger supplierLedger, SupplierLedgerView view)
        {
            supplierLedger.SupplierLedgerId = view.SupplierLedgerId;
            supplierLedger.SupplierId = view.SupplierId;
            supplierLedger.InvoiceId = view.InvoiceId;
            supplierLedger.AcctPayId = view.AcctPayId;
            supplierLedger.Amount = view.Amount;
            supplierLedger.Gldate = view.GLDate;
            supplierLedger.AccountId = view.AccountId;
            supplierLedger.GeneralLedgerId = view.GeneralLedgerId;
            supplierLedger.DocNumber = view.DocNumber;
            supplierLedger.DocType = view.DocType;
            supplierLedger.Comment = view.Comment;
            supplierLedger.AddressId = view.AddressId;
            supplierLedger.CreatedDate = view.CreatedDate;
            supplierLedger.FiscalPeriod = view.FiscalPeriod;
            supplierLedger.FiscalYear = view.FiscalYear;
        }
        public override void MapTimeAndAttendanceScheduleEntity(ref TimeAndAttendanceSchedule schedule, TimeAndAttendanceScheduleView view)
        {
            schedule.ScheduleId = view.ScheduleId;
            schedule.ScheduleName = view.ScheduleName;
            schedule.StartDate = view.StartDate;
            schedule.EndDate = view.EndDate;
            schedule.ShiftId = view.ShiftId;
            schedule.Monday = view.Monday;
            schedule.Tuesday = view.Tuesday;
            schedule.Wednesday = view.Wednesday;
            schedule.Thursday = view.Thursday;
            schedule.Friday = view.Friday;
            schedule.Saturday = view.Saturday;
            schedule.Sunday = view.Sunday;

            schedule.ScheduleGroup = view.ScheduleGroup;
        }
        public override void MapTimeAndAttendanceScheduledToWorkEntity(ref TimeAndAttendanceScheduledToWork scheduledToWork, TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeView, string payCode)
        {
            scheduledToWork.EmployeeId = employeeView.EmployeeId ?? 0;
            scheduledToWork.EmployeeName = employeeView.EmployeeName;
            scheduledToWork.ScheduleId = scheduleView.ScheduleId;
            scheduledToWork.ScheduleName = scheduleView.ScheduleName;
            scheduledToWork.StartDate = dayView.StartDate;
            scheduledToWork.EndDate = dayView.EndDate;
            scheduledToWork.StartDateTime = dayView.StartDateTime;
            scheduledToWork.EndDateTime = dayView.EndDateTime;
            scheduledToWork.ShiftId = scheduleView.ShiftId??0;
            scheduledToWork.JobCode = employeeView.JobCode;
            scheduledToWork.WorkedJobCode = employeeView.JobCode;
            scheduledToWork.PayCode = payCode;
        }
        public override void MapBudgetRangeEntity(ref BudgetRange budgetRange, BudgetRangeView view)
        {
            budgetRange.RangeId = view.RangeId;
            budgetRange.StartDate = view.StartDate;
            budgetRange.EndDate = view.EndDate;
            budgetRange.Location = view.Location;
            budgetRange.GenCode = view.GenCode;
            budgetRange.SubCode = view.SubCode;
            budgetRange.CompanyCode = view.CompanyCode;
            budgetRange.BusinessUnit = view.BusinessUnit;
            budgetRange.ObjectNumber = view.ObjectNumber;
            budgetRange.Subsidiary = view.Subsidiary;
            budgetRange.AccountId = view.AccountId;
            budgetRange.SupervisorCode = view.SupervisorCode;
            DateTime? now = DateTime.Now;
            budgetRange.LastUpdated = now;
            budgetRange.IsActive = view.IsActive;
        }
        public override void MapBudgetEntity(ref Budget budget, BudgetView budgetView)
        {
            budget.BudgetId = budgetView.BudgetId;
            budget.BudgetHours = budgetView.BudgetHours;
            budget.BudgetAmount = budgetView.BudgetAmount;
            budget.ActualHours = budgetView.ActualHours;
            budget.ActualAmount = budgetView.ActualAmount;
            budget.AccountId = budgetView.AccountId;
            budget.RangeId = budgetView.RangeId;
            budget.ProjectedHours = budgetView.ProjectedHours??0;
            budget.ProjectedAmount = budgetView.ProjectedAmount??0;
            budget.ActualsAsOfDate = budgetView.ActualsAsOfDate;
    }
        public override void MapRangeToBudgetViewEntity(ref BudgetView budgetView, BudgetRangeView budgetRangeView)
        {
            budgetView.AccountId = budgetRangeView.AccountId;
            budgetView.RangeId = budgetRangeView.RangeId;
            budgetView.RangeStartDate = budgetRangeView.StartDate;
            budgetView.RangeEndDate = budgetRangeView.EndDate;
            budgetView.RangeIsActive = budgetRangeView.IsActive;
        }
        public override BudgetActualsView MapBudgetRangeToBudgetActuals(BudgetRangeView budgetRangeView)
        {
            BudgetActualsView budgetActualsView = new BudgetActualsView();

            budgetActualsView.RangeId = budgetRangeView.RangeId;
            budgetActualsView.RangeStartDate = budgetRangeView.StartDate;
            budgetActualsView.RangeEndDate = budgetRangeView.EndDate;
            budgetActualsView.AccountId = budgetRangeView.AccountId;

            return budgetActualsView;
        }
        public override void MapSupplierViewEntity(ref Supplier supplier, SupplierView view)
        {
            supplier.SupplierId = view.SupplierId??0;
            supplier.AddressId = view.AddressId??0;
            supplier.Identification = view.SupplierIdentification;

    }
    public override BudgetView MapBudgetView(Budget budget)
        {
            return new BudgetView(budget);
        }
        public override BudgetRangeView MapBudgetRangeView(BudgetRange budgetRange)
        { return new BudgetRangeView(budgetRange); }
        public override TimeAndAttendanceScheduleView MapTimeAndAttendanceScheduleView(TimeAndAttendanceSchedule schedule)
        {
            return new TimeAndAttendanceScheduleView(schedule);
        }
        public override SupplierLedgerView MapSupplierLedgerView(GeneralLedgerView generalLedgerView)
        {
            return new SupplierLedgerView(generalLedgerView);
        }
        public override SupplierLedgerView MapSupplierLedgerView(SupplierLedger supplierLedger)
        {
            return new SupplierLedgerView(supplierLedger);
        }
        public override PackingSlipDetailView MapPackingSlipDetailView(PackingSlipDetail packingSlipDetail)
        {
            return new PackingSlipDetailView(packingSlipDetail);
        }
        public override CustomerClaimView MapCustomerClaimView(CustomerClaim customerClaim)
        {
            return new CustomerClaimView(customerClaim);
        }
        public override ScheduleEventView MapScheduleEventView(ScheduleEvent scheduleEvent)
        {
            return new ScheduleEventView(scheduleEvent);
        }
        public override ContractView MapContractView(Contract contract)
        {
            return new ContractView(contract);
        }
        public override LocationAddressView MapLocationAddressView(LocationAddress locationAddress)
        {
            return new LocationAddressView(locationAddress);
        }
        public override PhoneView MapPhoneView(Phones phone)
        {
            return new PhoneView(phone);
        }
        public override EmailView MapEmailView(Emails email)
        {
            return new EmailView(email);
        }
        public override AccountReceiveableView MapAccountReceivableView(AcctRec acctRec)
        {
            return new AccountReceiveableView(acctRec);
        }
        public override ChartOfAccountView MapChartOfAccountView(ChartOfAccts chartOfAcct)
        {
            return new ChartOfAccountView(chartOfAcct);
        }
        public override void MapChartOfAccountEntity(ref ChartOfAccts item, ChartOfAccountView view)
        {
            item.BusUnit = view.BusUnit;
            item.Account = view.Account;
            item.PostEditCode = view.PostEditCode;
            item.CompanyId = view.CompanyId;
            item.Level = view.Level;
            item.Description = view.Description;
        }
        public override void MapAddressBookEntity(ref AddressBook addressBook, CustomerView customerView)
        {
            addressBook.Name = customerView.CustomerName;
            addressBook.FirstName = customerView.FirstName;
            addressBook.LastName = customerView.LastName;
            addressBook.CompanyName = customerView.CustomerName;
        }
        public override void MapAddressBookEntity(ref AddressBook addressBook, AddressBookView view)
        {
            addressBook.AddressId = view.AddressId;
            addressBook.Name = view.Name;
            addressBook.FirstName = view.FirstName;
            addressBook.LastName = view.LastName;
            addressBook.CompanyName = view.CompanyName;

            addressBook.CategoryCodeChar1 = view.CategoryCodeChar1;
            addressBook.CategoryCodeChar2 = view.CategoryCodeChar2;
            addressBook.CategoryCodeChar3 = view.CategoryCodeChar3;
            addressBook.CategoryCodeInt1 = view.CategoryCodeInt1;
            addressBook.CategoryCodeInt2 = view.CategoryCodeInt2;
            addressBook.CategoryCodeInt3 = view.CategoryCodeInt3;
            addressBook.CategoryCodeDate1 = view.CategoryCodeDate1;
            addressBook.CategoryCodeDate2 = view.CategoryCodeDate2;
            addressBook.CategoryCodeDate3 = view.CategoryCodeDate3;
        }
        public override void MapEmailEntity(ref Emails email, EmailView emailView)
        {
            email.Email = emailView.EmailText;
            email.AddressId = emailView.AddressId;
            email.LoginEmail = emailView.LoginEmail;
            email.Password = emailView.Password;
        }
        public override void MapCustomerEntity(ref Customer customer, CustomerView customerView)
        {
            customer.AddressId = customerView.AddressId;

        }
        public override AccountPayableView MapAccountPayableView(AcctPay acctPay)
        {
            return new AccountPayableView(acctPay);

        }
    }

    //General Ledger Domain
    //public partial class ApplicationViewFactory : BusinessViewFactory
    public partial class ApplicationViewFactory : AbstractFactory
    {
        public override GeneralLedgerView MapGeneralLedgerView(GeneralLedger ledger)
        { return new GeneralLedgerView(ledger); }
        public override CustomerLedgerView MapCustomerLedgerView(CustomerLedger customerLedger)
        { return new CustomerLedgerView(customerLedger); }
        public override void MapGeneralLedgerEntity(ref GeneralLedger ledger, GeneralLedgerView view)
        {
            ledger.DocNumber = view.DocNumber;
            ledger.DocType = view.DocType;
            ledger.Amount = view.Amount;
            ledger.Units = view.Hours;
            ledger.LedgerType = view.LedgerType;
            ledger.Gldate = view.GLDate;
            ledger.AccountId = view.AccountId;
            ledger.CreatedDate = DateTime.Today.Date;
            ledger.AddressId = view.AddressId;
            ledger.Comment = view.Comment;
            ledger.DebitAmount = view.DebitAmount;
            ledger.CreditAmount = view.CreditAmount;
            ledger.FiscalYear = view.FiscalYear;
            ledger.FiscalPeriod = view.FiscalPeriod;
            ledger.CheckNumber = view.CheckNumber;
            ledger.PurchaseOrderNumber = view.PurchaseOrderNumber;
            ledger.Units = view.Units;
        }
        public override void MapCustomerLedgerEntity(ref CustomerLedger customerLedger, CustomerLedgerView ledgerView)
        {
            customerLedger.AcctRecId = ledgerView.AcctRecId;
            customerLedger.CustomerId = ledgerView.CustomerId;
            customerLedger.InvoiceId = ledgerView.InvoiceId;
            customerLedger.DocNumber = ledgerView.DocNumber;
            customerLedger.DocType = ledgerView.DocType;
            customerLedger.Amount = ledgerView.Amount;
            customerLedger.Gldate = ledgerView.GLDate;
            customerLedger.AccountId = ledgerView.AccountId;
            customerLedger.CreatedDate = DateTime.Today.Date;
            customerLedger.AddressId = ledgerView.AddressId;
            customerLedger.Comment = ledgerView.Comment;
            customerLedger.DebitAmount = ledgerView.DebitAmount;
            customerLedger.CreditAmount = ledgerView.CreditAmount;
            customerLedger.FiscalYear = ledgerView.FiscalYear;
            customerLedger.FiscalPeriod = ledgerView.FiscalPeriod;
            customerLedger.GeneralLedgerId = ledgerView.GeneralLedgerId;
            customerLedger.CheckNumber = ledgerView.CheckNumber;

        }
    }
}
