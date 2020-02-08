using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using lssWebApi2.BudgetDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.TimeAndAttendanceScheduleDomain;

namespace lssWebApi2.AbstractFactory
{
    public abstract partial class AbstractFactory
    {
        public abstract void MapSupplierLedgerEntity(ref SupplierLedger supplierLedger, SupplierLedgerView view);
        public abstract CustomerLedgerView MapToCustomerLedgerView(GeneralLedgerView ledgerView);
        public abstract SupplierLedgerView MapSupplierLedgerView(GeneralLedgerView generalLedgerView);
        public abstract void MapTimeAndAttendanceScheduledToWorkEntity(ref TimeAndAttendanceScheduledToWork scheduledToWork, TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeView, string payCode);
        public abstract void MapAddressBookEntity(ref AddressBook addressBook, CustomerView customerView);
          public abstract void MapRangeToBudgetViewEntity(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        public abstract BudgetActualsView MapBudgetRangeToBudgetActuals(BudgetRangeView budgetRangeView);
        public abstract void MapAcctRecFeeEntity(ref AccountReceivableFee acctRecFee, AccountReceivableFlatView view);
        public abstract void MapPackingSlipIntoInventoryEntity(ref Inventory inventory, PackingSlipDetailView packingSlipDetailView);
    }
    //Address Book
    public partial class ApplicationViewFactory : AbstractFactory
    {
        public override void MapSupplierLedgerEntity(ref SupplierLedger supplierLedger, SupplierLedgerView view)

        {
            supplierLedger.SupplierLedgerId = view.SupplierLedgerId;
            supplierLedger.SupplierId = view.SupplierId;
            supplierLedger.InvoiceId = view.InvoiceId;
            supplierLedger.AcctPayId = view.AcctPayId;
            supplierLedger.Amount = view.Amount;
            supplierLedger.Gldate = view.Gldate;
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
        public override CustomerLedgerView MapToCustomerLedgerView(GeneralLedgerView ledgerView)
        {
            return new CustomerLedgerView
            {
                CustomerId = ledgerView.CustomerId ?? 0,
                GeneralLedgerId = ledgerView.GeneralLedgerId,
                //CustomerName= ledgerView.Cusot
                InvoiceId = ledgerView.InvoiceId ?? 0,
                //AcctRecId 
                //CustomerLedgerId = ledgerView.cu
                DocNumber = ledgerView.DocNumber,
                DocType = ledgerView.DocType,
                Amount = ledgerView.Amount,
                LedgerType = ledgerView.LedgerType,
                GLDate = ledgerView.GLDate,
                AccountId = ledgerView.AccountId,
                CreatedDate = ledgerView.CreatedDate,
                AddressId = ledgerView.AddressId,
                Comment = ledgerView.Comment,
                DebitAmount = ledgerView.DebitAmount,
                CreditAmount = ledgerView.CreditAmount,
                FiscalPeriod = ledgerView.FiscalPeriod,
                FiscalYear = ledgerView.FiscalYear,
                CheckNumber = ledgerView.CheckNumber
            };

        }
        public override void MapAcctRecFeeEntity(ref AccountReceivableFee acctRecFee, AccountReceivableFlatView view)

        {
            //public decimal? FeeAmount { get; set; }
            acctRecFee.PaymentDueDate = view.PaymentDueDate;
            acctRecFee.CustomerId = view.CustomerId;
            acctRecFee.DocNumber = view.DocNumber ?? 0;
            acctRecFee.AcctRecDocType = view.AcctRecDocType;
            acctRecFee.AccountReceivableId = view.AccountReceivableId ?? 0;
        }
        public override void MapTimeAndAttendanceScheduledToWorkEntity(ref TimeAndAttendanceScheduledToWork scheduledToWork, TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeView, string payCode)

        {
            scheduledToWork.EmployeeId = employeeView.EmployeeId;
            scheduledToWork.EmployeeName = employeeView.EmployeeName;
            scheduledToWork.ScheduleId = scheduleView.ScheduleId;
            scheduledToWork.ScheduleName = scheduleView.ScheduleName;
            scheduledToWork.StartDate = dayView.StartDate;
            scheduledToWork.EndDate = dayView.EndDate;
            scheduledToWork.StartDateTime = dayView.StartDateTime;
            scheduledToWork.EndDateTime = dayView.EndDateTime;
            scheduledToWork.ShiftId = scheduleView.ShiftId ?? 0;
            scheduledToWork.JobCode = employeeView.JobCode;
            scheduledToWork.WorkedJobCode = employeeView.JobCode;
            scheduledToWork.PayCode = payCode;
        }
        public override SupplierLedgerView MapSupplierLedgerView(GeneralLedgerView ledgerView)
        {
            return new SupplierLedgerView
            {
                SupplierId = ledgerView.SupplierId ?? 0,
                InvoiceId = ledgerView.InvoiceId ?? 0,
                AcctPayId = ledgerView.AcctPayId ?? 0,
                DocNumber = ledgerView.DocNumber,
                DocType = ledgerView.DocType,
                Amount = ledgerView.Amount,
                Gldate = ledgerView.GLDate,
                CreatedDate = ledgerView.CreatedDate,
                AccountId = ledgerView.AccountId,
                AddressId = ledgerView.AddressId,
                Comment = ledgerView.Comment,
                DebitAmount = ledgerView.DebitAmount,
                CreditAmount = ledgerView.CreditAmount,
                FiscalPeriod = ledgerView.FiscalPeriod,
                FiscalYear = ledgerView.FiscalYear,
                GeneralLedgerId = ledgerView.GeneralLedgerId
            };
        }
    }
    //Customer Domain
    //public partial class ApplicationViewFactory : BusinessViewFactory
    public partial class ApplicationViewFactory : AbstractFactory
    {


        public override void MapPackingSlipIntoInventoryEntity(ref Inventory inventory, PackingSlipDetailView packingSlipDetailView)
        {
            inventory.ItemId = packingSlipDetailView.ItemId;
            inventory.Description = packingSlipDetailView.Description;
            inventory.UnitOfMeasure = packingSlipDetailView.UnitOfMeasure;
            inventory.Quantity = packingSlipDetailView.Quantity;
            inventory.ExtendedPrice = packingSlipDetailView.ExtendedCost;
            inventory.PackingSlipDetailId = packingSlipDetailView.PackingSlipDetailId;
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
    

        public override void MapAddressBookEntity(ref AddressBook addressBook, CustomerView customerView)
        {
            addressBook.Name = customerView.CustomerName;
            addressBook.FirstName = customerView.FirstName;
            addressBook.LastName = customerView.LastName;
            addressBook.CompanyName = customerView.CustomerName;
        }

      

    }


}
