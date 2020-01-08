using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using lssWebApi2.EntityFramework;

namespace lssWebApi2.ObserverMediator
{
    public class MessageAction: IMessageAction
    {
        public string observed_action { get; set; }
        public TypeOfObservableAction command_action { get; set; }
        public string targetByName { get; set; }

        public AddressBook AddressBook { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
        public BudgetRange BudgetRange { get; set; }
        public Budget Budget { get; set; }
        public ScheduleEvent ScheduleEvent { get; set; }
        public ProjectManagementProject ProjectManagementProject { get; set; }
        public ProjectManagementTask ProjectManagementTask { get; set; }
        public ProjectManagementMilestone ProjectManagementMilestone { get; set; }
        public ProjectManagementWorkOrder ProjectManagementWorkOrder { get; set; }
        public ProjectManagementWorkOrderToEmployee ProjectManagementWorkOrderToEmployee { get; set; }
        public Supervisor Supervisor { get; set; }
        public Udc Udc { get; set; }
        public Employee Employee { get; set; }
        public Supplier Supplier { get; set; }
        public Carrier Carrier { get; set; }
        public Buyer Buyer { get; set; }
        public Customer Customer { get; set; }
        public TimeAndAttendancePunchIn TimeAndAttendancePunchIn { get; set; }
        public TimeAndAttendanceSchedule TimeAndAttendanceSchedule { get; set; }
        public TimeAndAttendanceScheduledToWork TimeAndAttendanceScheduledToWork { get; set; }
        public TimeAndAttendanceShift TimeAndAttendanceShift { get; set; }
        public TimeAndAttendanceSetup TimeAndAttendanceSetup { get; set; }
        public Invoice Invoice { get; set; }
        public InvoiceDetail InvoiceDetail { get; set; }
        public AccountReceivable AccountReceivable { get; set; }
        public GeneralLedger GeneralLedger { get; set; }
        public ItemMaster ItemMaster { get; set; }
        public EmailEntity EmailEntity { get; set; }
        public LocationAddress LocationAddress { get; set; }
        public CustomerLedger CustomerLedger { get; set; }
        public AccountPayable AccountPayable { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }
        public PackingSlip PackingSlip { get; set; }
        public Inventory Inventory { get; set; }
        public Asset Asset { get; set; }
        public SupplierInvoice SupplierInvoice { get; set; }
        public SupplierLedger SupplierLedger { get; set; }
        public NextNumber NextNumber { get; set; }
        public SalesOrder SalesOrder { get; set; }
        public SalesOrderDetail SalesOrderDetail { get; set; }
        public Comment Comment { get; set; }
        public ShipmentDetail ShipmentDetail { get; set; }
        public Shipment Shipment { get; set; }
        public TaxRatesByCode TaxRatesByCode { get; set; }
        public PayRollGroup PayRollGroup { get; set; }
        public PayRollPaySequence PayRollPaySequence { get; set; }
        public PayRollTotals PayRollTotals { get; set; }
        public PayRollTransactionTypes PayRollTransactionTypes { get; set; }
        public PayRollTransactionControl PayRollTransactionControl { get; set; }
        public PayRollTransactionsByEmployee PayRollTransactionsByEmployee { get; set; }
        public PayRollCurrentPaySequence PayRollCurrentPaySequence { get; set; }
        public PayRollLedger PayRollLedger { get; set; }
        public PayRollDeductionLiabilities PayRollDeductionLiabilities { get; set; }
        public PayRollEarnings PayRollEarnings { get; set; }
        public PhoneEntity PhoneEntity { get; set; }
        public Contract Contract { get; set; }
        public ContractInvoice ContractInvoice { get; set; }
        public ContractItem ContractItem { get; set; }
        public CustomerClaim CustomerClaim { get; set; }
        public SupplierInvoiceDetail SupplierInvoiceDetail { get; set; }
        public Company Company { get; set; }
        public PackingSlipDetail PackingSlipDetail { get; set; }
        public AccountReceivableFee AccountReceivableFee { get; set; }
        public BudgetNote BudgetNote { get; set; }
        public AccountReceivableInterest AccountReceivableInterest { get; set; }
        public Equipment Equipment { get; set; }
        public Poquote PoQuote { get; set; }
        public ProjectManagementTaskToEmployee ProjectManagementTaskToEmployee { get; set; }
        public ServiceInformation ServiceInformation { get; set; }
        public ServiceInformationInvoice ServiceInformationInvoice { get; set; }
    }
    public class ObservableAction : IObservableAction
    {
        public ObservableAction()
        {
           Actions = new List<MessageAction>();
        }
        
        public List<MessageAction> Actions { get; set; }
  

    }
}
