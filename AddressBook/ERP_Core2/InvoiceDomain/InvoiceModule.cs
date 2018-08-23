using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.InvoiceDomain
{


    public interface IQuery
    {
        IQuery Query();
       
    }

    public interface IInvoiceModule
    {
        IInvoice Invoice();
        IInvoiceDetail InvoiceDetail();
        IAccountsReceivable AccountsReceivable();
        IGeneralLedger GeneralLedger();
    }
    public interface IInvoice 
    {
        IInvoice CreateInvoice(InvoiceView invoiceView);
        IInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView);
        IInvoice Apply();
        IQuery Query();
    }
    public interface IInvoiceDetail
    {

        IInvoiceDetail CreateInvoiceDetails(InvoiceView invoiceView);
        IInvoiceDetail Apply();
        IQuery Query();

    }
    public interface IAccountsReceivable
    {
        IAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView);
        IAccountsReceivable Apply();
        IQuery Query();
    }
    public interface IGeneralLedger
    {

        IGeneralLedger CreateGeneralLedger(InvoiceView invoiceView);
        IGeneralLedger Apply();
        IGeneralLedger UpdateLedgerBalances();
    }

    public class FluentInvoice : AbstractErrorHandling, IInvoice
    {
        InvoiceModule _parent;
        public FluentInvoice(){}
        public FluentInvoice(InvoiceModule parent) { _parent = parent; }
        public IInvoice Apply()
        {
            if (_parent.processStatus == CreateProcessStatus.Inserted) { _parent.unitOfWork.CommitChanges(); }
            return this as IInvoice;
        }
        public IInvoice CreateInvoice(InvoiceView invoiceView)
        {

            Task<CreateProcessStatus> resultTask = Task.Run(() => _parent.unitOfWork.invoiceRepository.CreateInvoiceByView(invoiceView));
            Task.WaitAll(resultTask);
            _parent.processStatus = resultTask.Result;
            return this as IInvoice;

        }
        public IInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView)
        {

            string invoiceNumber = invoiceView.InvoiceNumber;

            Task<Invoice> viewTask = Task.Run(() => _parent.unitOfWork.invoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber));
            Task.WaitAll(viewTask);

            //TODO applicationFactory needs to have a merge feature created
            invoiceView.InvoiceId = viewTask.Result.InvoiceId;
            invoiceView.InvoiceNumber = viewTask.Result.InvoiceNumber;
            invoiceView.InvoiceDate = viewTask.Result.InvoiceDate;
            invoiceView.Amount = viewTask.Result.Amount;
            invoiceView.CustomerId = viewTask.Result.Customer.CustomerId;
            invoiceView.CustomerName = viewTask.Result.Customer.AddressBook.Name;
            invoiceView.Description = viewTask.Result.Description;
            invoiceView.TaxAmount = viewTask.Result.TaxAmount;
            invoiceView.PaymentDueDate = viewTask.Result.PaymentDueDate;
            invoiceView.DiscountAmount = viewTask.Result.DiscountAmount;
            invoiceView.PaymentTerms = viewTask.Result.PaymentTerms;
            invoiceView.CompanyId = viewTask.Result.Company.CompanyId;
            invoiceView.CompanyName = viewTask.Result.Company.CompanyName;
            invoiceView.CompanyStreet = viewTask.Result.Company.CompanyStreet;
            invoiceView.CompanyCity = viewTask.Result.Company.CompanyCity;
            invoiceView.CompanyZipcode = viewTask.Result.Company.CompanyZipcode;
            invoiceView.DiscountDueDate = viewTask.Result.DiscountDueDate;
            invoiceView.FreightCost = viewTask.Result.FreightCost;

            return this as IInvoice;
        }

        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            query._parent = _parent;
            return query as IQuery;
        }

    }
    public class FluentInvoiceDetail : AbstractErrorHandling, IInvoiceDetail
    {
        InvoiceModule _parent;
        public FluentInvoiceDetail(){}
        public FluentInvoiceDetail(InvoiceModule parent) { _parent = parent; }

        public IInvoiceDetail CreateInvoiceDetails(InvoiceView invoiceView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => _parent.unitOfWork.invoiceDetailRepository.CreateInvoiceDetailsByView(invoiceView));
            Task.WaitAll(resultTask);
            _parent.processStatus = resultTask.Result;
            return this as IInvoiceDetail;
        }

        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            query._parent = _parent;
            return query as IQuery;
        }
        public IInvoiceDetail Apply()
        {
            if (_parent.processStatus == CreateProcessStatus.Inserted) { _parent.unitOfWork.CommitChanges(); }
            return this as IInvoiceDetail;
        }
    }
    public class FluentAccountsReceivable : AbstractErrorHandling, IAccountsReceivable
    {
        InvoiceModule _parent;
        public FluentAccountsReceivable() { }
        public FluentAccountsReceivable(InvoiceModule parent) { _parent = parent; }
        public IAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => _parent.unitOfWork.accountReceiveableRepository.CreateAcctRecFromInvoice(invoiceView));
            Task.WaitAll(resultTask);
            _parent.processStatus = resultTask.Result;
            return this as IAccountsReceivable;
        }
        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            query._parent = _parent;
            return query as IQuery;
        }
        public IAccountsReceivable Apply()
        {
            if (_parent.processStatus == CreateProcessStatus.Inserted) { _parent.unitOfWork.CommitChanges(); }
            return this as IAccountsReceivable;
        }
    }
    public class FluentGeneralLedger : AbstractErrorHandling, IGeneralLedger
    {
        InvoiceModule _parent;
        public FluentGeneralLedger() { }
        public FluentGeneralLedger(InvoiceModule parent) { _parent = parent; }
        public IGeneralLedger UpdateLedgerBalances()
        {

            Task<GeneralLedgerView> ledgerTask = Task.Run(() => _parent.unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(_parent.lastAccountReceivableView.DocNumber, "OV"));
            Task.WaitAll(ledgerTask);
            Task<bool> resultTask = Task.Run(() => _parent.unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerTask.Result.AccountId, ledgerTask.Result.FiscalYear, ledgerTask.Result.FiscalPeriod));
            Task.WaitAll(resultTask);
            return this as IGeneralLedger;
        }

        public IGeneralLedger CreateGeneralLedger(InvoiceView invoiceView)
        {
            try
            {

                Task<AccountReceiveableView> acctRecViewTask = Task.Run(() => _parent.unitOfWork.accountReceiveableRepository.GetAccountReceivableViewByInvoiceId(invoiceView.InvoiceId));
                Task.WaitAll(acctRecViewTask);
                if (acctRecViewTask.Result != null)
                {
                    _parent.lastAccountReceivableView = acctRecViewTask.Result;

                    Task<bool> resultTask = Task.Run(() => _parent.unitOfWork.generalLedgerRepository.CreateLedgerFromReceiveable(acctRecViewTask.Result));
                    Task.WaitAll(resultTask);
                    if (resultTask.Result == true)
                    {
                        _parent.processStatus = CreateProcessStatus.Inserted;
                        return this as IGeneralLedger;
                    }

                }
                _parent.processStatus = CreateProcessStatus.AlreadyExists;
                return this as IGeneralLedger;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }



        public IGeneralLedger Apply()
        {
            if (_parent.processStatus == CreateProcessStatus.Inserted) { _parent.unitOfWork.CommitChanges(); }
            return this as IGeneralLedger;
        }
    }
    public class FluentQuery: AbstractErrorHandling, IQuery
    {
        public InvoiceModule _parent;

        public IQuery Query() {
            return this as IQuery; }
     
    }
    public class InvoiceModule : AbstractModule, IInvoiceModule
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public AccountReceiveableView lastAccountReceivableView;

        public CreateProcessStatus processStatus;

        public IInvoice Invoice()
        {
            return new FluentInvoice(this) as IInvoice;
        }
        public IInvoiceDetail InvoiceDetail()
        {
            return new FluentInvoiceDetail(this) as IInvoiceDetail;
        }
        public IAccountsReceivable AccountsReceivable()
        {
            return new FluentAccountsReceivable(this) as IAccountsReceivable;
        }
        public IGeneralLedger GeneralLedger()
        {
            return new FluentGeneralLedger(this) as IGeneralLedger;
        }
     

    }
}
