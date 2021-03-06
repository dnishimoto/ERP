﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.ProjectManagementDomain;
using ERP_Core2.ScheduleEventsDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.InvoiceDetailsDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.ItemMasterDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.AccountsPayableDomain;
using ERP_Core2.PurchaseOrderDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.SupplierInvoicesDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using static ERP_Core2.TimeAndAttendanceDomain.TimeAndAttendanceScheduleView;
using ERP_Core2.TimeAndAttendanceDomain.Repository;
using ERP_Core2.ChartOfAccountsDomain;
using ERP_Core2.BudgetDomain;

namespace ERP_Core2.Services
{
    public class UnitOfWork
    {
        DbContext db = new Entities();
        public AddressBookRepository addressBookRepository => new AddressBookRepository(db);
        public ChartOfAccountRepository chartOfAccountRepository => new ChartOfAccountRepository(db);
        public BudgetSnapShotRepository budgetSnapShotRepository => new BudgetSnapShotRepository(db);
        public BudgetRangeRepository budgetRangeRepository => new BudgetRangeRepository(db);
        public BudgetRepository budgetRepository => new BudgetRepository(db);
        public ScheduleEventRepository scheduleEventRepository => new ScheduleEventRepository(db);
        public ProjectManagementProjectRepository projectManagementProjectRepository => new ProjectManagementProjectRepository(db);
        public ProjectManagementMilestoneRepository projectManagementMilestoneRepository => new ProjectManagementMilestoneRepository(db);
        public SupervisorRepository supervisorRepository => new SupervisorRepository(db);
        public UDCRepository udcRepository => new UDCRepository(db);
        public EmployeeRepository employeeRepository => new EmployeeRepository(db);
        public SupplierRepository supplierRepository => new SupplierRepository(db);
        public CarrierRepository carrierRepository => new CarrierRepository(db);
        public BuyerRepository buyerRepository => new BuyerRepository(db);
        public CustomerRepository customerRepository => new CustomerRepository(db);
        public TimeAndAttendanceRepository timeAndAttendanceRepository => new TimeAndAttendanceRepository(db);
        public TimeAndAttendanceScheduleRepository timeAndAttendanceScheduleRepository => new TimeAndAttendanceScheduleRepository(db);
        public TimeAndAttendanceScheduledToWorkRepository timeAndAttendanceScheduledToWorkRepository => new TimeAndAttendanceScheduledToWorkRepository(db);
        public InvoiceRepository invoiceRepository => new InvoiceRepository(db);
        public InvoiceDetailRepository invoiceDetailRepository => new InvoiceDetailRepository(db);
        public AccountReceivableRepository accountReceiveableRepository => new AccountReceivableRepository(db);
        public GeneralLedgerRepository generalLedgerRepository => new GeneralLedgerRepository(db);
        public ItemMasterRepository itemMasterRepository => new ItemMasterRepository(db);
        public EmailRepository emailRepository => new EmailRepository(db);
        public LocationAddressRepository locationAddressRepository => new LocationAddressRepository(db);
        public CustomerLedgerRepository customerLedgerRepository => new CustomerLedgerRepository(db);
        public AccountPayableRepository accountPayableRepository => new AccountPayableRepository(db);
        public PurchaseOrderRepository purchaseOrderRepository => new PurchaseOrderRepository(db);
        public PackingSlipRepository packingSlipRepository => new PackingSlipRepository(db);
        public InventoryRepository inventoryRepository => new InventoryRepository(db);
        public SupplierInvoiceRepository supplierInvoiceRepository => new SupplierInvoiceRepository(db);
        public SupplierLedgerRepository supplierLedgerRepository => new SupplierLedgerRepository(db);
        public UnitOfWork()
        {
            /*
            db.Database.Connection.Open();
            if (db.Database.Connection.State == ConnectionState.Open)
            {
                output.WriteLine(@"INFO: ConnectionString: " + db.Database.Connection.ConnectionString
                    + "\n DataBase: " + db.Database.Connection.Database
                    + "\n DataSource: " + db.Database.Connection.DataSource
                    + "\n ServerVersion: " + db.Database.Connection.ServerVersion
                    + "\n TimeOut: " + db.Database.Connection.ConnectionTimeout);
                db.Database.Connection.Close();

            }
            */

           
        }
        
        public void CommitChanges()
        {
            db.SaveChanges();
        }
    }
}
