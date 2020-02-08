using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.BudgetDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.ShipmentsDomain;
using lssWebApi2.NextNumberDomain;
using lssWebApi2.CommentDomain;
using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.SalesOrderDomain;
using lssWebApi2.PayRollDomain;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.CustomerClaimDomain;
using lssWebApi2.SupplierInvoiceDetailDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.CarrierDomain;
using lssWebApi2.BuyerDomain;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.CompanyDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.BudgetNoteDomain;
using lssWebApi2.AccountReceivableInterestDomain;
using lssWebApi2.EquipmentDomain;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.POQuoteDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.ServiceInformationInvoiceDomain;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.TimeAndAttendanceSetupDomain;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;
using lssWebApi2.ContractItemDomain;
using lssWebApi2.ContractInvoiceDomain;
using lssWebApi2.JobMasterDomain;
using lssWebApi2.JobPhaseDomain;
using lssWebApi2.JobCostTypeDomain;
using lssWebApi2.JobCostLedgerDomain;
using lssWebApi2.EmailDomain;
using lssWebApi2.AccountPayableDetailDomain;
using lssWebApi2.AccountReceivableDetailDomain;
//using Microsoft.Extensions.DependencyInjection;

namespace lssWebApi2.Services
{
    public class UnitOfWork
    {
        //ListensoftwaredbContext db = new ListensoftwaredbContext();
        private ListensoftwaredbContext _db;
        public UnitOfWork()
        {
            try
            {
                // var serviceCollection = new ServiceCollection();
                // IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
                // this._db = serviceProvider.GetRequiredService<ListensoftwaredbContext>();
                this._db = new ListensoftwaredbContext();
            }
            catch (Exception ex) { throw new Exception("UnitOfWork", ex); }
        }
        public AddressBookRepository addressBookRepository => new AddressBookRepository(_db);
        public ChartOfAccountRepository chartOfAccountRepository => new ChartOfAccountRepository(_db);
        public BudgetSnapShotRepository budgetSnapShotRepository => new BudgetSnapShotRepository(_db);
        public BudgetRangeRepository budgetRangeRepository => new BudgetRangeRepository(_db);
        public BudgetRepository budgetRepository => new BudgetRepository(_db);
        public ScheduleEventRepository scheduleEventRepository => new ScheduleEventRepository(_db);
        public ProjectManagementProjectRepository projectManagementProjectRepository => new ProjectManagementProjectRepository(_db);
        public ProjectManagementTaskRepository projectManagementTaskRepository => new ProjectManagementTaskRepository(_db);
        public ProjectManagementMilestoneRepository projectManagementMilestoneRepository => new ProjectManagementMilestoneRepository(_db);
        public ProjectManagementWorkOrderRepository projectManagementWorkOrderRepository => new ProjectManagementWorkOrderRepository(_db);
        public ProjectManagementWorkOrderToEmployeeRepository projectManagementWorkOrderToEmployeeRepository => new ProjectManagementWorkOrderToEmployeeRepository(_db);
        public SupervisorRepository supervisorRepository => new SupervisorRepository(_db);
        public UdcRepository udcRepository => new UdcRepository(_db);
        public EmployeeRepository employeeRepository => new EmployeeRepository(_db);
        public SupplierRepository supplierRepository => new SupplierRepository(_db);
        public CarrierRepository carrierRepository => new CarrierRepository(_db);
        public BuyerRepository buyerRepository => new BuyerRepository(_db);
        public CustomerRepository customerRepository => new CustomerRepository(_db);
        public TimeAndAttendanceRepository timeAndAttendanceRepository => new TimeAndAttendanceRepository(_db);
        public TimeAndAttendanceScheduleRepository timeAndAttendanceScheduleRepository => new TimeAndAttendanceScheduleRepository(_db);
        public TimeAndAttendanceScheduledToWorkRepository timeAndAttendanceScheduledToWorkRepository => new TimeAndAttendanceScheduledToWorkRepository(_db);
        public TimeAndAttendanceShiftRepository timeAndAttendanceShiftRepository => new TimeAndAttendanceShiftRepository(_db);
        public TimeAndAttendanceSetupRepository timeAndAttendanceSetupRepository => new TimeAndAttendanceSetupRepository(_db);
        public InvoiceRepository invoiceRepository => new InvoiceRepository(_db);
        public InvoiceDetailRepository invoiceDetailRepository => new InvoiceDetailRepository(_db);
        public AccountReceivableRepository accountReceivableRepository => new AccountReceivableRepository(_db);
        public AccountReceivableDetailRepository accountReceivableDetailRepository => new AccountReceivableDetailRepository(_db);
        public GeneralLedgerRepository generalLedgerRepository => new GeneralLedgerRepository(_db);
        public ItemMasterRepository itemMasterRepository => new ItemMasterRepository(_db);
        public EmailRepository emailRepository => new EmailRepository(_db);
        public LocationAddressRepository locationAddressRepository => new LocationAddressRepository(_db);
        public CustomerLedgerRepository customerLedgerRepository => new CustomerLedgerRepository(_db);
        public AccountPayableRepository accountPayableRepository => new AccountPayableRepository(_db);
        public AccountPayableDetailRepository accountPayableDetailRepository => new AccountPayableDetailRepository(_db);
        public PurchaseOrderRepository purchaseOrderRepository => new PurchaseOrderRepository(_db);
        public PurchaseOrderDetailRepository purchaseOrderDetailRepository => new PurchaseOrderDetailRepository(_db);
        public PackingSlipRepository packingSlipRepository => new PackingSlipRepository(_db);
        public InventoryRepository inventoryRepository => new InventoryRepository(_db);
        public AssetRepository assetRepository => new AssetRepository(_db);
        public SupplierInvoiceRepository supplierInvoiceRepository => new SupplierInvoiceRepository(_db);
        public SupplierLedgerRepository supplierLedgerRepository => new SupplierLedgerRepository(_db);
        public NextNumberRepository nextNumberRepository => new NextNumberRepository(_db);
        public SalesOrderRepository salesOrderRepository => new SalesOrderRepository(_db);
        public SalesOrderDetailRepository salesOrderDetailRepository => new SalesOrderDetailRepository(_db);
        public CommentRepository commentRepository=>new CommentRepository(_db);
        public ShipmentDetailRepository shipmentDetailRepository => new ShipmentDetailRepository(_db);
        public ShipmentRepository shipmentRepository => new ShipmentRepository(_db);
        public TaxRateByCodeRepository taxRateByCodeRepository => new TaxRateByCodeRepository(_db);
        public PayRollGroupRepository payRollGroupRepository => new PayRollGroupRepository(_db);
        public PayRollPaySequenceRepository payRollPaySequenceRepository => new PayRollPaySequenceRepository(_db);
        public PayRollTotalsRepository payRollTotalsRepository => new PayRollTotalsRepository(_db);
        public PayRollTransactionTypesRepository payRollTransactionTypesRepository => new PayRollTransactionTypesRepository(_db);
        public PayRollTransactionControlRepository payRollTransactionControlRepository => new PayRollTransactionControlRepository(_db);
        public PayRollTransactionsByEmployeeRepository payRollTransactionsByEmployeeRepository => new PayRollTransactionsByEmployeeRepository(_db);
        public PayRollCurrentPaySequenceRepository payRollCurrentPaySequenceRepository => new PayRollCurrentPaySequenceRepository(_db);
        public PayRollLedgerRepository payRollLedgerRepository => new PayRollLedgerRepository(_db);
        public PayRollDeductionLiabilitiesRepository payRollDeductionLiabilitiesRepository => new PayRollDeductionLiabilitiesRepository(_db);
        public PayRollEarningsRepository payRollEarningsRepository => new PayRollEarningsRepository(_db);
        public PhoneRepository phoneRepository => new PhoneRepository(_db);
        public ContractRepository contractRepository => new ContractRepository(_db);
        public ContractInvoiceRepository contractInvoiceRepository => new ContractInvoiceRepository(_db);
        public ContractItemRepository contractItemRepository => new ContractItemRepository(_db);
        public CustomerClaimRepository customerClaimRepository => new CustomerClaimRepository(_db);
        public SupplierInvoiceDetailRepository supplierInvoiceDetailRepository => new SupplierInvoiceDetailRepository(_db);
        public CompanyRepository companyRepository => new CompanyRepository(_db);
        public PackingSlipDetailRepository packingSlipDetailRepository => new PackingSlipDetailRepository(_db);
        public AccountReceivableFeeRepository accountReceivableFeeRepository => new AccountReceivableFeeRepository(_db);
        public BudgetNoteRepository budgetNoteRepository => new BudgetNoteRepository(_db);
        public AccountReceivableInterestRepository accountReceivableInterestRepository => new AccountReceivableInterestRepository(_db);
        public EquipmentRepository equipmentRepository => new EquipmentRepository(_db);
        public POQuoteRepository poQuoteRepository => new POQuoteRepository(_db);
        public ProjectManagementTaskToEmployeeRepository projectManagementTaskToEmployeeRepository => new ProjectManagementTaskToEmployeeRepository(_db);
        public ServiceInformationRepository serviceInformationRepository => new ServiceInformationRepository(_db);
        public ServiceInformationInvoiceRepository serviceInformationInvoiceRepository => new ServiceInformationInvoiceRepository(_db);
        public JobMasterRepository jobMasterRepository => new JobMasterRepository(_db);
        public JobPhaseRepository jobPhaseRepository => new JobPhaseRepository(_db);
        public JobCostTypeRepository jobCostTypeRepository => new JobCostTypeRepository(_db);
        public JobCostLedgerRepository jobCostLedgerRepository => new JobCostLedgerRepository(_db);

        public void CommitChanges()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ' ' + ex.InnerException);
            }
        }
    }
}
