﻿using ERP_Core2.EntityFramework;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.CustomerDomain;
using MillenniumERP.CustomerLedgerDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoiceDetailsDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract PhoneView MapPhoneView(Phone phone);
        public abstract EmailView MapEmailView(Email email);
        public abstract AccountReceiveableView MapAccountReceivableView(AcctRec acctRec);
        public abstract void MapEmailEntity(ref Email email, EmailView emailView);
        public abstract void MapAddressBookEntity(ref AddressBook addressBook, CustomerView customerView);
        public abstract void MapCustomerEntity(ref Customer customer, CustomerView customerView);
        public abstract void MapLocationAddressEntity(ref LocationAddress locationAddress, LocationAddressView view);
        public abstract ChartOfAccountView MapChartOfAccountView(ChartOfAcct chartOfAcct);
        public abstract void MapChartOfAccountEntity(ref ChartOfAcct item, ChartOfAccountView chartOfAccountView);
        public abstract GeneralLedgerView MapGeneralLedgerView(GeneralLedger generalLedger);
        public abstract void MapInvoiceEntity(ref Invoice invoice, InvoiceView invoiceView);
        public abstract void MapInvoiceDetailEntity(ref InvoiceDetail invoiceDetail, InvoiceDetailView invoiceDetailView);
        public abstract void MapCustomerLedgerEntity(ref CustomerLedger customerLedger, CustomerLedgerView customerLedgerView);
        public abstract CustomerLedgerView MapCustomerLedgerView(CustomerLedger customerLedger);
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
    public abstract partial class BusinessViewFactory : AbstractFactory
    {
        //pass through class
    }


    //Address Book
    public partial class ApplicationViewFactory : BusinessViewFactory
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
            locationAddress.Address_Line_1 = view.Address_Line1;
            locationAddress.City = view.City;
            locationAddress.State = view.State;
            locationAddress.Country = view.Country;
            locationAddress.Zipcode = view.Zipcode;
            locationAddress.TypeXRefId = view.TypeXRefId;
        }


    }
    //Time and Attendance
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override TimeAndAttendancePunchInView MapTAPunchinView(TimeAndAttendancePunchIn taPunchin)
        {
            return new TimeAndAttendancePunchInView(taPunchin);
        }
    }

    //Customer Domain
    public partial class ApplicationViewFactory : BusinessViewFactory
    {
        public override BuyerView MapBuyerView(Buyer buyer)
        {
            return new BuyerView(buyer);
        }
        public override InvoiceView MapInvoiceView(Invoice invoice)
        {
            InvoiceView invoiceView = new InvoiceView(invoice);
            List<InvoiceDetailView> list = new List<InvoiceDetailView>();


            foreach (var item in invoice.InvoiceDetails)
            {
                list.Add(MapInvoiceDetails(item));
            }
            invoiceView.InvoiceViewDetails = list;
            return invoiceView;
        }
        public override InvoiceDetailView MapInvoiceDetails(InvoiceDetail invoiceDetail)
        {
            return new InvoiceDetailView(invoiceDetail);
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

        }
        public override void MapInvoiceDetailEntity(ref InvoiceDetail invoiceDetail, InvoiceDetailView invoiceDetailView)
        {
            invoiceDetail.InvoiceId = invoiceDetailView.InvoiceId ?? 0;
            invoiceDetail.UnitOfMeasure = invoiceDetailView.UnitOfMeasure = "Project";
            invoiceDetail.Quantity = invoiceDetailView.Quantity;
            invoiceDetail.UnitPrice = invoiceDetailView.UnitPrice;
            invoiceDetail.Amount = invoiceDetailView.Amount;
            invoiceDetail.DiscountPercent = invoiceDetailView.DiscountPercent;
            invoiceDetail.DiscountAmount = invoiceDetailView.DiscountAmount;
            invoiceDetail.ItemId = invoiceDetailView.ItemId ?? 0;

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
        public override PhoneView MapPhoneView(Phone phone)
        {
            return new PhoneView(phone);
        }
        public override EmailView MapEmailView(Email email)
        {
            return new EmailView(email);
        }
        public override AccountReceiveableView MapAccountReceivableView(AcctRec acctRec)
        {
            return new AccountReceiveableView(acctRec);
        }
        public override ChartOfAccountView MapChartOfAccountView(ChartOfAcct chartOfAcct)
        {
            return new ChartOfAccountView(chartOfAcct);
        }
        public override void MapChartOfAccountEntity(ref ChartOfAcct item, ChartOfAccountView view)
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
        public override void MapEmailEntity(ref Email email, EmailView emailView)
        {
            email.Email1 = emailView.EmailText;
            email.AddressId = emailView.AddressId;
            email.LoginEmail = emailView.LoginEmail;
            email.Password = emailView.Password;
        }
        public override void MapCustomerEntity(ref Customer customer, CustomerView customerView)
        {
            customer.AddressId = customerView.AddressId;

        }
    }

    //General Ledger Domain
    public partial class ApplicationViewFactory : BusinessViewFactory
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
            ledger.LedgerType = view.LedgerType;
            ledger.GLDate = view.GLDate;
            ledger.AccountId = view.AccountId;
            ledger.CreatedDate = DateTime.Today.Date;
            ledger.AddressId = view.AddressId;
            ledger.Comment = view.Comment;
            ledger.DebitAmount = view.DebitAmount;
            ledger.CreditAmount = view.CreditAmount;
            ledger.FiscalYear = view.FiscalYear;
            ledger.FiscalPeriod = view.FiscalPeriod;
            ledger.CheckNumber = view.CheckNumber;
        }
        public override void MapCustomerLedgerEntity(ref CustomerLedger customerLedger, CustomerLedgerView ledgerView)
        {
            customerLedger.AcctRecId = ledgerView.AcctRecId;
            customerLedger.CustomerId = ledgerView.CustomerId;
            customerLedger.InvoiceId = ledgerView.InvoiceId;
            customerLedger.DocNumber = ledgerView.DocNumber;
            customerLedger.DocType = ledgerView.DocType;
            customerLedger.Amount = ledgerView.Amount;
            customerLedger.GLDate = ledgerView.GLDate;
            customerLedger.AccountId = ledgerView.AccountId;
            customerLedger.CreatedDate = DateTime.Today.Date;
            customerLedger.AddressId = ledgerView.AddressId;
            customerLedger.Comment = ledgerView.Comment;
            customerLedger.DebitAmount = ledgerView.DebitAmount;
            customerLedger.CreditAmount = ledgerView.CreditAmount;
            customerLedger.FiscalYear = ledgerView.FiscalYear;
            customerLedger.FiscalPeriod = ledgerView.FiscalPeriod;
            customerLedger.GeneralLedgerId = ledgerView.GeneralLedgerId;

        }
    }
}
