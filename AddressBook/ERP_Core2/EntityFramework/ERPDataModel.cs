namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
             : base("name=Entities")
        {
        }

        public virtual DbSet<AccountBalance> AccountBalances { get; set; }
        public virtual DbSet<AcctPay> AcctPays { get; set; }
        public virtual DbSet<AcctRec> AcctRecs { get; set; }
        public virtual DbSet<AddressBook> AddressBooks { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<BudgetRange> BudgetRanges { get; set; }
        public virtual DbSet<BudgetSnapShot> BudgetSnapShots { get; set; }
        public virtual DbSet<Buyer> Buyers { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<ChartOfAcct> ChartOfAccts { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractContent> ContractContents { get; set; }
        public virtual DbSet<ContractInvoice> ContractInvoices { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerClaim> CustomerClaims { get; set; }
        public virtual DbSet<CustomerLedger> CustomerLedgers { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<ItemMaster> ItemMasters { get; set; }
        public virtual DbSet<LocationAddress> LocationAddresses { get; set; }
        public virtual DbSet<NetTerm> NetTerms { get; set; }
        public virtual DbSet<NextNumber> NextNumbers { get; set; }
        public virtual DbSet<PackingSlip> PackingSlips { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<POQuote> POQuotes { get; set; }
        public virtual DbSet<ProjectManagementMilestone> ProjectManagementMilestones { get; set; }
        public virtual DbSet<ProjectManagementProject> ProjectManagementProjects { get; set; }
        public virtual DbSet<ProjectManagementTask> ProjectManagementTasks { get; set; }
        public virtual DbSet<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployees { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; }
        public virtual DbSet<ServiceInformation> ServiceInformations { get; set; }
        public virtual DbSet<ServiceInformationInvoice> ServiceInformationInvoices { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<ShipmentsDetail> ShipmentsDetails { get; set; }
        public virtual DbSet<ShippedToAddress> ShippedToAddresses { get; set; }
        public virtual DbSet<Supervisor> Supervisors { get; set; }
        public virtual DbSet<SupervisorEmployee> SupervisorEmployees { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TaxRatesByCode> TaxRatesByCodes { get; set; }
        public virtual DbSet<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }
        public virtual DbSet<TimeAndAttendanceSchedule> TimeAndAttendanceSchedules { get; set; }
        public virtual DbSet<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWorks { get; set; }
        public virtual DbSet<TimeAndAttendanceShift> TimeAndAttendanceShifts { get; set; }
        public virtual DbSet<UDC> UDCs { get; set; }
        public virtual DbSet<PackingSlipDetail> PackingSlipDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountBalance>()
                .Property(e => e.AccountBalanceType)
                .IsUnicode(false);

            modelBuilder.Entity<AccountBalance>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.GrossAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.Tax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.DocType)
                .IsUnicode(false);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.PaymentTerms)
                .IsUnicode(false);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.DiscountPercent)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.AmountOpen)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.OrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AcctPay>()
                .Property(e => e.AmountPaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.OpenAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.PaymentTerms)
                .IsUnicode(false);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.DebitAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.CreditAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AcctRec>()
                .HasMany(e => e.CustomerLedgers)
                .WithRequired(e => e.AcctRec)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.CategoryCodeChar1)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.CategoryCodeChar2)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.CategoryCodeChar3)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Buyers)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Carriers)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Emails)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.GeneralLedgers)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.LocationAddresses)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Phones)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Supervisors)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AddressBook>()
                .HasMany(e => e.Suppliers)
                .WithRequired(e => e.AddressBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.AssetCode)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.TagCode)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.ClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.SubLocation)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.GenericLocationLevel1)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.GenericLocationLevel2)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .Property(e => e.GenericLocationLevel3)
                .IsUnicode(false);

            modelBuilder.Entity<Budget>()
                .Property(e => e.BudgetHours)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Budget>()
                .Property(e => e.BudgetAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Budget>()
                .Property(e => e.ActualAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Budget>()
                .Property(e => e.ProjectedHours)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Budget>()
                .Property(e => e.ProjectedAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.GenCode)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.SubCode)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.SubsidiaryAcct)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.BusinessUnit)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.Subsidiary)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .Property(e => e.SupervisorCode)
                .IsUnicode(false);

            modelBuilder.Entity<BudgetRange>()
                .HasMany(e => e.Budgets)
                .WithOptional(e => e.BudgetRange)
                .HasForeignKey(e => e.RangeId);

            modelBuilder.Entity<BudgetRange>()
                .HasMany(e => e.Budgets1)
                .WithOptional(e => e.BudgetRange1)
                .HasForeignKey(e => e.RangeId);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.BudgetHours)
                .HasPrecision(18, 1);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.BudgetAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.ActualAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.ProjectedHours)
                .HasPrecision(18, 1);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.ProjectedAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.OpenPurchaseOrderAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<BudgetSnapShot>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Buyer>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.BusUnit)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.Subsidiary)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.SubSub)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.CompanyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.GenCode)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.SubCode)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.ObjectNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.SupCode)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.ThirdAccount)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.CategoryCode1)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.CategoryCode2)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.CategoryCode3)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .Property(e => e.PostEditCode)
                .IsUnicode(false);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.AccountBalances)
                .WithRequired(e => e.ChartOfAcct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.AcctPays)
                .WithRequired(e => e.ChartOfAcct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.AcctRecs)
                .WithRequired(e => e.ChartOfAcct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.Budgets)
                .WithOptional(e => e.ChartOfAcct)
                .HasForeignKey(e => e.AccountId);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.Budgets1)
                .WithOptional(e => e.ChartOfAcct1)
                .HasForeignKey(e => e.AccountId);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.GeneralLedgers)
                .WithRequired(e => e.ChartOfAcct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChartOfAcct>()
                .HasMany(e => e.PurchaseOrders)
                .WithRequired(e => e.ChartOfAcct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyStreet)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyCity)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyState)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyZipcode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.TaxCode1)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.TaxCode2)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.ChartOfAccts)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Contract>()
                .Property(e => e.RemainingBalance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Contract>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ServiceInformations)
                .WithRequired(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ContractContents)
                .WithRequired(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractContent>()
                .Property(e => e.WBS)
                .IsUnicode(false);

            modelBuilder.Entity<ContractContent>()
                .Property(e => e.TextMemo)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.TaxIdentification)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.AcctRecs)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ServiceInformations)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerClaims)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerLedgers)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.POQuotes)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.SalesOrders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Shipments)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerClaim>()
                .Property(e => e.Configuration)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerClaim>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerLedger>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CustomerLedger>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerLedger>()
                .Property(e => e.DocType)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerLedger>()
                .Property(e => e.DebitAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CustomerLedger>()
                .Property(e => e.CreditAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Email>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.Email1)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TaxIdentification)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CustomerClaims)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ScheduleEvents)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SupervisorEmployees)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TimeAndAttendancePunchIns)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.DocType)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.LedgerType)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.DebitAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.CreditAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.CheckNumber)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralLedger>()
                .Property(e => e.PurchaseOrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralLedger>()
                .HasMany(e => e.CustomerLedgers)
                .WithRequired(e => e.GeneralLedger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.UOM)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.SKU)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.ExtendedPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.InvoiceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.PaymentTerms)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.AcctRecs)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.CustomerLedgers)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.ServiceInformationInvoices)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.UnitOfMeasure)
                .IsUnicode(false);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.DiscountPercent)
                .HasPrecision(18, 4);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(e => e.ExtendedDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.UnitOfMeasure)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.CommodityCode)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.Description2)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.ItemNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ItemMaster>()
                .HasOptional(e => e.Inventory)
                .WithRequired(e => e.ItemMaster);

            modelBuilder.Entity<ItemMaster>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.ItemMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemMaster>()
                .HasMany(e => e.PurchaseOrderDetails)
                .WithRequired(e => e.ItemMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemMaster>()
                .HasMany(e => e.PackingSlipDetails)
                .WithRequired(e => e.ItemMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemMaster>()
                .HasMany(e => e.SalesOrderDetails)
                .WithRequired(e => e.ItemMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemMaster>()
                .HasMany(e => e.ShipmentsDetails)
                .WithRequired(e => e.ItemMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.Address_Line_1)
                .IsUnicode(false);

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.Address_Line_2)
                .IsUnicode(false);

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.Zipcode)
                .IsUnicode(false);

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.State)
                .IsFixedLength();

            modelBuilder.Entity<LocationAddress>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<LocationAddress>()
                .HasMany(e => e.ServiceInformations)
                .WithRequired(e => e.LocationAddress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NetTerm>()
                .Property(e => e.NetTerms)
                .IsUnicode(false);

            modelBuilder.Entity<NextNumber>()
                .Property(e => e.NextNumberName)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlip>()
                .Property(e => e.SlipDocument)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlip>()
                .Property(e => e.PONumber)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlip>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlip>()
                .Property(e => e.SlipType)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlip>()
                .HasMany(e => e.PackingSlipDetails)
                .WithRequired(e => e.PackingSlip)
                .HasForeignKey(e => e.PackagingSlipId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phone>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Phone>()
                .Property(e => e.PhoneType)
                .IsUnicode(false);

            modelBuilder.Entity<Phone>()
                .Property(e => e.Extension)
                .IsUnicode(false);

            modelBuilder.Entity<POQuote>()
                .Property(e => e.QuoteAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<POQuote>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<POQuote>()
                .Property(e => e.SKU)
                .IsUnicode(false);

            modelBuilder.Entity<POQuote>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementMilestone>()
                .Property(e => e.MilestoneName)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementMilestone>()
                .Property(e => e.WBS)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementMilestone>()
                .HasMany(e => e.ProjectManagementTasks)
                .WithRequired(e => e.ProjectManagementMilestone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectManagementProject>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementProject>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementProject>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementProject>()
                .HasMany(e => e.ProjectManagementTasks)
                .WithRequired(e => e.ProjectManagementProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectManagementTask>()
                .Property(e => e.WBS)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementTask>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementTask>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectManagementTask>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.DocType)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.PaymentTerms)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.GrossAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.PONumber)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.TakenBy)
                .IsFixedLength();

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.Tax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.AmountPaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToName)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToCity)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToZipcode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.ShippedToState)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.TaxCode1)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.TaxCode2)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(e => e.PurchaseOrderDetails)
                .WithRequired(e => e.PurchaseOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(e => e.OrderedQuantity)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(e => e.UnitOfMeasure)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.OrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.OrderType)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.TakenBy)
                .IsFixedLength();

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.UnitOfMeasure)
                .IsFixedLength();

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.FreightAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.PaymentInstrument)
                .IsFixedLength();

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.PaymentTerms)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrder>()
                .HasMany(e => e.SalesOrderDetails)
                .WithRequired(e => e.SalesOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.UnitOfMeasure)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.ServiceDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.AddOns)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.LocationDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.LocationGPS)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceInformation>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceInformation>()
                .HasMany(e => e.ScheduleEvents)
                .WithRequired(e => e.ServiceInformation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceInformation>()
                .HasMany(e => e.ServiceInformationInvoices)
                .WithRequired(e => e.ServiceInformation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.TrackingNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.ActualWeight)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.BillableWeight)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.Duty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.Tax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.ShippingCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.CodAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.VendorShippingCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.VendorHandlingCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipment>()
                .HasMany(e => e.ShipmentsDetails)
                .WithRequired(e => e.Shipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShipmentsDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ShippedToAddress>()
                .Property(e => e.ShipToAddressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<ShippedToAddress>()
                .Property(e => e.ShipToAddressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<ShippedToAddress>()
                .Property(e => e.ShipToState)
                .IsUnicode(false);

            modelBuilder.Entity<ShippedToAddress>()
                .Property(e => e.ShipToCity)
                .IsUnicode(false);

            modelBuilder.Entity<ShippedToAddress>()
                .Property(e => e.ShipToZipcode)
                .IsUnicode(false);

            modelBuilder.Entity<Supervisor>()
                .Property(e => e.SupervisorCode)
                .IsUnicode(false);

            modelBuilder.Entity<Supervisor>()
                .HasMany(e => e.SupervisorEmployees)
                .WithRequired(e => e.Supervisor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supervisor>()
                .HasMany(e => e.TimeAndAttendancePunchIns)
                .WithRequired(e => e.Supervisor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Identification)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.AcctPays)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.PackingSlips)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.POQuotes)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.PurchaseOrders)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaxRatesByCode>()
                .Property(e => e.TaxCode)
                .IsUnicode(false);

            modelBuilder.Entity<TaxRatesByCode>()
                .Property(e => e.TaxRate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TaxRatesByCode>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendancePunchIn>()
                .Property(e => e.PunchinDateTime)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendancePunchIn>()
                .Property(e => e.PunchoutDateTime)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendancePunchIn>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendancePunchIn>()
                .Property(e => e.mealPunchin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendancePunchIn>()
                .Property(e => e.mealPunchout)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendanceSchedule>()
                .Property(e => e.ScheduleName)
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendanceSchedule>()
                .HasMany(e => e.TimeAndAttendancePunchIns)
                .WithRequired(e => e.TimeAndAttendanceSchedule)
                .HasForeignKey(e => e.SupervisorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeAndAttendanceSchedule>()
                .HasMany(e => e.TimeAndAttendanceScheduledToWorks)
                .WithRequired(e => e.TimeAndAttendanceSchedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeAndAttendanceShift>()
                .Property(e => e.ShiftName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeAndAttendanceShift>()
                .Property(e => e.ShiftStartTime)
                .IsFixedLength();

            modelBuilder.Entity<TimeAndAttendanceShift>()
                .Property(e => e.ShiftEndTime)
                .IsFixedLength();

            modelBuilder.Entity<UDC>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<UDC>()
                .Property(e => e.KeyCode)
                .IsUnicode(false);

            modelBuilder.Entity<UDC>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.AcctRecs)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.AcctRecDocTypeXRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Assets)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.EquipmentStatusXRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Carriers)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.CarrierTypeXrefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.UDC)
                .HasForeignKey(e => e.ServiceTypeXRefId);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.CustomerClaims)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.ClassificationXRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.CustomerClaims1)
                .WithRequired(e => e.UDC1)
                .HasForeignKey(e => e.GroupIdXrefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.EmploymentStatusXRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Employees1)
                .WithRequired(e => e.UDC1)
                .HasForeignKey(e => e.JobTitleXrefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.LocationAddresses)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.TypeXRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.ProjectManagementTasks)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.StatusXrefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.ServiceInformations)
                .WithOptional(e => e.UDC)
                .HasForeignKey(e => e.ServiceTypeXRefId);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.Supervisors)
                .WithOptional(e => e.UDC)
                .HasForeignKey(e => e.JobTitleXrefId);

            modelBuilder.Entity<UDC>()
                .HasMany(e => e.TimeAndAttendancePunchIns)
                .WithRequired(e => e.UDC)
                .HasForeignKey(e => e.TypeOfTimeUdcXrefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PackingSlipDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PackingSlipDetail>()
                .Property(e => e.ExtendedCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PackingSlipDetail>()
                .Property(e => e.UnitOfMeasure)
                .IsUnicode(false);

            modelBuilder.Entity<PackingSlipDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
