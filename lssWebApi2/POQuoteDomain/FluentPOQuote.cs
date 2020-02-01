using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.POQuoteDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.POQuoteDomain
{

public class FluentPOQuote :IFluentPOQuote
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPOQuote(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPOQuoteQuery Query()
        {
            return new FluentPOQuoteQuery(unitOfWork) as IFluentPOQuoteQuery;
        }
        public IFluentPOQuote Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPOQuote;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPOQuote AddPOQuotes(List<Poquote> newObjects)
        {
            unitOfWork.poQuoteRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPOQuote;
        }
        public IFluentPOQuote UpdatePOQuotes(IList<Poquote> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.poQuoteRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPOQuote;
        }
        public IFluentPOQuote AddPOQuote(Poquote newObject) {
            unitOfWork.poQuoteRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPOQuote;
        }
        public IFluentPOQuote UpdatePOQuote(Poquote updateObject) {
            unitOfWork.poQuoteRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPOQuote;

        }
        public IFluentPOQuote DeletePOQuote(Poquote deleteObject) {
            unitOfWork.poQuoteRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPOQuote;
        }
   	public IFluentPOQuote DeletePOQuotes(List<Poquote> deleteObjects)
        {
            unitOfWork.poQuoteRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPOQuote;
        }
    }
}
