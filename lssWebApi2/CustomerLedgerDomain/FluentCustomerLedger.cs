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

namespace lssWebApi2.CustomerLedgerDomain
{

public class FluentCustomerLedger :IFluentCustomerLedger
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentCustomerLedger() { }
       
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

        public IFluentCustomerLedger CreateEntityByGeneralLedgerView(GeneralLedgerView ledgerView)
        {

            CustomerLedgerView customerLedgerView = applicationViewFactory.MapToCustomerLedgerView(ledgerView);

            //Get the AcctRecId
            Task<AccountReceivable> acctRecTask = Task.Run(() => unitOfWork.accountReceivableRepository.GetAcctRecByDocNumber(ledgerView.DocNumber));
            Task.WaitAll(acctRecTask);

            if (acctRecTask.Result != null)
            {
                customerLedgerView.AcctRecId = acctRecTask.Result.AcctRecId;
                customerLedgerView.InvoiceId = acctRecTask.Result.InvoiceId;
                customerLedgerView.CustomerId = acctRecTask.Result.CustomerId;
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
