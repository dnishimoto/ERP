using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace lssWebApi2.entityframework
{
    public partial class ListensoftwareDBContext : DbContext
    {
        public ListensoftwareDBContext()
        {
        }

        public ListensoftwareDBContext(DbContextOptions<ListensoftwareDBContext> options)
            : base(options)
        {

        }


        public virtual DbSet<AccountBalance> AccountBalance { get; set; }
        public virtual DbSet<AcctPay> AcctPay { get; set; }
        public virtual DbSet<AcctRec> AcctRec { get; set; }
        public virtual DbSet<AddressBook> AddressBook { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<Budget> Budget { get; set; }
        public virtual DbSet<BudgetRange> BudgetRange { get; set; }
        public virtual DbSet<BudgetSnapShot> BudgetSnapShot { get; set; }
        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<ChartOfAccts> ChartOfAccts { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractContent> ContractContent { get; set; }
        public virtual DbSet<ContractInvoice> ContractInvoice { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerClaim> CustomerClaim { get; set; }
        public virtual DbSet<CustomerLedger> CustomerLedger { get; set; }
        public virtual DbSet<Emails> Emails { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Equations> Equations { get; set; }
        public virtual DbSet<GeneralLedger> GeneralLedger { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual DbSet<ItemMaster> ItemMaster { get; set; }
        public virtual DbSet<LocationAddress> LocationAddress { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<NetTerms> NetTerms { get; set; }
        public virtual DbSet<NextNumber> NextNumber { get; set; }
        public virtual DbSet<PackingSlip> PackingSlip { get; set; }
        public virtual DbSet<PackingSlipDetail> PackingSlipDetail { get; set; }
        public virtual DbSet<Phones> Phones { get; set; }
        public virtual DbSet<Poquote> Poquote { get; set; }
        public virtual DbSet<ProjectManagementMilestones> ProjectManagementMilestones { get; set; }
        public virtual DbSet<ProjectManagementProject> ProjectManagementProject { get; set; }
        public virtual DbSet<ProjectManagementTask> ProjectManagementTask { get; set; }
        public virtual DbSet<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployee { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual DbSet<SalesOrder> SalesOrder { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual DbSet<ScheduleEvent> ScheduleEvent { get; set; }
        public virtual DbSet<ServiceInformation> ServiceInformation { get; set; }
        public virtual DbSet<ServiceInformationInvoice> ServiceInformationInvoice { get; set; }
        public virtual DbSet<Shipments> Shipments { get; set; }
        public virtual DbSet<ShipmentsDetail> ShipmentsDetail { get; set; }
        public virtual DbSet<ShippedToAddresses> ShippedToAddresses { get; set; }
        public virtual DbSet<Supervisor> Supervisor { get; set; }
        public virtual DbSet<SupervisorEmployees> SupervisorEmployees { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierInvoice> SupplierInvoice { get; set; }
        public virtual DbSet<SupplierInvoiceDetail> SupplierInvoiceDetail { get; set; }
        public virtual DbSet<SupplierLedger> SupplierLedger { get; set; }
        public virtual DbSet<TaxRatesByCode> TaxRatesByCode { get; set; }
        public virtual DbSet<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual DbSet<TimeAndAttendanceSchedule> TimeAndAttendanceSchedule { get; set; }
        public virtual DbSet<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }
        public virtual DbSet<TimeAndAttendanceShift> TimeAndAttendanceShift { get; set; }
        public virtual DbSet<Udc> Udc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var environmentPath = Environment.CurrentDirectory;
                var assemblyPath = System.IO.Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                */

                var directoryPath = Directory.GetCurrentDirectory();

              

                IConfigurationRoot configuration = new ConfigurationBuilder()
                       .SetBasePath(directoryPath)
                       .AddJsonFile("appsettings.json")
                       .Build();
                var connectionString = configuration.GetConnectionString("DbCoreConnectionString2");
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountBalance>(entity =>
            {
                entity.Property(e => e.AccountBalanceType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountBalance)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountBalance_ChartOfAccts");
            });

            modelBuilder.Entity<AcctPay>(entity =>
            {
                entity.HasIndex(e => e.ContractId)
                    .HasName("idx_acctpay_contractid")
                    .IsUnique();

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("idx_acctpay_invoiceid")
                    .IsUnique();

                entity.HasIndex(e => e.SupplierId)
                    .HasName("idx_acctpay_supperid")
                    .IsUnique();

                entity.Property(e => e.AmountOpen).HasColumnType("money");

                entity.Property(e => e.AmountPaid).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.DocType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PoquoteId).HasColumnName("POQuoteId");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AcctPay)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctPay_ChartOfAccts");

                entity.HasOne(d => d.Contract)
                    .WithOne(p => p.AcctPay)
                    .HasForeignKey<AcctPay>(d => d.ContractId)
                    .HasConstraintName("FK_AcctPay_Contract");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.AcctPay)
                    .HasForeignKey<AcctPay>(d => d.InvoiceId)
                    .HasConstraintName("FK__AcctPay__Invoice__06ED0088");

                entity.HasOne(d => d.Poquote)
                    .WithMany(p => p.AcctPay)
                    .HasForeignKey(d => d.PoquoteId)
                    .HasConstraintName("FK_AcctPay_POQuote");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.AcctPay)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK__AcctPay__Purchas__031C6FA4");

                entity.HasOne(d => d.Supplier)
                    .WithOne(p => p.AcctPay)
                    .HasForeignKey<AcctPay>(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctPay_Supplier");
            });

            modelBuilder.Entity<AcctRec>(entity =>
            {
                entity.Property(e => e.AcctRecDocTypeXrefId).HasColumnName("AcctRecDocTypeXRefId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CreditAmount).HasColumnType("money");

                entity.Property(e => e.DebitAmount).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("date");

                entity.Property(e => e.OpenAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDueDate).HasColumnType("date");

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AcctRec)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctRec_ChartOfAccts");

                entity.HasOne(d => d.AcctRecDocTypeXref)
                    .WithMany(p => p.AcctRec)
                    .HasForeignKey(d => d.AcctRecDocTypeXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctRec_UDC");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AcctRec)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctRec_Customer");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.AcctRec)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcctRec_Invoices");
            });

            modelBuilder.Entity<AddressBook>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.CategoryCodeChar1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCodeChar2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCodeChar3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCodeDate1).HasColumnType("date");

                entity.Property(e => e.CategoryCodeDate2).HasColumnType("date");

                entity.Property(e => e.CategoryCodeDate3).HasColumnType("date");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Assets>(entity =>
            {
                entity.HasKey(e => e.AssetId);

                entity.Property(e => e.AcquiredDate).HasColumnType("date");

                entity.Property(e => e.AssetCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentStatusXrefId).HasColumnName("EquipmentStatusXRefId");

                entity.Property(e => e.GenericLocationLevel1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenericLocationLevel2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenericLocationLevel3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TagCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EquipmentStatusXref)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.EquipmentStatusXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assets_UDC");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BudgetAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BudgetHours).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.ProjectedAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProjectedHours).HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Budget)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Budget_ChartOfAccts");

                entity.HasOne(d => d.Range)
                    .WithMany(p => p.Budget)
                    .HasForeignKey(d => d.RangeId)
                    .HasConstraintName("FK_Budget_BudgetRange");
            });

            modelBuilder.Entity<BudgetRange>(entity =>
            {
                entity.HasKey(e => e.RangeId);

                entity.Property(e => e.BusinessUnit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.GenCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.SubCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Subsidiary)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SupervisorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BudgetRange)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_BudgetRange_ChartOfAccts");
            });

            modelBuilder.Entity<BudgetSnapShot>(entity =>
            {
                entity.HasKey(e => e.BudgetId);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BudgetAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BudgetHours).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.OpenPurchaseOrderAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProjectedAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProjectedHours).HasColumnType("decimal(18, 1)");
            });

            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Buyer)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Buyer_AddressBook");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Carrier)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carier_AddressBook");

                entity.HasOne(d => d.CarrierTypeXref)
                    .WithMany(p => p.Carrier)
                    .HasForeignKey(d => d.CarrierTypeXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carrier__Carrier__668030F6");
            });

            modelBuilder.Entity<ChartOfAccts>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.Account)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.BusUnit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCode1)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCode2)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCode3)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GenCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostEditCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SubCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SubSub)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Subsidiary)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdAccount)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ChartOfAccts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChartOfAccts_Company");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyStreet)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyZipcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode1)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode2)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RemainingBalance).HasColumnType("money");

                entity.Property(e => e.ServiceTypeXrefId).HasColumnName("ServiceTypeXRefId");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.ServiceTypeXref)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.ServiceTypeXrefId)
                    .HasConstraintName("FK_Contract_UDC");
            });

            modelBuilder.Entity<ContractContent>(entity =>
            {
                entity.Property(e => e.TextMemo).IsUnicode(false);

                entity.Property(e => e.Wbs)
                    .HasColumnName("WBS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractContent)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractContent_Contract");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.TaxIdentification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_AddressBook");
            });

            modelBuilder.Entity<CustomerClaim>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.HasIndex(e => e.ClassificationXrefId)
                    .HasName("idx_customerclaim_classificationXrefid");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_customerclaim_customerid");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("idx_customerclaim_employeeid");

                entity.Property(e => e.ClassificationXrefId).HasColumnName("ClassificationXRefId");

                entity.Property(e => e.Configuration).IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.ProcessedDate).HasColumnType("date");

                entity.HasOne(d => d.ClassificationXref)
                    .WithMany(p => p.CustomerClaimClassificationXref)
                    .HasForeignKey(d => d.ClassificationXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerClaim_UDC");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerClaim)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerClaim_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CustomerClaim)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerClaim_Employee");

                entity.HasOne(d => d.GroupIdXref)
                    .WithMany(p => p.CustomerClaimGroupIdXref)
                    .HasForeignKey(d => d.GroupIdXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerClaim_UDC1");
            });

            modelBuilder.Entity<CustomerLedger>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditAmount).HasColumnType("money");

                entity.Property(e => e.DebitAmount).HasColumnType("money");

                entity.Property(e => e.DocType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.AcctRec)
                    .WithMany(p => p.CustomerLedger)
                    .HasForeignKey(d => d.AcctRecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLedger_AcctRec");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerLedger)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLedger_Customer");

                entity.HasOne(d => d.GeneralLedger)
                    .WithMany(p => p.CustomerLedger)
                    .HasForeignKey(d => d.GeneralLedgerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLedger_GeneralLedger");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.CustomerLedger)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLedger_Invoice");
            });

            modelBuilder.Entity<Emails>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emails_AddressBook");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmploymentStatusXrefId).HasColumnName("EmploymentStatusXRefId");

                entity.Property(e => e.HiredDate).HasColumnType("date");

                entity.Property(e => e.TaxIdentification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TerminationDate).HasColumnType("date");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Addres__4AD81681");

                entity.HasOne(d => d.EmploymentStatusXref)
                    .WithMany(p => p.EmployeeEmploymentStatusXref)
                    .HasForeignKey(d => d.EmploymentStatusXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Employ__49E3F248");

                entity.HasOne(d => d.JobTitleXref)
                    .WithMany(p => p.EmployeeJobTitleXref)
                    .HasForeignKey(d => d.JobTitleXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__JobTit__48EFCE0F");
            });

            modelBuilder.Entity<Equations>(entity =>
            {
                entity.Property(e => e.Cellname)
                    .HasColumnName("cellname")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Equation)
                    .HasColumnName("equation")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Evaluated)
                    .HasColumnName("evaluated")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Queueid)
                    .HasColumnName("queueid")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeneralLedger>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CheckNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditAmount).HasColumnType("money");

                entity.Property(e => e.DebitAmount).HasColumnType("money");

                entity.Property(e => e.DocType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.LedgerType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseOrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Units).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.GeneralLedger)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralLedger_ChartOfAccts");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.GeneralLedger)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralLedger_AddressBook");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExtendedPrice).HasColumnType("money");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ItemMaster");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.FreightCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDueDate).HasColumnType("date");

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Company");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Customer");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ExtendedDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetail)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesDetail_Invoices");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.InvoiceDetail)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesDetail_ItemMaster");
            });

            modelBuilder.Entity<ItemMaster>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.CommodityCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<LocationAddress>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("Address Line 1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("Address Line 2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.TypeXrefId).HasColumnName("TypeXRefId");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.LocationAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationAddress_AddressBook");

                entity.HasOne(d => d.TypeXref)
                    .WithMany(p => p.LocationAddress)
                    .HasForeignKey(d => d.TypeXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationAddress_UDCType");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<NetTerms>(entity =>
            {
                entity.HasKey(e => e.NetTermId);

                entity.Property(e => e.NetTerms1)
                    .HasColumnName("NetTerms")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NextNumber>(entity =>
            {
                entity.Property(e => e.NextNumberName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NextNumberValue).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PackingSlip>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Ponumber)
                    .HasColumnName("PONumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SlipDocument)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SlipType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PackingSlip)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipt_Supplier");
            });

            modelBuilder.Entity<PackingSlipDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ExtendedCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.PackingSlip)
                    .WithMany(p => p.PackingSlipDetail)
                    .HasForeignKey(d => d.PackingSlipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackingSlipDetail_PackingSlip");
            });

            modelBuilder.Entity<Phones>(entity =>
            {
                entity.HasKey(e => e.PhoneId);

                entity.Property(e => e.Extension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phones_AddressBook");
            });

            modelBuilder.Entity<Poquote>(entity =>
            {
                entity.ToTable("POQuote");

                entity.Property(e => e.PoquoteId).HasColumnName("POQuoteId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.QuoteAmount).HasColumnType("money");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubmittedDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Poquote)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POQuote_Customer");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Poquote)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POQuote_Supplier");
            });

            modelBuilder.Entity<ProjectManagementMilestones>(entity =>
            {
                entity.HasKey(e => e.MilestoneId);

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedEndDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedStartDate).HasColumnType("datetime");

                entity.Property(e => e.MilestoneName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wbs)
                    .HasColumnName("WBS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectManagementMilestones)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectManagementMilestones_ProjectManagementProject");
            });

            modelBuilder.Entity<ProjectManagementProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EstimatedEndDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedStartDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProjectManagementTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EstimatedEndDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedStartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wbs)
                    .HasColumnName("WBS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MileStone)
                    .WithMany(p => p.ProjectManagementTask)
                    .HasForeignKey(d => d.MileStoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManagementTask_ProjectManagementMilestones");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectManagementTask)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManagementTask_ProjectManagementProject");

                entity.HasOne(d => d.StatusXref)
                    .WithMany(p => p.ProjectManagementTask)
                    .HasForeignKey(d => d.StatusXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManagementTask_UDC");
            });

            modelBuilder.Entity<ProjectManagementTaskToEmployee>(entity =>
            {
                entity.HasKey(e => e.TaskToEmployeeId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProjectManagementTaskToEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__ProjectMa__Emplo__4DB4832C");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.ProjectManagementTaskToEmployee)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_ProjectManagementTaskToEmployee_ProjectManagementTask");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.AmountPaid).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DocType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ponumber)
                    .HasColumnName("PONumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PoquoteId).HasColumnName("POQuoteId");

                entity.Property(e => e.PromisedDeliveredDate).HasColumnType("date");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.RequestedDate).HasColumnType("date");

                entity.Property(e => e.ShippedToAddress1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToAddress2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToState)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToZipcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TakenBy).HasMaxLength(100);

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.TaxCode1)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PurchaseOrder)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_ChartOfAccts");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrder)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_Supplier");
            });

            modelBuilder.Entity<PurchaseOrderDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedDeliveryDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderedQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ReceivedDate).HasColumnType("date");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PurchaseOrderDetail)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderDetail_ItemMaster");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderDetail)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.Property(e => e.ActualPickupDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FreightAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentInstrument).HasMaxLength(10);

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduledPickupDate).HasColumnType("datetime");

                entity.Property(e => e.TakenBy).HasMaxLength(10);

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.UnitOfMeasure).HasMaxLength(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesOrder_Customer");
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesOrderDetail_ItemMaster");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.SalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesOrderDetail_SalesOrder");
            });

            modelBuilder.Entity<ScheduleEvent>(entity =>
            {
                entity.Property(e => e.EventDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ScheduleEvent)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ScheduleEvent_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ScheduleEvent)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ScheduleE__Emplo__2759D01A");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ScheduleEvent)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ScheduleE__Servi__2942188C");
            });

            modelBuilder.Entity<ServiceInformation>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.AddOns)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LocationGps)
                    .HasColumnName("LocationGPS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceTypeXrefId).HasColumnName("ServiceTypeXRefId");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ServiceInformation)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceIn__Contr__33BFA6FF");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ServiceInformation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceIn__Custo__2A363CC5");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ServiceInformation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceIn__Locat__39788055");

                entity.HasOne(d => d.ServiceTypeXref)
                    .WithMany(p => p.ServiceInformation)
                    .HasForeignKey(d => d.ServiceTypeXrefId)
                    .HasConstraintName("FK__ServiceIn__Servi__22951AFD");
            });

            modelBuilder.Entity<ServiceInformationInvoice>(entity =>
            {
                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.ServiceInformationInvoice)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceInformationInvoice_Invoice");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceInformationInvoice)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceInformationInvoice_ServiceInformation");
            });

            modelBuilder.Entity<Shipments>(entity =>
            {
                entity.HasKey(e => e.ShipmentId);

                entity.Property(e => e.ActualWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillableWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CodAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Duty).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ShipmentDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorHandlingCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.VendorShippingCost).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipments_Customer");
            });

            modelBuilder.Entity<ShipmentsDetail>(entity =>
            {
                entity.HasKey(e => e.ShipmentDetailId);

                entity.Property(e => e.ShipmentDetailId).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ShipmentsDetail)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentsDetail_ItemMaster");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentsDetail)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentsDetail_Shipments");
            });

            modelBuilder.Entity<ShippedToAddresses>(entity =>
            {
                entity.HasKey(e => e.ShippedToAddressId);

                entity.Property(e => e.ShipToAddressLine1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipToAddressLine2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipToCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipToState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipToZipcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.Property(e => e.SupervisorCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Supervisor)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supervisor_AddressBook");

                entity.HasOne(d => d.JobTitleXref)
                    .WithMany(p => p.Supervisor)
                    .HasForeignKey(d => d.JobTitleXrefId)
                    .HasConstraintName("FK_Supervisor_UDC");
            });

            modelBuilder.Entity<SupervisorEmployees>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SupervisorEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Superviso__Emplo__4BCC3ABA");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.SupervisorEmployees)
                    .HasForeignKey(d => d.SupervisorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupervisorEmployees_Supervisor");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Identification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_AddressBook");
            });

            modelBuilder.Entity<SupplierInvoice>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.FreightCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PaymentDueDate).HasColumnType("date");

                entity.Property(e => e.PaymentTerms)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ponumber)
                    .HasColumnName("PONumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierInvoiceDate).HasColumnType("date");

                entity.Property(e => e.SupplierInvoiceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierInvoice)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierInvoice_Supplier");
            });

            modelBuilder.Entity<SupplierInvoiceDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountDueDate).HasColumnType("date");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ExtendedCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SupplierInvoiceDetail)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierInvoicesDetail_ItemMaster");

                entity.HasOne(d => d.SupplierInvoice)
                    .WithMany(p => p.SupplierInvoiceDetail)
                    .HasForeignKey(d => d.SupplierInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierInvoicesDetail_SupplierInvoices");
            });

            modelBuilder.Entity<SupplierLedger>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditAmount).HasColumnType("money");

                entity.Property(e => e.DebitAmount).HasColumnType("money");

                entity.Property(e => e.DocType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gldate)
                    .HasColumnName("GLDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.AcctPay)
                    .WithMany(p => p.SupplierLedger)
                    .HasForeignKey(d => d.AcctPayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierLedger_AcctPay");

                entity.HasOne(d => d.GeneralLedger)
                    .WithMany(p => p.SupplierLedger)
                    .HasForeignKey(d => d.GeneralLedgerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierLedger_GeneralLedger");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.SupplierLedger)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierLedger_SupplierInvoice");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierLedger)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierLedger_Supplier");
            });

            modelBuilder.Entity<TaxRatesByCode>(entity =>
            {
                entity.HasKey(e => e.TaxId);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TaxRate).HasColumnType("money");
            });

            modelBuilder.Entity<TimeAndAttendancePunchIn>(entity =>
            {
                entity.HasKey(e => e.TimePunchinId);

                entity.Property(e => e.MealPunchin)
                    .HasColumnName("mealPunchin")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.MealPunchout)
                    .HasColumnName("mealPunchout")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessedDate).HasColumnType("date");

                entity.Property(e => e.PunchinDate).HasColumnType("date");

                entity.Property(e => e.PunchinDateTime)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.PunchoutDate).HasColumnType("date");

                entity.Property(e => e.PunchoutDateTime)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeAndAttendancePunchIn)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeAndAt__Emplo__4CC05EF3");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TimeAndAttendancePunchIn)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.TimeAndAttendancePunchIn)
                    .HasForeignKey(d => d.SupervisorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule");

                entity.HasOne(d => d.SupervisorNavigation)
                    .WithMany(p => p.TimeAndAttendancePunchIn)
                    .HasForeignKey(d => d.SupervisorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeAndAttendancePunchIn_Supervisor");

                entity.HasOne(d => d.TypeOfTimeUdcXref)
                    .WithMany(p => p.TimeAndAttendancePunchIn)
                    .HasForeignKey(d => d.TypeOfTimeUdcXrefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeAndAttendancePunchIn_UDC");
            });

            modelBuilder.Entity<TimeAndAttendanceSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ScheduleGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TimeAndAttendanceSchedule)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK_TimeAndAttendanceSchedule_TimeAndAttendanceShift");
            });

            modelBuilder.Entity<TimeAndAttendanceScheduledToWork>(entity =>
            {
                entity.HasKey(e => e.ScheduledToWorkId);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndDateTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartDateTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeAndAttendanceScheduledToWork)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeAndAttendanceScheduledToWork_Employee");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TimeAndAttendanceScheduledToWork)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceSchedule");
            });

            modelBuilder.Entity<TimeAndAttendanceShift>(entity =>
            {
                entity.HasKey(e => e.ShiftId);

                entity.Property(e => e.ShiftName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Udc>(entity =>
            {
                entity.HasKey(e => e.XrefId);

                entity.ToTable("UDC");

                entity.Property(e => e.XrefId).HasColumnName("XRefId");

                entity.Property(e => e.KeyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
