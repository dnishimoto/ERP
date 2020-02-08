using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AccountsReceivableDomain
{
    public class FluentAccountReceivableQuery : MapperAbstract<AccountReceivable, AccountReceivableView>, IFluentAccountReceivableQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentAccountReceivableQuery() { }
        public FluentAccountReceivableQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<IList<AccountReceivableFlatView>> GetOpenAccountReceivables()
        {
            return await _unitOfWork.accountReceivableRepository.GetOpenAcctRec();
        }
        public bool HasLateFee(long? AcctRecId)
        { return _unitOfWork.accountReceivableRepository.HasLateFee(AcctRecId); }
        public bool IsPaymentLate(long? invoiceId, DateTime asOfDate)
        {
            return _unitOfWork.accountReceivableRepository.IsPaymentLate(invoiceId, asOfDate);
        }
        public async Task<AccountReceivable> MapEntityFromPurchaseOrder(PurchaseOrder purchaseOrder, Udc udcAccountReceivableType,ChartOfAccount coaAccountReceivable)
        {
            return new AccountReceivable()
            {
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                PaymentTerms = purchaseOrder.PaymentTerms,
                CustomerId = purchaseOrder.CustomerId ?? 0,
                DocNumber = (await GetDocNumber()).NextNumberValue,
                Remark = purchaseOrder.Remark,
                Gldate = purchaseOrder.Gldate,
                CustomerPurchaseOrder = purchaseOrder.Ponumber,
                Description = purchaseOrder.Description,
                DiscountDueDate = purchaseOrder.DiscountDueDate,
                Tax = purchaseOrder.Tax,
                Amount = purchaseOrder.Amount,
                DebitAmount = purchaseOrder.Amount,
                DiscountPercent = purchaseOrder.DiscountPercent,
                DiscountAmount = purchaseOrder.DiscountAmount,
                AcctRecDocType= udcAccountReceivableType.Value,
                AcctRecDocTypeXrefId= udcAccountReceivableType.XrefId,
                AccountReceivableNumber=(await GetNextNumber()).NextNumberValue,
                CreateDate=DateTime.Now,
                AccountId=coaAccountReceivable.AccountId
            };
        }
        public override async Task<AccountReceivableView> MapToView(AccountReceivable inputObject)
        {

            AccountReceivableView outObject = mapper.Map<AccountReceivableView>(inputObject);

            Task<Invoice> invoiceTask =  _unitOfWork.invoiceRepository.GetEntityById(inputObject.InvoiceId);
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject?.CustomerId);
            Task<Udc> udcTask = _unitOfWork.udcRepository.GetEntityById(inputObject.AcctRecDocTypeXrefId);
            Task.WaitAll(invoiceTask, customerTask,udcTask);

            AddressBook addressBookCustomer = await _unitOfWork.addressBookRepository.GetEntityById(customerTask?.Result.AddressId);

            outObject.CustomerName = addressBookCustomer.Name;
            outObject.InvoiceDocument = invoiceTask.Result.InvoiceDocument;
            outObject.DocType = udcTask.Result.KeyCode;

            //await Task.Yield();
            return outObject;
        }
        public override async Task<AccountReceivable> MapToEntity(AccountReceivableView inputObject)
        {
            AccountReceivable outObject = mapper.Map<AccountReceivable>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public override async Task<AccountReceivable> GetEntityById(long? accountReceivableId)
        {
            return await _unitOfWork.accountReceivableRepository.GetEntityById(accountReceivableId);

        }
        public async Task<AccountReceivable> GetEntityByNumber(long? accountReceivableNumber)
        {
            return await _unitOfWork.accountReceivableRepository.GetEntityByNumber(accountReceivableNumber);
        }
        public override async Task<AccountReceivableView> GetViewById(long? accountReceivableId)
        {
            AccountReceivable detailItem = await _unitOfWork.accountReceivableRepository.GetEntityById(accountReceivableId);

            return await MapToView(detailItem);
        }


        public override async Task<IList<AccountReceivable>> MapToEntity(IList<AccountReceivableView> inputObjects)
        {
            IList<AccountReceivable> list = new List<AccountReceivable>();
            foreach (var item in inputObjects)
            {
                AccountReceivable outObject = mapper.Map<AccountReceivable>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public async Task<AccountReceivable> GetEntityByPurchaseOrderId(long? purchaseOrderId)
        {
            return await _unitOfWork.accountReceivableRepository.GetEntityByPurchaseOrderId(purchaseOrderId);
        }
        public async Task<NextNumber> GetDocNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfAccountReceivable.DocNumber.ToString());
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfAccountReceivable.AccountReceivableNumber.ToString());
        }
        public async Task<IList<AccountReceivableView>> GetAccountReceivableViewsByCustomerId(long customerId)
        {
            try
            {
                IQueryable<AccountReceivable> query = _unitOfWork.accountReceivableRepository.GetQueryableByCustomerId(customerId);
                IList<AccountReceivableView> list = new List<AccountReceivableView>();

                foreach (var item in query)
                {
                    list.Add(await MapToView(item));
                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception("GetAccountReceivableViewsByCustomerId", ex); }
        }



    }
}
