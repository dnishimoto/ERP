using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ObserverMediator
{
    public interface IMessageAction
    {
        string observed_action { get; set; }
        TypeOfObservableAction command_action { get; set; }
        string targetByName { get; set; }
        AddressBook AddressBook { get; set; }
        ChartOfAccount ChartOfAccount { get; set; }
        BudgetRange BudgetRange { get; set; }
        Budget Budget { get; set; }
        ScheduleEvent ScheduleEvent { get; set; }
        ProjectManagementProject ProjectManagementProject { get; set; }
        ProjectManagementTask ProjectManagementTask { get; set; }
        ProjectManagementMilestone ProjectManagementMilestone { get; set; }
        ProjectManagementWorkOrder ProjectManagementWorkOrder { get; set; }
        ProjectManagementWorkOrderToEmployee ProjectManagementWorkOrderToEmployee { get; set; }
        Supervisor Supervisor { get; set; }
        Udc Udc { get; set; }
        Employee Employee { get; set; }
        Supplier Supplier { get; set; }
        Carrier Carrier { get; set; }
        Buyer Buyer { get; set; }
        Customer Customer { get; set; }
        TimeAndAttendancePunchIn TimeAndAttendancePunchIn { get; set; }
        TimeAndAttendanceSchedule TimeAndAttendanceSchedule { get; set; }
        TimeAndAttendanceScheduledToWork TimeAndAttendanceScheduledToWork { get; set; }
        TimeAndAttendanceShift TimeAndAttendanceShift { get; set; }
        TimeAndAttendanceSetup TimeAndAttendanceSetup { get; set; }
        Invoice Invoice { get; set; }
        InvoiceDetail InvoiceDetail { get; set; }
        AccountReceivable AccountReceivable { get; set; }
        GeneralLedger GeneralLedger { get; set; }
        ItemMaster ItemMaster { get; set; }
        EmailEntity EmailEntity { get; set; }
        LocationAddress LocationAddress { get; set; }
        CustomerLedger CustomerLedger { get; set; }
        AccountPayable AccountPayable { get; set; }
        PurchaseOrder PurchaseOrder { get; set; }
        PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        PackingSlip PackingSlip { get; set; }
        Inventory Inventory { get; set; }
        Asset Asset { get; set; }
        SupplierInvoice SupplierInvoice { get; set; }
        SupplierLedger SupplierLedger { get; set; }
        NextNumber NextNumber { get; set; }
        SalesOrder SalesOrder { get; set; }
        SalesOrderDetail SalesOrderDetail { get; set; }
        Comment Comment { get; set; }
        ShipmentDetail ShipmentDetail { get; set; }
        Shipment Shipment { get; set; }
        TaxRatesByCode TaxRatesByCode { get; set; }
        PayRollGroup PayRollGroup { get; set; }
        PayRollPaySequence PayRollPaySequence { get; set; }
        PayRollTotals PayRollTotals { get; set; }
        PayRollTransactionTypes PayRollTransactionTypes { get; set; }
        PayRollTransactionControl PayRollTransactionControl { get; set; }
        PayRollTransactionsByEmployee PayRollTransactionsByEmployee { get; set; }
        PayRollCurrentPaySequence PayRollCurrentPaySequence { get; set; }
        PayRollLedger PayRollLedger { get; set; }
        PayRollDeductionLiabilities PayRollDeductionLiabilities { get; set; }
        PayRollEarnings PayRollEarnings { get; set; }
        PhoneEntity PhoneEntity { get; set; }
        Contract Contract { get; set; }
        ContractInvoice ContractInvoice { get; set; }
        ContractItem ContractItem { get; set; }
        CustomerClaim CustomerClaim { get; set; }
        SupplierInvoiceDetail SupplierInvoiceDetail { get; set; }
        Company Company { get; set; }
        PackingSlipDetail PackingSlipDetail { get; set; }
        AccountReceivableFee AccountReceivableFee { get; set; }
        BudgetNote BudgetNote { get; set; }
        AccountReceivableInterest AccountReceivableInterest { get; set; }
        Equipment Equipment { get; set; }
        Poquote PoQuote { get; set; }
        ProjectManagementTaskToEmployee ProjectManagementTaskToEmployee { get; set; }
        ServiceInformation ServiceInformation { get; set; }
        ServiceInformationInvoice ServiceInformationInvoice { get; set; }
    }
}
