using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using System.Threading.Tasks;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.InvoicesDomain;

namespace lssWebApi2.CustomerLedgerDomain
{

public class FluentCustomerLedger :IFluentCustomerLedger
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentCustomerLedger(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }

        public IFluentCustomerLedgerQuery Query()
        {
            return new FluentCustomerLedgerQuery(unitOfWork) as IFluentCustomerLedgerQuery;
        }
        private CustomerLedger MapToEntity(CustomerLedgerView inputObject)
        {
            Mapper mapper = new Mapper();
            CustomerLedger outObject = mapper.Map<CustomerLedger>(inputObject);
            return outObject;
        }
       
       
        public IFluentCustomerLedger CreateEntityByView(CustomerLedgerView view)
        {
            try
            {

                Task<CustomerLedger> queryTask = Task.Run(async () => await unitOfWork.customerLedgerRepository.FindEntityByGeneralLedgerId(view.GeneralLedgerId));
                Task.WaitAll(queryTask);


                if (queryTask.Result == null)
                {

                    CustomerLedger customerLedger = MapToEntity(view);
                    //applicationViewFactory.MapCustomerLedgerEntity(ref customerLedger, view);


                    AddCustomerLedger(customerLedger);

                    return this as IFluentCustomerLedger;


                }
                processStatus= CreateProcessStatus.AlreadyExists;
                return this as IFluentCustomerLedger;
            }
            catch (Exception ex)
            { throw new Exception("CreateEntityFromView", ex); }

        }

        public async Task<IFluentCustomerLedger> CreateEntityByGeneralLedgerView(GeneralLedgerView ledgerView)
        {

            CustomerLedgerView customerLedgerView = applicationViewFactory.MapToCustomerLedgerView(ledgerView);

            //Get the AcctRecId
           AccountReceivable acctRec = await unitOfWork.accountReceivableRepository.GetAcctRecByDocNumber(ledgerView.DocNumber);
            

            if (acctRec != null)
            {
                customerLedgerView.AcctRecId = acctRec.AcctRecId;
                customerLedgerView.InvoiceId = acctRec.InvoiceId??0;
                customerLedgerView.CustomerId = acctRec.CustomerId;
                customerLedgerView.GeneralLedgerId = ledgerView.GeneralLedgerId;

                CreateEntityByView(customerLedgerView);
              
            }
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentCustomerLedger;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public async Task<IFluentCustomerLedger> CreateCustomerLedgerByInvoiceView(InvoiceView invoiceView)
        {
            Task<AccountReceivable> acctRecLookupTask = unitOfWork.accountReceivableRepository.GetEntityByPurchaseOrderId(invoiceView.PurchaseOrderId);
            Task<Customer> customerTask = unitOfWork.customerRepository.GetEntityById(invoiceView.CustomerId);
            Task.WaitAll(acctRecLookupTask,customerTask);

            Task<GeneralLedger> generalLedgerTask = unitOfWork.generalLedgerRepository.GetEntityByDocNumber(acctRecLookupTask.Result?.DocNumber,"OV");
            Task.WaitAll(generalLedgerTask);

            CustomerLedger customerLedger = new CustomerLedger {
                CustomerId = invoiceView.CustomerId??0,
                GeneralLedgerId = generalLedgerTask.Result?.GeneralLedgerId??0,
                InvoiceId = invoiceView.InvoiceId??0,
                AcctRecId = acctRecLookupTask.Result?.AcctRecId??0,
                DocNumber = generalLedgerTask.Result?.DocNumber??0,
                DocType = generalLedgerTask.Result ?.DocType,
                Amount = invoiceView.Amount,
                Gldate = generalLedgerTask.Result?.Gldate,
                AccountId= generalLedgerTask.Result?.AccountId??0,
                CreatedDate = DateTime.Now,
                AddressId = customerTask.Result.AddressId,
                Comment = generalLedgerTask.Result?.Comment,
                DebitAmount = generalLedgerTask.Result?.DebitAmount,
                CreditAmount = generalLedgerTask.Result?.CreditAmount,
                FiscalPeriod = generalLedgerTask.Result?.FiscalPeriod??0,
                FiscalYear = generalLedgerTask.Result?.FiscalYear??0,
                CheckNumber = generalLedgerTask.Result?.CheckNumber,
                CustomerLedgerNumber = (await unitOfWork.nextNumberRepository.GetNextNumber(TypeOfCustomerLedger.CustomerLedgerNumber.ToString())).NextNumberValue
            };

            AddCustomerLedger(customerLedger);
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger AddCustomerLedgers(List<CustomerLedger> newObjects)
        {
            unitOfWork.customerLedgerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger UpdateCustomerLedgers(IList<CustomerLedger> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.customerLedgerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger AddCustomerLedger(CustomerLedger newObject) {
            unitOfWork.customerLedgerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomerLedger;
        }
        public IFluentCustomerLedger UpdateCustomerLedger(CustomerLedger updateObject) {
            unitOfWork.customerLedgerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomerLedger;

        }
        public IFluentCustomerLedger DeleteCustomerLedger(CustomerLedger deleteObject) {
            unitOfWork.customerLedgerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomerLedger;
        }
   	public IFluentCustomerLedger DeleteCustomerLedgers(List<CustomerLedger> deleteObjects)
        {
            unitOfWork.customerLedgerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomerLedger;
        }
    }
}
