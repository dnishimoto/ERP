using lssWebApi2.AbstractFactory;
using lssWebApi2.Interfaces;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;

namespace lssWebApi2.AccountsReceivableDomain
{
   
   
    public class FluentAccountReceivable : AbstractErrorHandling, IFluentAccountReceivable
    {
        public UnitOfWork unitOfWork ;
        public CreateProcessStatus processStatus;

        public FluentAccountReceivable(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }

        public IFluentAccountReceivableQuery Query()
        {
            FluentAccountReceivableQuery query = new FluentAccountReceivableQuery(unitOfWork);
            return query as IFluentAccountReceivableQuery;
        }
        public IFluentAccountReceivable AdjustOpenAmount(AccountReceivableFlatView view)
        {
            try
            {
                Task<Decimal> glAmountTask = Task.Run(async () => await unitOfWork.generalLedgerRepository.GetGLAmountByDocNumber(view.DocNumber));
                Task<Decimal> acctFeeAmountTask = Task.Run(async () => await unitOfWork.accountReceivableFeeRepository.GetFeeAmountById(view.AcctRecId));
                Task<AccountReceivable> acctRecTask = Task.Run(async () => await unitOfWork.accountReceivableRepository.GetEntityByInvoiceId(view.AcctRecId));

                Task.WaitAll(glAmountTask, acctFeeAmountTask, acctRecTask);

                decimal amountPaid = glAmountTask.Result;
                decimal feeAmount = acctFeeAmountTask.Result;


                acctRecTask.Result.OpenAmount = acctRecTask.Result.Amount + feeAmount - amountPaid;
                acctRecTask.Result.LateFee = feeAmount;

                UpdateAccountReceivable(acctRecTask.Result);

                return this as IFluentAccountReceivable;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentAccountReceivable UpdateAccountReceivableByGeneralLedgerView(GeneralLedgerView ledgerView)
        {

            try
            {
                Task<AccountReceivable> acctRecTask = Task.Run(async () => await unitOfWork.accountReceivableRepository.GetAcctRecByDocNumber(ledgerView.DocNumber));
                Task.WaitAll(acctRecTask);

              


                if (acctRecTask.Result != null)
                {
                    Task<IList<GeneralLedger>> glListTask= Task.Run(async()=> await unitOfWork.generalLedgerRepository.FindEntitiesByExpression(
                    e=>
                    e.DocNumber == ledgerView.DocNumber
                                       && e.DocType == "PV"
                                       && e.LedgerType == "AA"
                                       && e.AccountId == ledgerView.AccountId
                    ));
                    Task.WaitAll(glListTask);


                    Decimal  cash = glListTask.Result.Sum(e => e.Amount);

              
                    acctRecTask.Result.DebitAmount = cash;
                    acctRecTask.Result.OpenAmount = acctRecTask.Result.Amount - acctRecTask.Result.DebitAmount;
                    decimal discountAmount = acctRecTask.Result.Amount * acctRecTask.Result.DiscountPercent ?? 0;
                    //Check for Discount Dates
                    if (
                        (acctRecTask.Result.DiscountDueDate <= ledgerView.GLDate)
                        &&
((acctRecTask.Result.DebitAmount + discountAmount) == acctRecTask.Result.Amount)
                        )
                    {
                        acctRecTask.Result.OpenAmount = acctRecTask.Result.Amount - (acctRecTask.Result.DebitAmount + discountAmount);

                    }
                    UpdateAccountReceivable(acctRecTask.Result);
                    return this as IFluentAccountReceivable;
                }
                processStatus= CreateProcessStatus.Failed;
                return this as IFluentAccountReceivable;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<IFluentAccountReceivable> UpdateAcctRecByInvoiceView(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoice=  await unitOfWork.invoiceRepository.GetEntityByInvoiceDocument(invoiceView.InvoiceDocument);
                Decimal? totalInvoiceAmount= await unitOfWork.invoiceRepository.GetInvoicedAmountByPurchaseOrderId(invoiceView.PurchaseOrderId);

                
                if (invoice != null)
                {
                    long? invoiceId = invoice.InvoiceId;

                    AccountReceivable acctRecLookup =  await unitOfWork.accountReceivableRepository.GetEntityByPurchaseOrderId(invoice.PurchaseOrderId);

                    if (acctRecLookup != null)
                    {


                        //decimal ? totalInvoiceAmount = listInvoiceTasks.Result.Sum<Invoice>(e => e.Amount);
        
                        acctRecLookup.OpenAmount = acctRecLookup.Amount-totalInvoiceAmount;
                        acctRecLookup.DebitAmount = 0;
                        acctRecLookup.CreditAmount = totalInvoiceAmount;
                        
                        UpdateAccountReceivable(acctRecLookup);
                        return this as IFluentAccountReceivable;
                    }

                }
                processStatus= CreateProcessStatus.AlreadyExists;
                return this as IFluentAccountReceivable;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        
       
       
      

           
        public IFluentAccountReceivable  Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return  this as IFluentAccountReceivable;
        }
        public IFluentAccountReceivable AddAccountReceivablesByList(List<AccountReceivable> newObjects)
        {
            unitOfWork.accountReceivableRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivable;
        }
        public IFluentAccountReceivable UpdateAccountReceivablesByList(IList<AccountReceivable> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountReceivableRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivable;
        }
        public IFluentAccountReceivable AddAccountReceivable(AccountReceivable newObject)
        {
            unitOfWork.accountReceivableRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivable;
        }
        public IFluentAccountReceivable UpdateAccountReceivable(AccountReceivable updateObject)
        {
            unitOfWork.accountReceivableRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivable;

        }
        public IFluentAccountReceivable DeleteAccountReceivable(AccountReceivable deleteObject)
        {
            unitOfWork.accountReceivableRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivable;
        }
        public IFluentAccountReceivable DeleteAccountReceivablesByList(List<AccountReceivable> deleteObjects)
        {
            unitOfWork.accountReceivableRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivable;
        }
    }
}
