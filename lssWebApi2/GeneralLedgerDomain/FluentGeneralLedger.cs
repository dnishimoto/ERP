using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.FluentAPI
{
   
    public class FluentGeneralLedger : AbstractErrorHandling, IFluentGeneralLedger
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public AccountReceivableView lastAccountReceivableView;
        public CreateProcessStatus processStatus;

        private FluentGeneralLedgerQuery _query = null;

        public IFluentGeneralLedgerQuery Query()
       {
            if (_query == null) { _query = new FluentGeneralLedgerQuery(unitOfWork); }

            return _query as IFluentGeneralLedgerQuery;
        }
        public FluentGeneralLedger() { }
        public IFluentGeneralLedger UpdateAccountBalances(GeneralLedgerView ledgerView)
        {
            Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerView.AccountId, ledgerView.FiscalYear, ledgerView.FiscalPeriod,ledgerView.DocType));
            Task.WaitAll(resultTask);
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger UpdateLedgerBalances()
        {

            Task<GeneralLedger> ledgerTask = Task.Run(() => unitOfWork.generalLedgerRepository.GetEntityByDocNumber(lastAccountReceivableView.DocNumber, "OV"));
            Task.WaitAll(ledgerTask);
            Task<bool> resultTask = Task.Run(() => unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerTask.Result.AccountId, ledgerTask.Result.FiscalYear, ledgerTask.Result.FiscalPeriod,ledgerTask.Result.DocType));
            Task.WaitAll(resultTask);
            return this as IFluentGeneralLedger;
        }
        private GeneralLedger MapToEntity(GeneralLedgerView inputObject)
        {
            Mapper mapper = new Mapper();
            GeneralLedger outObject = mapper.Map<GeneralLedger>(inputObject);
           
            return outObject;
        }
        public IFluentGeneralLedger CreateGeneralLedgerByView(GeneralLedgerView ledgerView)
        {
            try
            {
                Task<GeneralLedger> generalLedgerLookupTask = Task.Run(async()=>await unitOfWork.generalLedgerRepository.FindEntityByView(ledgerView));
                Task.WaitAll(generalLedgerLookupTask);

                if (generalLedgerLookupTask.Result == null)
                {
                    GeneralLedger ledger = new GeneralLedger();
                    ledger = MapToEntity(ledgerView);
                    AddGeneralLedger(ledger);
                    return this as IFluentGeneralLedger;
                }

                processStatus = CreateProcessStatus.AlreadyExists;

                return this as IFluentGeneralLedger;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentGeneralLedger CreateGeneralLedgerByInvoiceView(InvoiceView invoiceView)
        {
            try
            {

                Task<AccountReceivable> acctRecTask = Task.Run(() => unitOfWork.accountReceivableRepository.GetEntityByInvoiceId(invoiceView.InvoiceId));
                Task.WaitAll(acctRecTask);

                if (acctRecTask.Result != null)
                {

                    Task<GeneralLedger> generalLedgerLookupTask = Task.Run(async () => await unitOfWork.generalLedgerRepository.FindEntityByDocNumber(acctRecTask.Result.DocNumber));
                    Task.WaitAll(generalLedgerLookupTask);



                    if (generalLedgerLookupTask.Result == null)
                    {

                        Task<long> addressIdTask = Task.Run(async () => await unitOfWork.addressBookRepository.GetAddressIdByCustomerId(acctRecTask.Result.CustomerId));

                        //Revenue Account
                        Task<ChartOfAccount> chartOfAcctTask = Task.Run(async () => await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "250", ""));

                        Task.WaitAll(addressIdTask, chartOfAcctTask);

                        GeneralLedger ledger = new GeneralLedger();
                        ledger.DocNumber = acctRecTask.Result.DocNumber ?? 0;
                        ledger.DocType = "OV";
                        ledger.Amount = acctRecTask.Result.Amount ?? 0;
                        ledger.LedgerType = "AA";
                        ledger.Gldate = DateTime.Now.Date;
                        ledger.FiscalPeriod = DateTime.Now.Date.Month;
                        ledger.FiscalYear = DateTime.Now.Date.Year;
                        ledger.AccountId = chartOfAcctTask.Result.AccountId;
                        ledger.CreatedDate = DateTime.Now.Date;
                        ledger.AddressId = addressIdTask.Result;
                        ledger.Comment = acctRecTask.Result.Remarks;
                        ledger.DebitAmount = 0.0M;
                        ledger.CreditAmount = acctRecTask.Result.Amount ?? 0;

                        AddGeneralLedger(ledger);
                        return this as IFluentGeneralLedger;

                    }
                    processStatus = CreateProcessStatus.AlreadyExists;
                    return this as IFluentGeneralLedger;
                }
                processStatus = CreateProcessStatus.Failed;
                return this as IFluentGeneralLedger;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }



        public IFluentGeneralLedger Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger AddGeneralLedgers(List<GeneralLedger> newObjects)
        {
            unitOfWork.generalLedgerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger UpdateGeneralLedgers(List<GeneralLedger> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.generalLedgerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger AddGeneralLedger(GeneralLedger newObject)
        {
            unitOfWork.generalLedgerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger UpdateGeneralLedger(GeneralLedger updateObject)
        {
            unitOfWork.generalLedgerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentGeneralLedger;

        }
        public IFluentGeneralLedger DeleteGeneralLedger(GeneralLedger deleteObject)
        {
            unitOfWork.generalLedgerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentGeneralLedger;
        }
        public IFluentGeneralLedger DeleteGeneralLedgers(List<GeneralLedger> deleteObjects)
        {
            unitOfWork.generalLedgerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentGeneralLedger;
        }
    }
}
