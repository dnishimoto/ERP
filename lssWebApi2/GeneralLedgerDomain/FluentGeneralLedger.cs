using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.AccountReceivableDomain;
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

namespace lssWebApi2.GeneralLedgerDomain
{
   
    public class FluentGeneralLedger : AbstractErrorHandling, IFluentGeneralLedger
    {
        public UnitOfWork unitOfWork ;
        public CreateProcessStatus processStatus;

        public FluentGeneralLedger(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
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
        public IFluentGeneralLedger UpdateLedgerBalances(long docNumber, string docType)
        {

            Task<GeneralLedger> ledgerTask =  unitOfWork.generalLedgerRepository.GetEntityByDocNumber(docNumber, docType);
            Task.WaitAll(ledgerTask);
            Task<bool> resultTask =  unitOfWork.generalLedgerRepository.UpdateBalanceByAccountId(ledgerTask.Result.AccountId, ledgerTask.Result.FiscalYear, ledgerTask.Result.FiscalPeriod,ledgerTask.Result.DocType);
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
        public async Task<IFluentGeneralLedger> CreateGeneralLedgerByInvoiceView(InvoiceView invoiceView)
        {
            try
            {

                AccountReceivable acctRec=  await unitOfWork.accountReceivableRepository.GetEntityByPurchaseOrderId(invoiceView.PurchaseOrderId);


                if (acctRec != null)
                {

                    GeneralLedger generalLedgerLookup = await unitOfWork.generalLedgerRepository.FindEntityByDocNumber(acctRec.DocNumber);
  
                    if (generalLedgerLookup == null)
                    {

                        long addressId = await unitOfWork.addressBookRepository.GetAddressIdByCustomerId(acctRec.CustomerId);

                        //Revenue Account
                        ChartOfAccount chartOfAcct = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "250", "");
                      

                        GeneralLedger ledger = new GeneralLedger();
                        ledger.DocNumber = acctRec.DocNumber ?? 0;
                        ledger.DocType = "OV";
                        ledger.Amount = acctRec.Amount ?? 0;
                        ledger.LedgerType = "AA";
                        ledger.Gldate = DateTime.Now.Date;
                        ledger.FiscalPeriod = DateTime.Now.Date.Month;
                        ledger.FiscalYear = DateTime.Now.Date.Year;
                        ledger.AccountId = chartOfAcct.AccountId;
                        ledger.CreatedDate = DateTime.Now.Date;
                        ledger.AddressId = addressId;
                        ledger.Comment = acctRec.Remarks;
                        ledger.DebitAmount = 0.0M;
                        ledger.CreditAmount = acctRec.Amount ?? 0;
                        ledger.GeneralLedgerNumber = (await unitOfWork.nextNumberRepository.GetNextNumber(TypeOfGeneralLedger.GeneralLedgerNumber.ToString())).NextNumberValue;

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
        public IFluentGeneralLedger UpdateGeneralLedgers(IList<GeneralLedger> newObjects)
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
